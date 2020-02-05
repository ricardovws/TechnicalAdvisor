using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Services
{
    public class UserService
    {
        private readonly TechnicalAdvisorContext _context;

        public UserService(TechnicalAdvisorContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }
    }
}
