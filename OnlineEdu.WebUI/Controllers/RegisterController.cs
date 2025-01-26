using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDtos;
using OnlineEdu.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.WebUI.Controllers
{
    public class RegisterController(IUserService _userService) : Controller
    {

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterDto userRegisterDto)
        {
            var result = await _userService.CreateUserAsync(userRegisterDto);
            if (!result.Succeeded || !ModelState.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Index", "Login"); // Add a return statement for successful registration
        }
    }
}
