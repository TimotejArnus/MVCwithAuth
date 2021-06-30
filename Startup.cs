using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Owin;
using NapredniObrazec.DataBase;
using NapredniObrazec.Models;
using Microsoft.Owin.Security.Cookies;
using PathString = Microsoft.AspNetCore.Http.PathString;


namespace NapredniObrazec
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
            //services.AddControllersWithViews();
            //services.Configure<CookiePolicyOptions>(opttion =>
            //{
            //    opttion.CheckConsentNeeded = context => true;
            //    opttion.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            //DataBase.Database db = new Database();
            


            services.AddDbContext<DataBase.Database>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(
                    v =>
                    {
                        v.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                        v.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                    })
                .AddGoogle(googleOptions =>
                {
                    //AIzaSyC503PBeTP0zmGVg6v5pOLvA1fxAOQY7X0 key

                    //IConfigurationSection googleAuthNSection =
                    //    Configuration.GetSection("Authentication:Google");

                    //googleOptions.ClientId = googleAuthNSection["ClientId"];      //53863904145-ghh5ga8bb3h7cn7699a4qfeb8ni16khc.apps.googleusercontent.com
                    //googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];

                    googleOptions.ClientId = "562367496248-9le21so99h4doq40ifma71dc0ftlkqc6.apps.googleusercontent.com";
                    googleOptions.ClientSecret = "-mTan01wzljnjJaWL1ELXb_c";

                }); 

            services.AddIdentity<User, IdentityRole>()      // Vkljucitev Identytija 
                .AddRoleManager<RoleManager<IdentityRole>>()    // TEST
                
                .AddEntityFrameworkStores<DataBase.Database>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseAuthorization();

           

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


            

            app.UseAuthentication();

            app.UseAuthorization();

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseCookiePolicy();
        }
    }
}
