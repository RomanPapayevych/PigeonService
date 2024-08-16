using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
