using AutoMapper;
using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Account;
using FinalProjectMyBlog.ViewModels.Comments;
using FinalProjectMyBlog.ViewModels.Publications;
using FinalProjectMyBlog.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                 .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
                 .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
                 .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
            CreateMap<LoginViewModel, User>();

            CreateMap<PublicationCreateViewModel, Publication>();
            CreateMap<Publication, PublicationCreateViewModel>();

            CreateMap<PublicationEditViewModel, Publication>();
            CreateMap<Publication, PublicationEditViewModel>();

            CreateMap<CommentEditViewModel, Comment>();
            CreateMap<Comment, CommentEditViewModel>();

            CreateMap<TagEditViewModel, Tag>();
            CreateMap<Tag, TagEditViewModel>();

            CreateMap<UserWithFriendExt, User>();
            CreateMap<User, UserWithFriendExt>();
        }
    }
}
