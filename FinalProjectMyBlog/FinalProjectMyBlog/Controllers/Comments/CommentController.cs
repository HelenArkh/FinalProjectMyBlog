using AutoMapper;
using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Data.UoW;
using FinalProjectMyBlog.Extentions;
using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Comments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Controllers.Comments
{
    public class CommentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CommentController(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("CreateComment")]
        [HttpPost]
        public IActionResult CreateComment(string id)
        {
            //ViewBag.Id = id;

            var model = new CommentCreateViewModel();

            model.PublicationId = id;

            return View("CreateComment", model);
        }

        [Route("SaveComment")]
        [HttpPost]
        public async Task<IActionResult> SaveComment(CommentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentuser = User;

                var comment = new Comment();

                comment.Convert(model);

                var user = await _userManager.GetUserAsync(currentuser);

                var repository = _unitOfWork.GetRepository<Comment>() as CommentsRepository;

                repository.AddComment(user, comment);

                Program.Logger.Info($"Пользователь {user.Email} добавил комментарий {comment.Text}");

                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                Program.Logger.Info($"Некорректные данные при добавлении пользователем комментария");
                return View("CreateComment", model);
            }
        }

        [Route("ReadComment")]
        [HttpPost]
        public IActionResult ReadComment(string id)
        {           
            var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

            var publication = repository.GetPublicationsById(id);

            var model = new ReadCommentViewModel(publication);

            model.Comments = GetAllComment(model.Publication);

            return View("ReadComments", model);
        }

        private List<Comment> GetAllComment(Publication publication)
        {
            var repository = _unitOfWork.GetRepository<Comment>() as CommentsRepository;

            return repository.GetCommentsByPublication(publication);
        }

        [Route("EditComment")]
        [HttpPost]
        public IActionResult EditComment(string id)
        {
            var repository = _unitOfWork.GetRepository<Comment>() as CommentsRepository;

            var result = repository.GetCommentsById(id);

            var editmodel = _mapper.Map<CommentEditViewModel>(result);

            return View("EditComment", editmodel);
        }

        [Route("UpdateComment")]
        [HttpPost]
        public IActionResult UpdateComment(
            CommentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repository = _unitOfWork.GetRepository<Comment>() as CommentsRepository;

                var comment = repository.GetCommentsById(model.Id);

                repository.UpdateComment(
                    comment,
                    new UpdateCommentQuery(model.Text)
                );

                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("EditComment", model);
            }
        }

        [Route("DeleteComment")]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var repository = _unitOfWork.GetRepository<Comment>() as CommentsRepository;

            var comment = repository.GetCommentsById(id);

            repository.DeleteComment(comment);

            return RedirectToAction("MyPage", "AccountManager");

        }
    }
}
