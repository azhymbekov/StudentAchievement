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
    public interface IGroupService : IBaseService<Group>
    {
        IQueryable<GroupDto> GetList();

        Task<OperationResult> SaveAsync(GroupDto model);

        Task<GroupDto> PrepareForEditView(Guid? id);

        Task<GroupDto> Get(Guid Id);
    }
}
