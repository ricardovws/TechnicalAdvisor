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

            List<XmlProduct> xmlProducts = new List<XmlProduct>();

            var queryXML =
                from g in root.Element("XXX").Elements("YYY")
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
            

                foreach (var nOME in queryXML)
                {
                    int xmlId = ID++;
                    
                    var tituloDoBloco = nOME.Element("Description").Value;
                    var infosDiversas = nOME.Element("LittleText").Value;
                    // var linkDaImagem = genero.Element("LinkDaImagem").Value;
                    //var maisInfos = genero.Element("MaisInfos").Value;
                    var linkDaImagem = "PRECISO COLOCAR UMA IMAGEM!!!";
                    var maisInfos = "_________";

                    XmlProduct xmlproduct = new XmlProduct(xmlId, xmlName, produtoId
                        ,
                        tituloDoBloco,
                        infosDiversas,
                        linkDaImagem,
                        maisInfos
                        );

                    xmlProducts.Add(xmlproduct);
                }

            var xml1 = xmlProducts.FirstOrDefault();
            xmlProducts.Remove(xml1);
            var xml2 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml2);
            //var xml3 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml3);

            //xml1.InfosDiversas += xml2.InfosDiversas;
            //xml1.InfosDiversas += xml3.InfosDiversas;

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
