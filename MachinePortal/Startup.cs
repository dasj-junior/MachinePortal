using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MachinePortal.Models;
using MachinePortal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Razor;
using MachinePortal.Areas.Identity.Services;

namespace MachinePortal
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
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddHttpContextAccessor();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1073741824;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()   
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions
                        .AuthorizePage("/Home/AccessDenied")
                        .AddPageApplicationModelConvention("/Create",
                            model =>
                            {
                                model.Filters.Add(
                                                new RequestSizeLimitAttribute(1073741824));
                            });
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en-US", "pt-BR" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });

            services.AddDbContext<MachinePortalContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("MachinePortalContext"), builder => builder.MigrationsAssembly("MachinePortal")).EnableSensitiveDataLogging());

            services.Configure<IdentityOptions>(options =>
            {
                // User settings.
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";

                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 9;

                // Lockout settings.
                options.Lockout.MaxFailedAccessAttempts = 999;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.AddScoped<DeviceService>();
            services.AddScoped<DocumentService>();
            services.AddScoped<ResponsibleService>();
            services.AddScoped<LineService>();
            services.AddScoped<SectorService>();
            services.AddScoped<AreaService>();
            services.AddScoped<MachineService>();
            services.AddScoped<PermissionsService>();
            services.AddScoped<PasswordService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<SeedingService>();
            services.AddScoped<SeedingServiceDEV>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService, SeedingServiceDEV seedingServiceDEV)
        {
            if (env.IsDevelopment())
            {
                seedingService.Seed();
                seedingServiceDEV.Seed();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                seedingService.Seed();
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var supportedCultures = new[] { "en-US", "pt-BR" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
