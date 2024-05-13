using Microsoft.AspNetCore.Identity;

namespace Prism.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;
    }
}
