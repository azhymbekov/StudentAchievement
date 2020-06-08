using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentAchievement.Service.Interfaces;
using StudentAchievement.Claims;
using StudentAchievement.Models;

namespace StudentAchievement.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }
        public IActionResult Index()
        {
            var userViewModel = this.profileService.GetProfile(this.User.GetUserId());
            return this.View(userViewModel);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var passwordViewModel = new ChangePasswordViewModel();
            return this.View(passwordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.profileService.ChangePasswordAsync(this.User.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return this.RedirectToAction(nameof(ProfileController.Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(error.Code, error.Description);
                }

                return this.View(model);
            }
        }
    }
}