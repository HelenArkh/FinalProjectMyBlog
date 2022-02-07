using API.Contracts;
using API.Contracts.Tags;
using AutoMapper;
using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        // Ссылка на объект конфигурации
        private CommentsRepository _comments;
        private PublicationsRepository _publications;
        private IMapper _mapper;
        private readonly UserManager<User> _userManager;

        // Инициализация конфигурации при вызове конструктора
        public CommentController(UserManager<User> userManager, CommentsRepository comments,
            PublicationsRepository publications, IMapper mapper)
        {
            _comments = comments;
            _mapper = mapper;
            _userManager = userManager;
            _publications = publications;
        }

        /// <summary>
        /// Добавление нового комментария
        /// </summary>
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> AddComment([FromRoute] string id, [FromBody] AddCommentRequest request)
        {
            var newComment = _mapper.Map<AddCommentRequest, Comment>(request);

            var user = await _userManager.FindByIdAsync(id);

            _comments.AddComment(user, newComment);

            return StatusCode(201, $"Комментарий {request.Text} добавлен");
        }

        /// <summary>
        /// Просмотр всех комментариев в статье
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCommentsByPublication([FromRoute] string id)
        {
            var publication = _publications.GetPublicationsById(id);
                  
            var comments = _comments.GetCommentsByPublication(publication)
                .ToArray();

            var resp = new GetCommentsResponse
            {
                CommentAmount = comments.Length,
                Comments = _mapper.Map<Comment[], CommentView[]>(comments)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Просмотр комментария по его Id
        /// </summary>
        [HttpGet]
        [Route("GetCommentById/{id}")]
        public IActionResult GetCommentById([FromRoute] string id)
        {

            var comment = _comments.GetCommentsById(id);

            var resp = new 
            {               
                Comment = _mapper.Map<Comment, CommentView>(comment)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Обновление существующего комментария
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public IActionResult EditComment(
            [FromRoute] string id,
            [FromBody] EditCommentRequest request)
        {
            var comment = _comments.GetCommentsById(id);
            if (comment == null)
                return StatusCode(400, $"Ошибка: Комментария с идентификатором {id} не существует.");

            _comments.UpdateComment(
                comment,
                new UpdateCommentQuery(request.Text)
            );

            return StatusCode(200, $"Комментарий обновлен!  Новый комментарий — {comment.Text}");
        }

        /// <summary>
        /// Удаление существующего комментария
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteComment(
            [FromRoute] string id)
        {
            var comment = _comments.GetCommentsById(id);
            if (comment == null)
                return StatusCode(400, $"Ошибка: Комментария с идентификатором {id} не существует.");

            _comments.DeleteComment(
                comment
            );

            return StatusCode(200, $"Комментарий удален!");
        }
    }
}
