using FinalProjectMyBlog.Data;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Extentions;
using FinalProjectMyBlog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Загрузка конфигурации из файла Json
        /// </summary>
        public IConfiguration Configuration { get; } = new ConfigurationBuilder()
          .AddJsonFile("Comment.json")
          .Build();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Подключаем автомаппинг
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            services.AddAutoMapper(assembly);

            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<TagsRepository>()
            .AddSingleton<CommentsRepository>()
            .AddSingleton<PublicationsRepository>()
            .AddSingleton<FriendsRepository>()
            .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

            services
               .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection))
               .AddUnitOfWork()
               .AddIdentity<User, IdentityRole>(opts => {
                   opts.Password.RequiredLength = 5;
                   opts.Password.RequireNonAlphanumeric = false;
                   opts.Password.RequireLowercase = false;
                   opts.Password.RequireUppercase = false;
                   opts.Password.RequireDigit = false;
               })
                   .AddEntityFrameworkStores<ApplicationDbContext>();


            // Нам не нужны представления, но в MVC бы здесь стояло AddControllersWithViews()
            services.AddControllers();
            // поддерживает автоматическую генерацию документации WebApi с использованием Swagger
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
