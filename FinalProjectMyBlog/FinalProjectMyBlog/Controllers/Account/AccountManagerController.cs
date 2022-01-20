using AutoMapper;
using FinalProjectMyBlog.Data;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Data.UoW;
using FinalProjectMyBlog.Extentions;
using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Controllers.Account
{
    public class AccountManagerController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IUnitOfWork _unitOfWork;

        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Home/Login");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = _mapper.Map<User>(model);

                var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {                      
                        return Redirect(model.ReturnUrl);                       
                    }
                    else
                    {
                        Program.Logger.Info($"Пользователь {user.Email} вошел на свою страницу"); 
                        return RedirectToAction("MyPage", "AccountManager");                       
                    }
                }
                else
                {
                    Program.Logger.Info($"Пользователь ввел неправильный логин {model.Email} или пароль {model.Password}");
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            else
            {
                Program.Logger.Info($"Пользователь ввел неправильный логин {model.Email} или пароль {model.Password}. Модель не валидна");
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                return View("Login", model);
            }
            
            return RedirectToAction("Index", "Home");           
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Program.Logger.Info($"Пользователь разлогинился");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPage()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var model = new UserViewModel(result);

            model.Friends = await GetAllFriend(model.User);
            model.Publications = await GetAllPublication(model.User);

            return View("User", model);
        }

        private async Task<List<User>> GetAllFriend(User user)
        {
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriendsByUser(user);
        }

        private async Task<List<User>> GetAllFriend()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriendsByUser(result);
        }

        private async Task<List<Publication>> GetAllPublication(User user)
        {
            var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

            return repository.GetPublicationsByUser(user);
        }

        private async Task<List<Publication>> GetAllPublication()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

            return repository.GetPublicationsByUser(result);
        }

        [Authorize]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                user.Convert(model);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    Program.Logger.Info($"Пользователь {user.Email} отредактировал свой профиль");
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    Program.Logger.Info($"Что-то пошло не так при редактировании профиля пользователя {user.Email}");
                    return RedirectToAction("Edit", "AccountManager");
                }
            }
            else
            {
                Program.Logger.Info($"Пользователь {model.Email} ввел некорректные данные при редактировании профиля");
                ModelState.AddModelError("", "Некорректные данные");
                return View("Edit", model);
            }
        }

        [Route("UserList")]
        [HttpGet]
        public async Task<IActionResult> UserList(string search)
        {
            var model = await CreateSearch(search);
            return View("UserList", model);
        }

        [Route("AddFriend")]
        [HttpPost]
        public async Task<IActionResult> AddFriend(string id)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var friend = await _userManager.FindByIdAsync(id);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            repository.AddFriend(result, friend);

            Program.Logger.Info($"Пользователь {result.Email} добавил в друзья {friend.Email}");

            return RedirectToAction("MyPage", "AccountManager");

        }

        [Route("DeleteFriend")]
        [HttpPost]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var friend = await _userManager.FindByIdAsync(id);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            repository.DeleteFriend(result, friend);

            Program.Logger.Info($"Пользователь {result.Email} удалил из друзей {friend.Email}");

            return RedirectToAction("MyPage", "AccountManager");
        }

        [Route("DeleteUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            string userLog = user.Email;

            var result = await _userManager.DeleteAsync(user);

            Program.Logger.Info($"Пользователь {userLog} удален");

            return RedirectToAction("MyPage", "AccountManager");
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit()
        {
            var user = User;

            var result = _userManager.GetUserAsync(user);

            var editmodel = _mapper.Map<UserEditViewModel>(result.Result);

            return View("Edit", editmodel);
        }

        private async Task<SearchViewModel> CreateSearch(string search)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var list = _userManager.Users.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(search.ToLower())).ToList();
            var withfriend = await GetAllFriend();

            var data = new List<UserWithFriendExt>();
            list.ForEach(x =>
            {
                var t = _mapper.Map<UserWithFriendExt>(x);
                t.IsFriendWithCurrent = withfriend.Where(y => y.Id == x.Id || x.Id == result.Id).Count() != 0;
                data.Add(t);
            });

            var model = new SearchViewModel()
            {
                UserList = data
            };

            return model;
        }        

        [Route("Generate")]
        [HttpGet]
        public async Task<IActionResult> Generate()
        {

            var usergen = new GenetateUsers();
            var userlist = usergen.Populate(35);

            foreach (var user in userlist)
            {
                var result = await _userManager.CreateAsync(user, "123456");

                if (!result.Succeeded)
                    continue;
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
