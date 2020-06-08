using AutoMapper;
using StudentAchievement.Data.Domain.Entities;
using StudentAchievement.Data.Domain.Entities.Identity;
using StudentAchievement.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAchievement.Service.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(desc => desc.Login)).ReverseMap();
            CreateMap<UserModel, UserForView>().ReverseMap();

            CreateMap<Group, GroupDto>().ReverseMap();
        }
    }
}
