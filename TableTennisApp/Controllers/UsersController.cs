using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableTennisApp.Data.Constants;
using TableTennisApp.Data.ViewModels;
using TableTennisApp.Models;

namespace TableTennisApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersViewModels = new List<UsersVM>(users.Count);
            for (int i = 0; i < users.Count; i++)
            {
                var userVM = new UsersVM
                {
                    Id = users[i].Id,
                    UserName = users[i].UserName,
                    Email = users[i].Email,
                    Roles = await _userManager.GetRolesAsync(users[i])
                };
                usersViewModels.Add(userVM);
            }
            return View(usersViewModels);
        }

        [Route("Users/Details/{id:guid}")]
        [HttpGet]        
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (user is null)
            {
                // TODO: Create view "NotFound"
                return Json("Not found");
            }

            var model = new UserDetailsVM
            { 
                Id = user.Id,
                UserName = user.UserName,
                Email=user.Email,
                Roles = await _userManager.GetRolesAsync(user),
                Rating = user.Rating,
                TotalNumberOfGames = user.TotalNumberOfGames,
            };

            return View(model);
        }

        [Route("Users/Edit/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (user is null)
            {
                // TODO: Create view "NotFound"
                return Json("Not found");
            }

            var model = new UserDetailsVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user),
                Rating = user.Rating,
                TotalNumberOfGames = user.TotalNumberOfGames,
            };

            return View(model);
        }
    }
}
