﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using WebStore.Clients.Employees;
using WebStore.Clients.Identity;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Values;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrustructure.AutoMapperProfiles;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestApi;
using WebStore.Services.Products.InCookies;
using WebStore.Logger;
using WebStore.Infrustructure.Middleware;
using WebStore.Services.Products;

namespace WebStore
{
    public class Startup
    {
       
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ViewModelsMapping>();
            }, typeof(Startup));

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();

            #region WebApi Identity
            services
                .AddTransient<IUserStore<User>, UsersClient>()
                .AddTransient<IUserPasswordStore<User>, UsersClient>()
                .AddTransient<IUserEmailStore<User>, UsersClient>()
                .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTransient<IUserClaimStore<User>, UsersClient>()
                .AddTransient<IUserLoginStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion


            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG          
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredUniqueChars = 3;

                opt.User.RequireUniqueEmail = false;
#endif

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opt.Lockout.MaxFailedAccessAttempts = 10;
            });

            services.ConfigureApplicationCookie(opt => {

                opt.Cookie.Name = "WebStore.ru";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;

            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddScoped<IEmployeesData, EmployeesClient>();
            services.AddScoped<IProductData, ProductsClient>();
            //  services.AddScoped<ICartService, CookiesCartService>();
            services.AddScoped<ICartStore, CookiesCartStore>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrdersClient>();
            services.AddTransient<IValueService, ValuesClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.UseStaticFiles();
            app.UseDefaultFiles();
            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
