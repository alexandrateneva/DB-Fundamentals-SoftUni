using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagraph.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int ProfilePictureId { get; set; }
        public Picture ProfilePicture { get; set; }

        [InverseProperty("User")]
        public ICollection<UserFollower> Followers { get; set; }

        [InverseProperty("Follower")]
        public ICollection<UserFollower> UsersFollowing { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
