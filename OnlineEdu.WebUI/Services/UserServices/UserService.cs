﻿    using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAcces.Context;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.UserDtos;
using OnlineEdu.WebUI.Models;

namespace OnlineEdu.WebUI.Services.UserServices
{
    public class UserService : IUserService
    {
  
        private readonly HttpClient _client;

        // Constructor Injection
        public UserService(IHttpClientFactory clientFactory)
        {
        
            _client = clientFactory.CreateClient("EduClient");
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            var user = new AppUser
            {
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
            };

            // Şifre doğrulaması
            if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Passwords do not match" });
            }

            // Kullanıcıyı oluşturma
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Student");
                return result;
            }
            return result;
        }

        public Task<bool> AssignRoleAsync(List<AssignRoleDto> assignRoleDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateRoleAsync(UserRoleDto userRoleDto)
        {
            throw new NotImplementedException();
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            
        }

        async Task<string> IUserService.LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }

            // Check roles sequentially
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return "Admin";
            }
            else if (await _userManager.IsInRoleAsync(user, "Teacher"))
            {
                return "Teacher";
            }
            else if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                return "Student";
            }

            // Fallback if no role matches
            return null;
        }

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            return await _client.GetFromJsonAsync<List<UserViewModel>>("roleAssigns");
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ResultUserDto>> Get4Teachers()
        {
            var user = await _userManager.Users.Include(x => x.TeacherSocials).ToListAsync();
            var teachers = user.Where(user => _userManager.IsInRoleAsync(user,"Teacher").Result).OrderByDescending(x => x.Id).Take(4).ToList();
            return _mapper.Map<List<ResultUserDto>>(teachers);
        }

        public async Task<int> GetTeacherCount()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            return teachers.Count();

        }

        public async Task<List<ResultUserDto>> GetAllTeachers()
        {

            var users = await _userManager.Users.Include(x => x.TeacherSocials).ToListAsync();
            var teachers = users.Where(user => _userManager.IsInRoleAsync(user, "Teacher").Result).ToList();
            return _mapper.Map<List<ResultUserDto>>(teachers);

        }

        public async Task<List<AssignRoleDto>> GetUserForRoleAssign(int id)
        {
            return await _client.GetFromJsonAsync<List<AssignRoleDto>>("roleAssigns/" + id);

        }
    }
}
