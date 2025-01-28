using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.UserDtos;

namespace OnlineEdu.WebUI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        // Constructor Injection
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            return await _userManager.CreateAsync(user, userRegisterDto.Password);
        }

        public Task<bool> AssignRoleAsync(List <AssignRoleDto> assignRoleDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateRoleAsync(UserRoleDto userRoleDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginAsync(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogOutAsync()
        {
            throw new NotImplementedException();
        }

        Task<string> IUserService.LoginAsync(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            return await  _userManager.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync( x => x.Id == id);
        }
    }
}
