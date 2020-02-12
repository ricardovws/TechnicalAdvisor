using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly CompanyService _companyService;
        private readonly DealerService _dealerService;

        public UserService(TechnicalAdvisorContext context, CompanyService companyService, DealerService dealerService)
        {
            _context = context;
            _companyService = companyService;
            _dealerService = dealerService;
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User CheckAccessLevel(string emailToConfirm)
        {

            User user = new User();

            var test = _context.User.First(u => u.Email == emailToConfirm);

            user = test;

            var dealer = LoadDealer(user.DealerID);

            user.Dealer = dealer;

            var company = LoadCompany(dealer.CompanyID);

            user.Dealer.Company = company;

            return user;
           
        }
        private Dealer LoadDealer(int dealerID)
        {
            var dealer = _dealerService.FindDealerById(dealerID);
            return dealer;
        }
        private Company LoadCompany(int companyID)
        {
            var company = _companyService.FindCompanyById(companyID);
            return company;
        }


    }
}
