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

        public bool CheckAccessLevel(string emailToConfirm)
        {
            var test = _context.User.First(u => u.Email == emailToConfirm);
            if(test == null)
            {
                return false;
            }
            if (test.Email == emailToConfirm)
            {
                return true;
            }
            else
                return false;
        }
        

      
    }
}
