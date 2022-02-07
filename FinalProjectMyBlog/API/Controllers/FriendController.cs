using API.Contracts.Friends;
using AutoMapper;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendController : ControllerBase
    {
        private FriendsRepository _friends;
        private IMapper _mapper;
        private readonly UserManager<User> _userManager;

        // Инициализация конфигурации при вызове конструктора
        public FriendController(UserManager<User> userManager, FriendsRepository friends,
            IMapper mapper)
        {
            _friends = friends;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Добавление друга
        /// </summary>
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> AddFriend([FromRoute] string id, [FromBody] AddFriendRequest request)
        {
            var newFriend = await _userManager.FindByIdAsync(request.Id);

            var user = await _userManager.FindByIdAsync(id);

            _friends.AddFriend(user, newFriend);

            return StatusCode(201, $"Друг {newFriend.UserName} добавлен");
        }

        /// <summary>
        /// Просмотр всех друзей пользователя
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFriendsByUser([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var friends = _friends.GetFriendsByUser(user)
                .ToArray();

            var resp = new GetFriendsResponse
            {
                FriendAmount = friends.Length,
                Friends = _mapper.Map<User[], FriendView[]>(friends)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Удаление из друзей
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment(
            [FromRoute] string id, [FromBody] DeleteFriendRequest request)
        {
            var deleteFriend = await _userManager.FindByIdAsync(request.Id);

            var user = await _userManager.FindByIdAsync(id);

            if (deleteFriend == null)
                return StatusCode(400, $"Ошибка: Друга с идентификатором {id} не существует.");

            _friends.DeleteFriend(user,
                deleteFriend
            );

            return StatusCode(200, $"Друг {deleteFriend.Id} удален!");
        }
    }
}
