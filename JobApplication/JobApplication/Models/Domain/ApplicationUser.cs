using Microsoft.AspNetCore.Identity;

namespace JobApplication.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
