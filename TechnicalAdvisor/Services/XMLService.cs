using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Models;

namespace TechnicalAdvisor.Services
{
    public class XMLService
    {
        private readonly TechnicalAdvisorContext _context;

        public XMLService(TechnicalAdvisorContext context)
        {
            _context = context;
        }


        public void SaveThis(XmlProduct xmlproduct)
        {
            
            _context.Add(xmlproduct);
            _context.SaveChanges();
        }

 
       


    }
}
