using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TableTennisApp.Data.ViewModels;
using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<UserDetailsVM?> GetUserDetailsByIdAsync(Guid id)
        {
            var user = await Users.SingleOrDefaultAsync(user => user.Id == id);
            if (user is null)
            {
                return null;
            }

            var model = new UserDetailsVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = (await GetRolesAsync(user)).ToArray(),
                Rating = user.Rating,
                TotalNumberOfGames = user.TotalNumberOfGames,
            };
            return model;
        }
    }
}
