﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OnlineEdu.WebUI.DTOs.LoginDtos;
using OnlineEdu.WebUI.DTOs.UserDtos;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.UserServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineEdu.WebUI.Controllers
{
    public class LoginController: Controller
    {

        private readonly HttpClient _client  = HttpClientInstance.CreateClient();
        public IActionResult SignIn()
        {
         return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginDto _userLoginDto)
        {
            var result = await _client.PostAsJsonAsync("users/login", _userLoginDto);
            if (!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
                return View(_userLoginDto);

            }
            var handler = new JwtSecurityTokenHandler();
            var response = await result.Content.ReadFromJsonAsync<LoginResponseDto>();
            var token = handler.ReadJwtToken(response.Token);
            var claims = token.Claims.ToList();

            if (response.Token != null)
            {
                claims.Add(new Claim("Token", response.Token));
                var claimsIdentity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);
                var authProps = new AuthenticationProperties
                {
                    ExpiresUtc=response.ExpireDate,
                    IsPersistent = true,

                };
                await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProps);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
            return View(_userLoginDto);


        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();   
            return RedirectToAction("Index", "Home");

        }

    }
}
