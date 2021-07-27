namespace MassageStudioLorem
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using MassageStudioLorem.Data;
    using MassageStudioLorem.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services.Masseurs;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDefaultIdentity<IdentityUser>
                    //    (IdentityOptionsProvider.GetIdentityOptions)
                    //.AddRoles<ApplicationRole>
                    (options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.User.RequireUniqueEmail = true;
                    })
                .AddEntityFrameworkStores<LoremDbContext>();

            services.AddDbContext<LoremDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews(options =>
            {
                options.Filters
                    .Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IMasseurService, MasseurService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapControllerRoute
                    (
                        "Details",
                        "{controller}/{action}/{massageId?}/{categoryId?}",
                        new {controller = "Categories", action = "Details"}
                    );
                    //TODO: Fix routes
                    endpoints.MapControllerRoute
                    (
                        "All",
                        "{controller}/{action}/{massageId?}/{categoryId?}",
                        new {controller = "Masseurs", action = "All"}
                    );
                    endpoints.MapRazorPages();
                });
        }
    }
}