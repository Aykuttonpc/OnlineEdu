using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using OnlineEdu.DTO.DTOs.RoleDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Mapping
{
    public class RoleMapping:Profile
    {
        public RoleMapping()
        {
            CreateMap<AppRole, CreateRoleDto>().ReverseMap();

        }
    }
}
