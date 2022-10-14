using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.ViewModels;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyPortfolioDB"));
            });

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>)) ;



            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMvc();
            Action<GlobalVariables> Global = (opt =>
            {
                opt.IsLoggedIn = false;
                opt.IsAdmin = false;
            });
            services.Configure(Global);
            //Fetching Connection string from APPSETTINGS.JSON  
        

         
         


            var config = new MapperConfiguration(cfg =>
            {
                //User
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = ".Gardin.Session";
                options.Cookie.IsEssential = true;
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // This method gets called by the runtime. Use this method to add services to the container.
            services.AddScoped<IDataRepository<User>, DataRepository<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "defaultRoute",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });



           
        }
    }
}
