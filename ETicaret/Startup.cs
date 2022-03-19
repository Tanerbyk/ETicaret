using ETicaret.Shared.Repository.Abstract;
using ETicaret.Shared.Repository.EntityFramework;
using ETicaret.Shared.Repository.UnitOfWork;
using ETicaret.Shared.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using ETicaret.Shared.Application;
using Microsoft.AspNetCore.Http;

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
            services.AddDbContext<WebDbContext>(options => options.UseSqlServer("server=DESKTOP-FT9GJRN;database=ETicaretDb;integrated security = true"));
            services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WebDbContext>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddScoped<IUnitOfWork, UnitOfWork>();



            services.AddMvc();


 
            services.AddHttpClient();
            services.Configure<FilePathOptions>(Configuration.GetSection(FilePathOptions.ConfigurationPath));

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
            app.UseAuthorization();
            app.UseAuthentication(); 
            app.UseHttpsRedirection();
           

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
            });
        }
    }
}
