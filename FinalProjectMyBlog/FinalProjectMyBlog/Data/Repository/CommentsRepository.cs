using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Data.Repository
{
    public class CommentsRepository : Repository<Comment>
    {
        private readonly ApplicationDbContext _db;
        public CommentsRepository(ApplicationDbContext db)
    : base(db)
        {
            db = _db;
        }

        public void AddComment(User user, Comment comment)
        {
            var ids = Guid.NewGuid().ToString();

            var item = new Comment()
            {
                Id = ids,
                UserId = user.Id,
                UserName = user.FirstName + " " + user.LastName,
                Text = comment.Text,
                Date = DateTime.Now,
                PublicationId = comment.PublicationId,
                CurrentCommentId = ids,
            };

            Create(item);
        }

        public List<Comment> GetCommentsByPublication(Publication target)
        {
            var comments = Set.Include(x => x.CurrentComment).AsEnumerable().Where(x => x.PublicationId == target.Id).Select(x => x.CurrentComment);

            return comments.ToList();
        }

        public Comment GetCommentsById(string id)
        {
            var comment = Set.AsEnumerable().FirstOrDefault(x => x.Id == id);

            return comment;
        }

        //public List<Publication> GetAllPublications()
        //{
        //    var publications = GetAll().ToList();

        //    return publications;
        //}


        public void UpdateComment(Comment comment, UpdateCommentQuery query)
        {
            if (!string.IsNullOrEmpty(query.NewText))
                comment.Text = query.NewText;

            Update(comment);
        }

        public void DeleteComment(Comment item)
        {
            var comment = Set.AsEnumerable().FirstOrDefault(x => x.Id == item.Id);

            if (comment != null)
            {
                Delete(comment);
            }
        }
    }
}
