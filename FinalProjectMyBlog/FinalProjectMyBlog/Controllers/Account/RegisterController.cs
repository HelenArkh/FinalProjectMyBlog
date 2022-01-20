using AutoMapper;
using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Controllers.Account
{
    public class RegisterController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegisterController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Home/Register");
        }

        [Route("RegisterPart2")]
        [HttpGet]
        public IActionResult RegisterPart2(RegisterViewModel model)
        {
            return View("RegisterPart2", model);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);

                var result = await _userManager.CreateAsync(user, model.PasswordReg);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Пользователь");
                    await _signInManager.SignInAsync(user, false);
                    //await Authenticate(user);
                    Program.Logger.Info($"Зарегистрирован новый пользователь {user.Email}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Program.Logger.Info($"При регистрации пользователя найдены ошибки: {error.Description}");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            
            Program.Logger.Info($"Ошибки в модели при регистрации пользователя");
            return View("RegisterPart2", model);
        }       
    }
}