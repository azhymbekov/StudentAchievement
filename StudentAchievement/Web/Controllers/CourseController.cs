using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentAchievement.Service.Interfaces;
using StudentAchievement.Service.Models;
using StudentAchievement.Service.Models.OperationResult;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAchievement.Controllers
{
    [Authorize(Policy = "ReadAccess")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: Courses
        public ActionResult Index()
        {
            var model =_courseService.GetList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SaveData(Guid? id)
        {
            if (id.HasValue)
            {
                var course = await _courseService.PrepareForEditView(id);
                return View(course);
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveData(CourseDto model)
        {
            OperationResult result;
            if(model.Id == Guid.Empty)
            {
                 result = await _courseService.SaveAsync(model, false);
            }
            else
            {
                 result = await _courseService.SaveAsync(model, true);
            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Course");
            }
            ModelState.Clear();

            this.ModelState.AddModelError("CreateIsFailed", "Такой предмет уже существует");
            return this.View(model);
        }

        public IActionResult Delete(Guid id)
        {
             _courseService.Remove(id);
            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var details = await _courseService.Get(Id);
            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> AppointSubject(Guid id)
        {
            var courseStudent = await _courseService.GetCourseToStudentAsync(id);
            ViewBag.Courses = courseStudent.Courses.Select(x => new SelectListItem(x.Value, x.Key.ToString(), courseStudent.CurrentCourseIds.Contains(x.Key)));
            return View(courseStudent.Student);
        }

        [HttpPost]
        public async Task<IActionResult> AppointSubject(StudentsForDisplay model)
        {
            StudentCourses studentCourses;
            if (!ModelState.IsValid)
            {
                studentCourses = await _courseService.GetCourseToStudentAsync(model.Id);
                ViewBag.Courses = studentCourses.Courses.Select(x => new SelectListItem(x.Value, x.Key.ToString(), studentCourses.CurrentCourseIds.Contains(x.Key)));
                return View(model);
            }

            var result = await _courseService.AppointCourseToStudent(model);
            if (result.Succeeded)
            {
                return RedirectToAction("GetListOfStudents", "ApplicationUser");
            }

            studentCourses = await _courseService.GetCourseToStudentAsync(model.Id);
            ViewBag.Courses = studentCourses.Courses.Select(x => new SelectListItem(x.Value, x.Key.ToString(), studentCourses.CurrentCourseIds.Contains(x.Key)));
            
            return View(model);
        }

    }
}
