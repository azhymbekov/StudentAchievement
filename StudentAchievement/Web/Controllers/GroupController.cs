using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAchievement.Service.Interfaces;
using StudentAchievement.Service.Models;

namespace StudentAchievement.Controllers
{
    [Authorize(Policy = "ReadAccess")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public IActionResult Index()
        {
            var model = _groupService.GetList();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> SaveData(Guid? id)
        {
            if (id.HasValue)
            {
                var course = await _groupService.PrepareForEditView(id);
                return View(course);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var details = await _groupService.Get(Id);
            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> SaveData(string name)
        {
            var model = new GroupDto();
            model.Name = name;
            var result = await _groupService.SaveAsync(model);
         
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Group");
            }
            ModelState.Clear();

            this.ModelState.AddModelError("CreateIsFailed", "Ошибка");
            return this.View(model);
        }
    }
}