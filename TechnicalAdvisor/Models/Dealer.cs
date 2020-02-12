using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class Dealer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        
        
        public Dealer()
        {
        }

        public Dealer(string name, Company company)
        {
            Name = name;
            Company = company;
        }

        public Dealer(int id, string name, Company company)
        {
            Id = id;
            Name = name;
            Company = company;
        }

        
    }
}
