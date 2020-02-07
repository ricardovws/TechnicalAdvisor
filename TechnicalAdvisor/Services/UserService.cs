using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Areas.Identity.Data;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Services
{
    public class UserService
    {
        private readonly TechnicalAdvisorContext _context;
        private readonly UserManager<AppIdentityUser> _userManager;

        public UserService(TechnicalAdvisorContext context, UserManager<AppIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

      
    }
}
