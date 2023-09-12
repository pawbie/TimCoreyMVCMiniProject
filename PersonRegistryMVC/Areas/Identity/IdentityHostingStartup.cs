using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistryMVC.Areas.Identity.Data;
using PersonRegistryMVC.Data;

[assembly: HostingStartup(typeof(PersonRegistryMVC.Areas.Identity.IdentityHostingStartup))]
namespace PersonRegistryMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PersonRegistryMVCContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("PersonRegistryMVCContextConnection")));

                services.AddDefaultIdentity<PersonRegistryMVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<PersonRegistryMVCContext>();
            });
        }
    }
}