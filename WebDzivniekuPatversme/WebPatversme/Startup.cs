using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Services;
using WebDzivniekuPatversme.Repositories;
using WebDzivniekuPatversme.Services.Other;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repositories.Interfaces;
using AutoMapper;

namespace WebDzivniekuPatversme
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(WebShelterDbContext), new WebShelterDbContext(Configuration.GetConnectionString("ShelterConnection"))));

            services.AddScoped<IAnimalsService, AnimalsService>();
            services.AddScoped<IAnimalsRepository, AnimalsRepository>();

            services.AddScoped<IContactsService, ContactsService>();
            services.AddScoped<IContactsRepository, ContactsRepository>();

            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<IShelterService, ShelterService>();
            services.AddScoped<IShelterRepository, ShelterRepository>();

            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IHomeRepository, HomeRepository>();

            services.AddScoped<ILoggerRepository, LoggerRepository>();

            services.AddTransient<UserChecker>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("ShelterConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<IdentityErrorDescriberLV>();

            services.AddTransient<IEmailSender, EmailSender>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserChecker seeder)
        {
            seeder.RoleChecker();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Animals}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}