using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Data.Domain.Interfaces;
using StudentAchievement.Models;
using StudentAchievement.Service.Interfaces;
using StudentAchievement.Service.Models;

namespace StudentAchievement.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserAdministration applicationUserAdministration;
        private readonly IRepository<ApplicationRole> roleRepository;

        public ApplicationUserController(
             IApplicationUserAdministration applicationUserAdministration,
             IRepository<ApplicationRole> roleRepository)
        {
            this.applicationUserAdministration = applicationUserAdministration;
            this.roleRepository = roleRepository;
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<ItemViewModel> roles = new List<ItemViewModel>();
            roles = roleRepository.All().Select(x=>
            new ItemViewModel
            {
                RoleId = x.Id,
                Name = x.Name
            }).ToList();
            ViewBag.Name = roles;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            try
            {
                var result = await this.applicationUserAdministration.CreateAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetListOfStudents");
                }

                StringBuilder builder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }

                this.ModelState.AddModelError(nameof(UserModel.Login), builder.ToString());
                var errorList = (from item in this.ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToList();
                return this.Json(new { Success = false, Errors = errorList });
            }
            catch (InvalidOperationException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var errorList = (from item in this.ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToList();
                return this.View();
            }
        }

        [HttpGet]
        public IActionResult GetListOfStudents()
        {
            var students = this.applicationUserAdministration.GetListOfUsers();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id)
        {
            var studentInfo = await applicationUserAdministration.UserInfo(id);
            return this.View(studentInfo);
        }

    }
}