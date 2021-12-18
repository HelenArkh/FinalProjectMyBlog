using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Data.Repository
{
    public class PublicationsRepository : Repository<Publication>
    {
        private readonly ApplicationDbContext _db;
        public PublicationsRepository(ApplicationDbContext db)
    : base(db)
        {
            db = _db;
        }

        public void AddPublication(User user, Publication publicaton)
        {
            var ids = Guid.NewGuid().ToString();

            var item = new Publication()
            {
                Id = ids,
                UserId = user.Id,
                UserName = user.FirstName + " "+ user.LastName,
                Title = publicaton.Title,
                Text = publicaton.Text,
                Date = DateTime.Now,
                CurrentPublicationId = ids,
            };

                Create(item);
        }

        public List<Publication> GetPublicationsByUser(User target)
        {
            var publications = Set.Include(x => x.CurrentPublication).AsEnumerable().Where(x => x.User?.Id == target.Id).Select(x => x.CurrentPublication);

            return publications.ToList();
        }

        public Publication GetPublicationsById(string id)
        {
            var publication = Set.AsEnumerable().FirstOrDefault(x => x.Id == id);

            return publication;
        }

        public List<Publication> GetAllPublications()
        {
            var publications = GetAll().ToList();

            return publications;
        }

        /// <summary>
        /// Редактировать публикацию
        /// </summary>
        public void UpdatePublication(Publication publication, UpdatePublicationQuery query)
        {
            if (!string.IsNullOrEmpty(query.NewTitle))
                publication.Title = query.NewTitle;
            if (!string.IsNullOrEmpty(query.NewText))
                publication.Text = query.NewText;

            Update(publication);
        }

        public void DeletePublication(Publication item)
        {
            var publication = Set.AsEnumerable().FirstOrDefault(x => x.Id == item.Id);

            if (publication != null)
            {
                Delete(publication);
            }
        }
    }
}
