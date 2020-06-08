using AutoMapper;
using StudentAchievement.Data.Domain.Entities;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Data.Domain.Interfaces;
using StudentAchievement.Service.Interfaces;
using StudentAchievement.Service.Models;
using StudentAchievement.Service.Models.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Services
{
    public class CourseService : BaseService<Course>, ICourseService
    {
        public readonly IMapper _map;
        public CourseService(IUnitOfWork uow, IMapper map) : base(uow)
        {
            _map = map;
        }

        public async Task<OperationResult> AppointCourseToStudent(StudentsForDisplay model)
        {
            var result = new OperationResult();
            try
            {
                var studentsCourses = _uow.GetRepository<CourseUser>().Where(x => x.UserId == model.Id);
                _uow.GetRepository<CourseUser>().Remove(studentsCourses);
                if (model.Courses != null)
                {
                    foreach (var courseId in model.Courses)
                    {
                        _uow.GetRepository<CourseUser>().Add(new CourseUser
                        {
                            UserId = model.Id,
                            CourseId = courseId
                        });
                    }
                }

                await _uow.CommitAsync();
                result.Succeeded = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public async Task<CourseDto> Get(Guid Id)
        {
            var courseInfo = await _uow.GetRepository<Course>().FindByIdAsync(Id);
            return _map.Map<CourseDto>(courseInfo);
        }

        public async Task<StudentCourses> GetCourseToStudentAsync(Guid studentId)
        {
            var student = await GetStudentAsync(studentId);
            var model = new StudentCourses
            {
                Student = new StudentsForDisplay { Id = student.Id, Name = student.FirstName },
                CurrentCourseIds = _uow.GetRepository<CourseUser>().Where(x => x.UserId == studentId).Select(x => x.CourseId).ToArray(),
                Courses = GetList().ToDictionary(x => x.Id, x => x.Name)
            };

            return model;
        }

        private async Task<UserModel> GetStudentAsync(Guid studentId)
        {
            var userInfo = await _uow.GetRepository<ApplicationUser>().FindByIdAsync(studentId);
            return _map.Map<UserModel>(userInfo);
        }

        public IQueryable<CourseDto> GetList()
        {
            return _map.ProjectTo<CourseDto>(_uow.GetRepository<Course>().All().OrderBy(x => x.Name));
        }

        public async Task<CourseDto> PrepareForEditView(Guid? id)
        {
            var course = await _uow.GetRepository<Course>().FindByIdAsync(id);
            return _map.Map<CourseDto>(course);
        }



        public override void Remove(Guid id)
        {
            var entity = _uow.GetRepository<Course>().FindById(id);
            Remove(entity);
        }

        public async Task<OperationResult> SaveAsync(CourseDto model , bool isForEdit)
        {
            var result = new OperationResult();

            var course = _uow.GetRepository<Course>().FindById(model.Id);
            var courseByName = _uow.GetRepository<Course>().All().FirstOrDefault(x=>x.Name==model.Name);
            if (course == null && !isForEdit && courseByName == null)
            {
                var newCourse = _map.Map<Course>(model);
                _uow.GetRepository<Course>().Add(newCourse);
                result.Succeeded = true;
            }
            else if(course != null && isForEdit)
            {
                _map.Map(model, course);
                _uow.GetRepository<Course>().Update(course);
                result.Succeeded = true;
            }
            else
            {
                result.Message = "Такой предмет уже существует";
            }

             await _uow.CommitAsync();

            return result;
        }
    }
}
