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

        public void LoadXML(int produtoId, string xmlName)  //Método que pega o XML e manda pro sistema de fato
        {
            string path = "C:\\Users\\Ricardo\\Documents\\TechnicalAdvisor\\TechnicalAdvisor\\XMLFiles\\";


            //int ID = 0; -->Quando o banco é gerado novamente e recebe o seeding service, as vezes o Entity se perde, e não cria ID nas tabelas. Aí é preciso add esse trecho de código para funcionar.
            string extensionFile = ".xml";

            string fullXMLPath = path + xmlName + extensionFile;

            XElement root = XElement.Load(fullXMLPath);

            List<XmlProduct> xmlProducts = new List<XmlProduct>();

            var queryXML =
                from g in root.Element("XXX").Elements("YYY")
                select g;


            //--> Quando o banco é gerado novamente e recebe o seeding service, as vezes o Entity se perde, e não cria ID nas tabelas. Aí é preciso add esse trecho de código para funcionar.
            //try {
            //    var verify = _context.XmlProduct.Any();
            //    var lastXml = _context.XmlProduct.LastOrDefault();

            //    ID = lastXml.Id+1;

            //}
            //catch
            //{
            //    ID = 1;
            //}
            //

            foreach (var nOME in queryXML)
                {
                //int xmlId = ID++; --> Quando o banco é gerado novamente e recebe o seeding service, as vezes o Entity se perde, e não cria ID nas tabelas. Aí é preciso add esse trecho de código para funcionar.

                var tituloDoBloco = nOME.Element("Description").Value;
                var infosDiversas = nOME.Element("LittleText").Value;



                XmlProduct xmlproduct = new XmlProduct(xmlName, produtoId
                        ,
                        tituloDoBloco,
                        infosDiversas
                        );

                    xmlProducts.Add(xmlproduct);
                }
            var xml1 = xmlProducts.FirstOrDefault();
            xmlProducts.Remove(xml1);

            var queryXML2 =
               from g in root.Element("ZZZ").Elements("YYY")
               select g;
            foreach (var nOME in queryXML2)
            {
                //int xmlId = ID++; --> Quando o banco é gerado novamente e recebe o seeding service, as vezes o Entity se perde, e não cria ID nas tabelas. Aí é preciso add esse trecho de código para funcionar.

                
                var maisInfos = nOME.Element("Text").Value;
                xml1.MaisInfos = maisInfos;
            }

            //var xml1 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml1);
            //var xml2 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml2);
            //var xml3 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml3);

            //xml1.InfosDiversas += xml2.InfosDiversas;
            //xml1.InfosDiversas += xml3.InfosDiversas;

            xml1.LinkDaImagem = "car.svg";

            _xMLService.SaveThis(xml1);
          
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

       

    }
}
