using API.Contracts;
using API.Contracts.Friends;
using API.Contracts.Publication;
using API.Contracts.Tags;
using AutoMapper;
using FinalProjectMyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    /// <summary>
    /// Настройки маппинга всех сущностей приложения
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// В конструкторе настроим соответствие сущностей при маппинге
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Comment, AllcommentsResponse>();
            CreateMap<Tag, TagView>();
            CreateMap<Comment, CommentView>();
            CreateMap <User, FriendView>();
            CreateMap<AddTagRequest, Tag>();
            CreateMap<AddCommentRequest, Comment>();
            CreateMap<AddPublicationRequest, Publication>();
            CreateMap<AddFriendRequest, User>();
            CreateMap<Publication, PublicationView>();
        }
           
    }
}
