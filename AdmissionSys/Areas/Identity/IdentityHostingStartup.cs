using System;
using AdmissionSys.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AdmissionSys.Areas.Identity.IdentityHostingStartup))]
namespace AdmissionSys.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AdmissionSysIdentityDbContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("AdmissionSysIdentityDbContextConnection")));

                services.AddIdentity<NuvAdUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                    config.SignIn.RequireConfirmedPhoneNumber = true;
                })
                    .AddEntityFrameworkStores<AdmissionSysIdentityDbContext>()
                .AddDefaultTokenProviders()
                 .AddDefaultUI();
            });
        }
    }
}