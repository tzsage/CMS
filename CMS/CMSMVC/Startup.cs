using Autofac;
using AutoMapper;
using CMSMVC.Filter;
using CMSMVC.Models;
using CMSMVC.Validation;
using Common.Options;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace CMSMVC
{
    public class Startup
    {
        public static IContainer AutofacContainer;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //配置数据库连接
            services.Configure<DbOption>("Cms", Configuration.GetSection("DbOpion"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/Index";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            });
            //Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
            });
            // for CSRF Encrypt
            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder propertiest.
                options.FormFieldName = "AntiforgeryKey_CMS";
                options.HeaderName = "X-CSRF-TOKEN-CMS";
                options.SuppressXFrameOptionsHeader = false;
            });
            services.AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
                //option.Filters.Add(new StaticPageAttribute());
            })
              .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
              .AddControllersAsServices()
              .AddFluentValidation(fv =>
              {
                  //程序集方式引入
                  fv.RegisterValidatorsFromAssemblyContaining<ManagerRoleValidation>();
                  //去掉其他的验证，只使用FluentValidation的验证规则
                  fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
              });

            services.AddAutoMapper();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews(); 



            // return new AutofacServiceProvider(builder.Build());
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
            app.UseDefaultFiles();
            app.UseStaticFiles();
           
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "Action1Html",
            //        pattern: "{controller=Home}/{action=Index}/{id}.html");
            //});
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //添加依赖注入关系
            builder.RegisterModule(new AutofacModuleRegister());
            //var controllerBaseType = typeof(Controller);
            ////在控制器中使用依赖注入
            //builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            // .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
            // .PropertiesAutowired();
        }

    }
}
