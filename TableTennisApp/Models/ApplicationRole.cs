using Microsoft.AspNetCore.Identity;

namespace TableTennisApp.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
