using AutoMapper;
using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Data.UoW;
using FinalProjectMyBlog.Extentions;
using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Comments;
using FinalProjectMyBlog.ViewModels.Tags;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Controllers.Comments
{
    public class TagController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public TagController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [Route("AddTag")]
        [HttpPost]
        public IActionResult AddTag(string id)
        {
            ViewBag.Id = id;

            var repository = _unitOfWork.GetRepository<Tag>() as TagsRepository;

            var model = new TagViewModel();

            model.Tags = repository.GetAllTags();

            return View("AddTag", model);
        }

        [Route("CreateTag")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View("CreateTag");
        }

        [Route("CreateTag")]
        [HttpPost]
        public async Task<IActionResult> CreateTag(TagCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag();

                tag.Convert(model);

                var repository = _unitOfWork.GetRepository<Tag>() as TagsRepository;

                repository.AddTag(tag);

                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("CreateTag", model);
            }
        }

        [Route("EditTag")]
        [HttpPost]
        public IActionResult EditTag(string id)
        {
            var repository = _unitOfWork.GetRepository<Tag>() as TagsRepository;

            var result = repository.GetTagsById(id);

            var editmodel = _mapper.Map<TagEditViewModel>(result);

            return View("EditTag", editmodel);
        }

        [Route("UpdateTag")]
        [HttpPost]
        public IActionResult UpdateTag(
           TagEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repository = _unitOfWork.GetRepository<Tag>() as TagsRepository;

                var tag = repository.GetTagsById(model.Id);

                repository.UpdateTag(
                    tag,
                    new UpdateTagQuery(model.TagName)
                );

                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("EditTag", model);
            }
        }

        [Route("DeleteTag")]
        [HttpPost]
        public async Task<IActionResult> DeleteTag(string id)
        {
            var repository = _unitOfWork.GetRepository<Tag>() as TagsRepository;

            var tag = repository.GetTagsById(id);

            repository.DeleteTag(tag);

            return RedirectToAction("MyPage", "AccountManager");
        }
    }
}
