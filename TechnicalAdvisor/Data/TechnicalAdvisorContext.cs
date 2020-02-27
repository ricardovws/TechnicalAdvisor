using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Models
{
    public class TechnicalAdvisorContext : DbContext
    {
        public TechnicalAdvisorContext (DbContextOptions<TechnicalAdvisorContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }

        public DbSet<Dealer> Dealer { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<XmlProduct> XmlProduct { get; set; }

        

    }
}
