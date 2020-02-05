using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor
{
    public class ProductService
    {
        private readonly TechnicalAdvisorContext _context;

        public ProductService(TechnicalAdvisorContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
    }
}
