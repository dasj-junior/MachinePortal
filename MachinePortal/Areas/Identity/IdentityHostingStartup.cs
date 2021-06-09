using System;
using MachinePortal.Areas.Identity.Data;
using MachinePortal.Areas.Identity.Services;
using MachinePortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(MachinePortal.Areas.Identity.IdentityHostingStartup))]
namespace MachinePortal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        { 
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddTransient<IEmailSender, EmailSender>();

                services.AddDefaultIdentity<MachinePortalUser>()
                    .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}