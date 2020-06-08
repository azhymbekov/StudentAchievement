using AutoMapper;
using StudentAchievement.Data.Domain.Entities;
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
    public class GroupService : BaseService<Group>, IGroupService
    {
        public readonly IMapper _map;

        public GroupService(IUnitOfWork uow, IMapper map) : base(uow)
        {
            _map = map;
        }

        public IQueryable<GroupDto> GetList()
        {
            return _map.ProjectTo<GroupDto>(_uow.GetRepository<Group>().All().OrderBy(x => x.Name));
        }

        public async Task<GroupDto> PrepareForEditView(Guid? id)
        {
            var group = await _uow.GetRepository<Group>().FindByIdAsync(id);
            return _map.Map<GroupDto>(group);
        }

        public async Task<OperationResult> SaveAsync(GroupDto model)
        {
            var result = new OperationResult();

            var group = _uow.GetRepository<Group>().FindById(model.Id);
            if (group == null )
            {
                var newCourse = _map.Map<Group>(model);
                _uow.GetRepository<Group>().Add(newCourse);
                result.Succeeded = true;
            }
            else 
            {
                _map.Map(model, group);
                _uow.GetRepository<Group>().Update(group);
                result.Succeeded = true;
            }         

            await _uow.CommitAsync();

            return result;
        }

        public async Task<GroupDto> Get(Guid Id)
        {
            var info = await _uow.GetRepository<Group>().FindByIdAsync(Id);
            return _map.Map<GroupDto>(info);
        }
    }
}
