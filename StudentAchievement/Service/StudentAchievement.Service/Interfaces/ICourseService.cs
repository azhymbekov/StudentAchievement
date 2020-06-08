using StudentAchievement.Data.Domain.Entities;
using StudentAchievement.Service.Models;
using StudentAchievement.Service.Models.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Interfaces
{
    public interface ICourseService : IBaseService<Course>
    {
        IQueryable<CourseDto> GetList();

        Task<OperationResult> SaveAsync(CourseDto model, bool isForEdit);        

        Task<CourseDto> Get(Guid Id);

        Task<CourseDto> PrepareForEditView(Guid? id);

        Task<StudentCourses> GetCourseToStudentAsync(Guid Id);

        Task <OperationResult> AppointCourseToStudent(StudentsForDisplay model);
    }
}
