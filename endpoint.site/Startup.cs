using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using bugeto_stor.persistence.contexts;
using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.application.services.users.command.rgegisteruser;
using bugeto_stor.application.services.users.command.removeusers;
using bugeto_stor.application.services.users.command.userlogins;
using bugeto_stor.application.services.users.command.UserSatusChanges;
using bugeto_stor.application.services.users.command.editusers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using bugeto_stor.application.services.users.queries.getusers;
using bugeto_stor.application.services.users.queries.getrols;
using bugeto_stor.application.services.products.facapattern;
using bugeto_stor.application.interfaces.facadpatterns;

namespace EndPoint.Site
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

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });


            services.AddScoped<Idatabasecontext, databasecontext>();
            services.AddScoped<Igetusersservies,  getusersservies>();
            services.AddScoped<Igetrolsservice,  getrolesservice>();
            services.AddScoped<Irgegisteruserservice, rgegisteruserservice>();
            services.AddScoped<Iremoveuserservice, removeuserservice>();
            services.AddScoped<Iuserloginservice, userloginservice>();
            services.AddScoped<Iusersatuschangeservice, usersatuschangeservice>();
            services.AddScoped<Iedituserservice, edituserservice>();


            ////FacadeInject
          //  services.AddScoped <Iproductfacad, productfacad>();

            string contectionString = @"Data Source=.; Initial Catalog=bugeto_storeDb; Integrated Security=True;";
            services.AddEntityFrameworkSqlServer().AddDbContext<databasecontext>(option => option.UseSqlServer(contectionString));
            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                   name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
