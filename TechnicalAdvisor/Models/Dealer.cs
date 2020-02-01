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
        public Company Company { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public Dealer()
        {
        }

        public Dealer(int id, string name, Company company)
        {
            Id = id;
            Name = name;
            Company = company;
        }
    }
}
