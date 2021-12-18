using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Data.Repository
{
    public class TagsRepository : Repository<Tag>
    {
        private readonly ApplicationDbContext _db;
        public TagsRepository(ApplicationDbContext db)
    : base(db)
        {
            db = _db;
        }

        public void AddTag(Tag tag)
        {
            var ids = Guid.NewGuid().ToString();

            var item = new Tag()
            {
                Id = ids,
                TagName = tag.TagName,
                CurrentTagId = ids,
            };

            Create(item);
        }

        public Tag GetTagsById(string id)
        {
            var tag = Set.AsEnumerable().FirstOrDefault(x => x.Id == id);

            return tag;
        }

        public List<Tag> GetAllTags()
        {
            var tags = GetAll().ToList();

            return tags;
        }

        public void UpdateTag(Tag tag, UpdateTagQuery query)
        {
            if (!string.IsNullOrEmpty(query.NewTagName))
                tag.TagName = query.NewTagName;

            Update(tag);
        }

        public void DeleteTag(Tag item)
        {
            var tag = Set.AsEnumerable().FirstOrDefault(x => x.Id == item.Id);

            if (tag != null)
            {
                Delete(tag);
            }
        }
    }
}
