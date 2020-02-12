using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Areas.Identity.Data;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Security.Services
{
    public static class DefaultUsersAndRoles
    {
        public static async Task Trasjh(AppIdentityContext context, UserManager<AppIdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Chad"));

        }
    }
}
