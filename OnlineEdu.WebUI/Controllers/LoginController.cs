using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OnlineEdu.WebUI.DTOs.UserDtos;
using OnlineEdu.WebUI.Services.UserServices;

namespace OnlineEdu.WebUI.Controllers
{
    public class LoginController(IUserService _userService) : Controller
    {
        public IActionResult SignIn()
        {
         return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginDto _userLoginDto)
        {
            var userRole = await _userService.LoginAsync(_userLoginDto);

            if (userRole == "Admin")
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            else if (userRole == "Student")
            {
                return RedirectToAction("Index", "CourseRegister", new { area = "Student" });
            }
            else if (userRole == "Teacher")
            {
                return RedirectToAction("Index", "MyCourse", new { area = "Teacher" });
            }
            
            else
            {
                ModelState.AddModelError("", "Email veya Şifre Hatalı");
                return View();
            }
        }
    }
    
}
