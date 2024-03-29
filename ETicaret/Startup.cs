﻿using ETicaret.Shared.Application;
using ETicaret.Shared.Data;
using ETicaret.Shared.Repository.UnitOfWork;
using ETicaret.Web.IdentityContext;
using ETicaret.Shared.Dal.Web;
using ETicaret.Shared.Application.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Application.Extensions;
using ETicaret.Shared.Application.Mapping;
using ETicaret.Web.Application.Basket;
using MediatR;
using System;
using ETicaret.Web.Application.Cookie;
using ETicaret.Shared.Application.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using ETicaret.Web.Application.Startup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using FluentValidation.AspNetCore;
using ETicaret.Shared.Application.Features.Category.Commands;
using ETicaret.Shared.Application.Registeration;
using ETicaret.Web.Data;

namespace ETicaret.Web
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
            services.AddApplicationCoreServices(Configuration, typeof(Startup), typeof(MappingProfile));

            services.AddDbContext<WebDbContext>(options => options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<MailSettings>(options => Configuration.GetSection("MailSettings").Bind(options));

            services.AddTransient<IEmailSender, SMTPMailService>();

            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<MarketPlaceDbContext>();
            services.AddTransient<RabbitMqHelper>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            

            services.ApplicationRegisteration();

			services.AddSignalR();
			services.AddScoped<ICookieService,CookieService>();

            var connstr = Configuration.GetValue<string>("RedisConfiguration:Connection") + ",password=" + Configuration.GetValue<string>("RedisConfiguration:Password");
            services.AddStackExchangeRedisCache(options => { options.Configuration = connstr; });
            services.AddDbContext<WebIdentityContext>(_ => _.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<WebUser, IdentityRole>().AddEntityFrameworkStores<WebIdentityContext>().AddDefaultTokenProviders();
            //services.AddDefaultIdentity<WebUser>().AddRoles<IdentityRole>().
            //    AddEntityFrameworkStores<WebIdentityContext>().AddDefaultTokenProviders();

            services.AddMvc();
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //       .RequireAuthenticatedUser()
            //       .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers()
          .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());



            services.AddHttpClient();

            services.Configure<FilePathOptions>(Configuration.GetSection(FilePathOptions.ConfigurationPath));



            //services.AddScoped<XCookieAuthEvents>();

            //// optional: customize cookie expiration time
            //services.ConfigureApplicationCookie(ops =>
            //{
            //    ops.EventsType = typeof(XCookieAuthEvents);
            //    ops.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    ops.SlidingExpiration = true;
            //});

            services.ConfigureApplicationCookie(_ =>
            {
                _.LoginPath = new PathString("/Identity/Account/Login");
             
             
            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<FilePathOptions> options)
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

            //var supportedCultures = new List<CultureInfo>
            //{
            //     new CultureInfo("tr-TR"),
            //     new CultureInfo("en-US"),
            //};



            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures,
            //    DefaultRequestCulture = new RequestCulture("tr-TR")
            //});

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseHttpsRedirection();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);



            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(options.Value.RootPath),
                RequestPath = new PathString(options.Value.ResponsePath)
            });
            app.UseStaticFiles();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
				endpoints.MapHub<ChatHub>("/chathub"); // ChatHub'ınızın URL'sini doğru şekilde eşleştirin.
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
        }
    }
}
