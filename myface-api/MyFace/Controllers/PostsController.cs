using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFace.Models.Request;
using MyFace.Models.Response;
using MyFace.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace MyFace.Controllers
{
    [ApiController]
    [Route("/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepo _posts;
        private readonly MyFaceDbContext _context;

        public PostsController(IPostsRepo posts)
        {
            _posts = posts;
        }

        [HttpGet("")]
        public ActionResult<PostListResponse>
        Search([FromQuery] PostSearchRequest searchRequest)
        {
            var posts = _posts.Search(searchRequest);
            var postCount = _posts.Count(searchRequest);
            return PostListResponse.Create(searchRequest, posts, postCount);
        }

        [HttpGet("{id}")]
        public ActionResult<PostResponse> GetById([FromRoute] int id)
        {
            var post = _posts.GetById(id);
            return new PostResponse(post);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreatePostRequest newPost)
        {
            string authHeader = Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith("Basic")) {
                var msg = new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "The authorization header is either empty or isn't Basic." };
                throw new HttpResponseException(msg);
            }

             // Extract credentials (get rid of "Basic ")
            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();

            // decode it
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            // usernamePassword looks like username:password
            int separatorIndex = usernamePassword.IndexOf(':');
            string username = usernamePassword.Substring(0, separatorIndex);
            string password = usernamePassword.Substring(separatorIndex + 1);

            var user = _context.Users.Single(user => user.Username == username);

            string HashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(user.Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (HashedPassword != user.Hashed_password) 
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Oops!!! That password isn't right." };
                throw new HttpResponseException(msg);
            }                     

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _posts.Create(newPost);

            var url = Url.Action("GetById", new { id = post.Id });
            var postResponse = new PostResponse(post);
            return Created(url, postResponse);
        }

        [HttpPatch("{id}/update")]
        public ActionResult<PostResponse>
        Update([FromRoute] int id, [FromBody] UpdatePostRequest update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _posts.Update(id, update);
            return new PostResponse(post);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _posts.Delete (id);
            return Ok();
        }
    }

    [Serializable]
    internal class HttpResponseException : Exception
    {
        private HttpResponseMessage msg;

        public HttpResponseException()
        {
        }

        public HttpResponseException(HttpResponseMessage msg)
        {
            this.msg = msg;
        }

        public HttpResponseException(string message) : base(message)
        {
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
