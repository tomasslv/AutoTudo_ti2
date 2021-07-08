using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StandV_ti2.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StandV_ti2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static void CreateDefaultAdmin(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            // www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx

            //adding admin role and default user
            var roleCheck =  roleManager.RoleExistsAsync("Admin").Result;
            if (!roleCheck)
            {
                 roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }
            var roleCheck2 = roleManager.RoleExistsAsync("Cliente").Result;
            if (!roleCheck2)
            {
                roleManager.CreateAsync(new IdentityRole("Cliente")).Wait();
            }
            if (userManager.FindByNameAsync("admin@gmail.com").Result == null)
            {
                var adminDefault = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };

                var adminResult = userManager.CreateAsync(adminDefault, "Admin123.").Result;
                if (adminResult.Succeeded)
                {
                     userManager.AddToRoleAsync(adminDefault, "Admin").Wait();
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReparacaoDB>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>() // ativa a utilização de Roles
                    .AddEntityFrameworkStores<ReparacaoDB>();

            services.AddControllersWithViews();
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            // criar o utilizador Admin que será o primeiro utilizador da App
            CreateDefaultAdmin(roleManager, userManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


         

        }
    }
}
