using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor
{
    public class DealerService
    {
        private readonly TechnicalAdvisorContext _context;

        public DealerService(TechnicalAdvisorContext context)
        {
            _context = context;
        }



        public void AddDealer(Dealer dealer)
        {
            _context.Dealer.Add(dealer);
            _context.SaveChanges();
        }

        public Dealer FindDealerById(int id)
        {
            var dealer = _context.Dealer.First(x => x.Id == id);
            return dealer;

        }

    }
}
