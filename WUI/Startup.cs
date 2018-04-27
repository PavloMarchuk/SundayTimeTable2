using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using DbLayer;
using EFCoreGenericRepository.Common;
using EFCoreGenericRepository.Typed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;

namespace WUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.Configure<WebEncoderOptions>(options => 
			{ options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All); });

			//додаємо посилання на провайдера сервера баз даних і додаємо строку підключення
			services.AddDbContext<SundayTimeTableContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("SundayTimeTableConnection")));

			//використовуємо встроєний контейнер створення залежностей
			services.AddScoped<DbContext, SundayTimeTableContext>();
			services.AddTransient<IGenericRepository<Lesson>, LessonRepository>();
			services.AddTransient<IGenericRepository<Ssubject>, SsubjectRepository>();
			services.AddTransient<IGenericRepository<Sgroup>, SgroupRepository>();
			services.AddTransient<IGenericRepository<Student>, StudentRepository>();
			services.AddTransient<IGenericRepository<Teacher>, TeacherRepository>();
			services.AddTransient<IGenericRepository<TeacherInSubject>, TeacherInSubjectRepository>();


			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Students}/{action=Index}/{id?}");
            });
        }
    }
}
