using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechnicalAdvisor.Areas.Identity.Data;
using TechnicalAdvisor.Models;

[assembly: HostingStartup(typeof(TechnicalAdvisor.Areas.Identity.IdentityHostingStartup))]
namespace TechnicalAdvisor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AppIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppIdentityContextConnection")));


                services.AddDefaultIdentity<AppIdentityUser>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    
                })
                .AddEntityFrameworkStores<AppIdentityContext>();
                
                


            });
            

        }

        
    }
}