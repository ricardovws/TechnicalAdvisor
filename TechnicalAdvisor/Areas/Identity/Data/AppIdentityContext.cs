using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechnicalAdvisor.Areas.Identity.Data;

namespace TechnicalAdvisor.Models
{
    public class AppIdentityContext : IdentityDbContext<AppIdentityUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public readonly UserManager<IdentityUser> _userManager;

        public AppIdentityContext(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

    
    }
}
