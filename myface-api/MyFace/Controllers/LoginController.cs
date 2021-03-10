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
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyFace.Controllers
{
    [ApiController]
    [Route("/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersRepo _users;

        public LoginController(IUsersRepo users)
        {
            _users = users;
        }
        public ActionResult Index() {

            string authHeader = Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith("Basic")) 
            {
                // var resp = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                // {
                //     ReasonPhrase = "The authorization header is either empty or isn't Basic."
                // };
                // throw new HttpResponseException(resp);   
                return StatusCode(401);
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
           
            var user = _users.GetByUsername(username);

            string HashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(user.Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (HashedPassword != user.Hashed_password) 
            {
                // var resp = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                // {
                //     ReasonPhrase = "The password is incorrect."
                // };
                // throw new HttpResponseException(resp);
                return StatusCode(401);
            }      

            return StatusCode(200);
        }
    }
}