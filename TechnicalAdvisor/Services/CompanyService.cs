using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor
{
    public class CompanyService
    {
        private readonly TechnicalAdvisorContext _context;

        public CompanyService(TechnicalAdvisorContext context)
        {
            _context = context;
        }

        public Company FindCompanyById(int id)
        {
            var company = _context.Company.First(x => x.Id == id);
            return company;
            
        }
    }
}
