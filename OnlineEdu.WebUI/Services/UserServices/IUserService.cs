    using Microsoft.AspNetCore.Identity;
    using OnlineEdu.Entity.Entities;
    using OnlineEdu.WebUI.DTOs.UserDtos;
    using OnlineEdu.WebUI.Models;

namespace OnlineEdu.WebUI.Services.UserServices
    {
        public interface IUserService
        {
        Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto);
        Task<string> LoginAsync(UserLoginDto userLoginDto);
        Task LogOutAsync();
        Task<bool> CreateRoleAsync(UserRoleDto userRoleDto);
        Task<bool> AssignRoleAsync(List<AssignRoleDto> assignRoleDto);
        Task <List<UserViewModel>> GetAllUsersAsync();
        Task <AppUser> GetUserByIdAsync(int id);
        Task <List<AssignRoleDto>> GetUserForRoleAssign(int id);
        Task<List<ResultUserDto>> Get4Teachers();
        Task<int> GetTeacherCount();
        Task<List<ResultUserDto>> GetAllTeachers(); 
        
            




        }
    }
