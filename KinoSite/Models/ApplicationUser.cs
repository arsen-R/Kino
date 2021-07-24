using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using KinoSite.Models.Comments;
namespace KinoSite.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
