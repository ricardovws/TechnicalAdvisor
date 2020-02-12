using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Areas.Identity.Data;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Data
{
    public class SeedingService
    {
        private readonly TechnicalAdvisorContext _context;
        private readonly AppIdentityContext _appIdentityContext;
        private readonly UserManager<AppIdentityUser> _userManager;

        public SeedingService(TechnicalAdvisorContext context, AppIdentityContext appIdentityContext, UserManager<AppIdentityUser> userManager)
        {
            _context = context;
            _appIdentityContext = appIdentityContext;
            _userManager = userManager;
        }

        public void Seed()
        {
            if(_context.User.Any() ||
               _context.Product.Any() ||
               _context.Dealer.Any() ||
               _context.Company.Any())
            {
                return; //DB has been seeded.
            }

            //Companies

            Company c1 = new Company {
                Name = "Stara"
            };
            Company c2 = new Company
            {
                Name = "Jan"
            };
            Company c3 = new Company
            {
                Name = "Kepler Weber"
            };
            Company c4 = new Company
            {
                Name = "Pinhalense"
            };
            Company c5 = new Company
            {
                Name = "QueroDiesel"
            };
       

            //Dealers
            Dealer d1 = new Dealer("MaqLixo", c1);
            Dealer d2 = new Dealer("MaqMerda", c1);
            Dealer d3 = new Dealer("Podridão Máquinas LTDA.", c1);
            Dealer d4 = new Dealer("Máquinas Agrícolas Diversas", c1);

            Dealer d5 = new Dealer("MaqLixo", c2);
            Dealer d6 = new Dealer("MaqMerda", c2);
            Dealer d7 = new Dealer("Podridão Máquinas LTDA.", c2);
            Dealer d8 = new Dealer("Máquinas Agrícolas Diversas", c2);
            

            //Products
            Product p1 = new Product("RX-8000", "Pistola de Hádrons", "RMO-897500589", c1);
            Product p2 = new Product("TY-899", "Punheteira Pneumática", "R999-TY67KJ89", c1);
            Product p3 = new Product("JJK-009", "Estourador de Rabos Pneumático", "R-TY9999k", c1);
            Product p4 = new Product("JsssssJK-009", "Boqueteador Sismico", "R9k-1.0", c1);

            Product p6 = new Product("RX-8000", "Pistola de Hádrons", "RMO-897500589", c2);
            Product p7 = new Product("TY-899", "Punheteira Pneumática", "R999-TY67KJ89", c2);
            Product p8 = new Product("JJK-009", "Estourador de Rabos Pneumático", "R-TY9999k", c2);
            Product p9 = new Product("JsssssJK-009", "Boqueteador Sismico", "R9k-1.0", c2);

            //Users

            User u1 = new User("Joaquim", "joaquim@cu.com", d1);
            User u2 = new User("Jorge", "jorge@cu.com", d1);
            User u3 = new User("Cuzão", "cuzao@cu.com", d1);
            User u4 = new User("Cuseiro", "cuseiro@cu.com", d1);

            _context.Company.AddRange(c1, c2, c3, c4, c5);
            _context.Dealer.AddRange(d1, d2, d3, d4);
            _context.Product.AddRange(p1, p2, p3, p4, p6, p7, p8, p9);
            _context.User.AddRange(u1, u2, u3, u4);


            //_roleManager.CreateAsync(new IdentityRole("Admin"));
            
            




            _context.SaveChanges();

        }
    }
}
