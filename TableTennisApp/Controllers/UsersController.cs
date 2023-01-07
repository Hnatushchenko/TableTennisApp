using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableTennisApp.Data.Constants;
using TableTennisApp.Data.ViewModels;
using TableTennisApp.Models;
using TableTennisApp.Services;

namespace TableTennisApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        public UsersController(ApplicationUserManager userManager)
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
            var model = await _userManager.GetUserDetailsByIdAsync(id);

            if (model is null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [Route("Users/Edit/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var model = await _userManager.GetUserDetailsByIdAsync(id);
            
            if (model is null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDetailsVM updatedUser)
        {
            if (ModelState.IsValid == false)
            {
                return View(updatedUser);
            }

            var existingUser = await _userManager.FindByIdAsync(updatedUser.Id.ToString());
            existingUser.UserName = updatedUser.UserName;
            existingUser.TotalNumberOfGames = updatedUser.TotalNumberOfGames;
            existingUser.Rating = updatedUser.Rating;
            existingUser.Email = updatedUser.Email;
            var result = await _userManager.UpdateAsync(existingUser);

            if (result.Succeeded == false)
            {
                TempData["Errors"] = result.Errors.Select(error => error.Description);
                return View(updatedUser);
            }

            await _userManager.RemoveFromRolesAsync(existingUser, UserRoles.AllRoles);
            foreach (string role in updatedUser.Roles)
            {
                await _userManager.AddToRoleAsync(existingUser, role);
            }
            return RedirectToAction(nameof(Index));
        }

        [Route("Users/Delete/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return View("NotFound");
            }

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
