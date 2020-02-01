using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Dealer> Dealers { get; set; } = new List<Dealer>();
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public Company()
        {
        }

        public Company(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
