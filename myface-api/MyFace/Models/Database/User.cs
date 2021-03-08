using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFace.Models.Database
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string Hashed_password {get; set; }
        public byte[] Salt { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
    }
}