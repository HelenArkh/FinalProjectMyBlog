using FinalProjectMyBlog.Configs;
using FinalProjectMyBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Publication> Publications { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<Friend>(new FriendConfiguration());
            builder.ApplyConfiguration<Publication>(new PublicationConfiguration());
            builder.ApplyConfiguration<Comment>(new CommentConfiguration());
            builder.ApplyConfiguration<Tag>(new TagConfiguration());
        }
    }
}
