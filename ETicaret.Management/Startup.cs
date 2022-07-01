
using ETicaret.Management;
using ETicaret.Shared.Application;
using ETicaret.Shared.Application.Features.Category.Queries;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Repository.UnitOfWork;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Reflection;
using static ETicaret.Shared.Application.Features.Category.Queries.GetAllCategoryQuery;


namespace E_Ticaret.Management
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MarketPlaceDbContext>();

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());




            //services.AddMediatR(typeof(Startup));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();


            //var connstr = Configuration.GetValue<string>("RedisConfiguration:Connection") + ",password=" + Configuration.GetValue<string>("RedisConfiguration:Password");
            //services.AddStackExchangeRedisCache(options => { options.Configuration = connstr; });

             services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup).Assembly);




            services.AddMvc(config =>
            {
                config.CacheProfiles.Add("Create",
         new CacheProfile()
         {
             Location = ResponseCacheLocation.None,
             NoStore = true
         });
            }).AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
               
            });



            services.AddMvc();
            services.AddMemoryCache();
             
            services.AddHttpClient();
            services.Configure<FilePathOptions>(Configuration.GetSection(FilePathOptions.ConfigurationPath));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(options.Value.RootPath),
                RequestPath = new PathString(options.Value.ResponsePath)
            });
        


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCors(options => options.AllowAnyOrigin());

        }
    }
}
