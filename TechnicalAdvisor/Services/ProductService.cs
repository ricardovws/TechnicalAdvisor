using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechnicalAdvisor.Models;
using TechnicalAdvisor.Services;

namespace TechnicalAdvisor
{
    public class ProductService
    {
        private readonly TechnicalAdvisorContext _context;
        private readonly XMLService _xMLService;

        public ProductService(TechnicalAdvisorContext context, XMLService xMLService)
        {
            _context = context;
            _xMLService = xMLService;
        }

        public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void LoadXML()
        {

            XElement root = XElement.Load(@"C:\Users\Ricardo\Downloads\musics_diversos.xml");

            var queryXML =
                from g in root.Element("Generos").Elements("Genero")
                select g;

            foreach (var genero in queryXML)
            {
                Console.WriteLine("{0}\t{1}", genero.Element("GeneroId").Value, genero.Element("Nome").Value);


                var generoId = genero.Element("GeneroId").Value;
                var nome = genero.Element("Nome").Value;
                int id = int.Parse(generoId);
                XmlProduct xmlproduct = new XmlProduct(id, nome);
                
                _xMLService.SaveThis(xmlproduct);
            }


           

        }

       
    }
}
