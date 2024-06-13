using FinalProjectDOIT.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectDOIT.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
