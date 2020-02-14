using Microsoft.AspNetCore.Mvc;
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

        public void LoadXML(int produtoId, string xmlName)
        {
            string path = "C:\\Users\\Ricardo\\Documents\\TechnicalAdvisor\\TechnicalAdvisor\\XMLFiles\\";


            int ID = 0;
            string extensionFile = ".xml";

            string fullXMLPath = path + xmlName + extensionFile;

            XElement root = XElement.Load(fullXMLPath);

            var queryXML =
                from g in root.Element("Generos").Elements("Genero")
                select g;
           
            try {
                var verify = _context.XmlProduct.Any();
                var lastXml = _context.XmlProduct.LastOrDefault();

                ID = lastXml.Id+1;
                 
            }
            catch
            {
                ID = 1;
            }
            

            foreach (var genero in queryXML)
            {
                // Console.WriteLine("{0}\t{1}", genero.Element("GeneroId").Value, genero.Element("Nome").Value);
                int xmlId = ID++;
                //int produtoID = 1+produtoId; //tentar descobrir pq precisa disso para funcionar!
                var tituloDoBloco = genero.Element("GeneroId").Value;
                var infosDiversas = genero.Element("Nome").Value;
                // var linkDaImagem = genero.Element("LinkDaImagem").Value;
                //var maisInfos = genero.Element("MaisInfos").Value;
                var linkDaImagem = "MERDA";
                var maisInfos = "MERDA";

                XmlProduct xmlproduct = new XmlProduct(xmlId, xmlName, produtoId
                    ,
                    tituloDoBloco,
                    infosDiversas,
                    linkDaImagem,
                    maisInfos
                    );
                
                _xMLService.SaveThis(xmlproduct);
            }

        }

        public Product FindProductById(int id)
        {
            var product = _context.Product.First(f => f.Id == id);
            return product;

        }

        public XmlProduct XmlObjectByProductId(int productId)
        {
            var xmlProduct=_xMLService.XmlObjectByProductId(productId);
            return xmlProduct;
        }

        public void AddXmlToProduct(int productID)
        {
            var product = FindProductById(productID);
            var xml = _xMLService.XmlObjectByProductId(productID);

            product.XMLInfo = xml;

            _context.Product.Update(product);
            _context.SaveChanges();
        }

    }
}
