using AutoMapper;
using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Data.UoW;
using FinalProjectMyBlog.Extentions;
using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Account;
using FinalProjectMyBlog.ViewModels.Publications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Controllers.Publications
{
    public class PublicationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PublicationController(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PublicationCreateViewModel();

            model.Tags = await GetAllTag();

            return View("Create", model);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(PublicationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentuser = User;

                var publication = new Publication();

                //сюда нужно добавить сохранение тегов, выбранных чекбоксами

                publication.Convert(model);

                var user = await _userManager.GetUserAsync(currentuser);

                var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

                repository.AddPublication(user, publication);

                    return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("Create", model);
            }
        }

        [Route("EditPublication")]
        [HttpPost]
        public async Task<IActionResult> EditPublication(string id)
        {
            var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

            var result = repository.GetPublicationsById(id);

            var editmodel = _mapper.Map<PublicationEditViewModel>(result);

            editmodel.Tags = await GetAllTag();

            return View("EditPublication", editmodel);
        }

        [Route("UpdatePublication")]
        [HttpPost]
        public IActionResult UpdatePublication(
            PublicationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

                var publication = repository.GetPublicationsById(model.Id);

                repository.UpdatePublication(
                    publication,
                    new UpdatePublicationQuery(model.Title, model.Text)
                );

                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("Edit", model);
            }
        }

        [Route("DeletePublication")]
        [HttpPost]
        public async Task<IActionResult> DeletePublication(string id)
        {
            var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

            var publication = repository.GetPublicationsById(id);

            repository.DeletePublication(publication);

            return RedirectToAction("MyPage", "AccountManager");

        }

        [Route("GetAllPublications")]
        [HttpGet]
        public IActionResult GetAllPublications()
        {
            var repository = _unitOfWork.GetRepository<Publication>() as PublicationsRepository;

            var model = new PublicationViewModel();

            model.Publications = repository.GetAllPublications();

            return View("GetAllPublications", model);
        }

        private async Task<List<Tag>> GetAllTag()
        {
            var repository = _unitOfWork.GetRepository<Tag>() as TagsRepository;

            return repository.GetAllTags();
        }
    }
}
