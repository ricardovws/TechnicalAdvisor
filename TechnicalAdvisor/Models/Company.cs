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
