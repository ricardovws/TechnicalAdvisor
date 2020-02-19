using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TechnicalAdvisor.Models;
using TechnicalAdvisor.Models.PublicationNonDBModels;
using TechnicalAdvisor.Models.ViewModels;
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

        public void LoadXML(LoadProductXMLFormViewModel loadProductXMLFormViewModel)  
        {
             // associa o documento xml com o produto

            string xmlName = loadProductXMLFormViewModel.XmlName;


            string path = "C:\\Users\\Ricardo\\Documents\\TechnicalAdvisor\\TechnicalAdvisor\\XMLFiles\\";
            string extensionFile = ".xml";
            string fullXMLPath = path + xmlName + extensionFile;

            XmlProduct xmlProduct = new XmlProduct();

            xmlProduct.ProductId = loadProductXMLFormViewModel.ID;
            xmlProduct.FileName = fullXMLPath;
            var product = FindProductById(loadProductXMLFormViewModel.ID);
            product.XmlProductId = xmlProduct.Id;
            _context.Update(product);
            _context.SaveChanges();


            // salva no DB o objeto que associa o documento xml com o produto

            _xMLService.SaveThis(xmlProduct);

            // já eras!
          
          
        }

        // Abaixo é um método para pegar arquivo xml já formatado, e montar ele na view.

            public Manual TakeAndReadXML(XmlProduct xmlProduct)
        {

            // recebe um produtoxml e abre o arquivo xml

            XElement root = XElement.Load(xmlProduct.FileName);

       
            var queryXML1 =
              from a in root.Element("Sections").Elements("ManualSection").Elements("Chapters").Elements("ManualChapter").
              Elements("Paragraph").Elements("ManualParagraph")

              select a;

            //ele entra nos nós dos paragrafos, onde ele pega todas as infos necessarias para criar o objeto manualparagraph
            List<ManualParagraph> paragraphs = new List<ManualParagraph>();
            foreach(var node in queryXML1)
            {
                ManualParagraph paragraph = new ManualParagraph();
                paragraph.SectionTitle = node.Element("SectionTitle").Value;
                paragraph.ChapterTitle = node.Element("ChapterTitle").Value;
                paragraph.Texts = node.Element("Text").Value;
                paragraphs.Add(paragraph);
            }

            //agora ele vai pegar e fazer uma lista com todos os capitulos e já vincular eles com os paragrafos 
            var queryXML2 =
             from b in root.Element("Sections").Elements("ManualSection").Elements("Chapters").Elements("ManualChapter")
            
              select b;
            List<ManualChapter> chapters = new List<ManualChapter>();
            foreach (var node2 in queryXML2)
            {
                ManualChapter chapter = new ManualChapter();
                chapter.Title = node2.Element("ChapterTitle").Value;
                //agora cada capitulo vai receber seus paragrafos
                var paras = paragraphs.Where(x => x.ChapterTitle == chapter.Title).ToList();
                chapter.Paragraph = paras;
                chapters.Add(chapter);
            }
            //Agora fará o mesmo com as seções
            var queryXML3 =
           from c in root.Element("Sections").Elements("ManualSection")

           select c;
            List<ManualSection> sections = new List<ManualSection>();
            foreach (var sec in queryXML3)
            {
                ManualSection section = new ManualSection();
                section.Title = sec.Element("SectionTitle").Value;
                
                //agora cada seção vai receber seus capitulos
                var chapters_ = chapters.Where(x => x.SectionTitle == section.Title).ToList();

                section.Chapters = chapters_;
                
                sections.Add(section);
                
            }

            Manual manual = new Manual("Carro muito louco", paragraphs, chapters, sections);

            return manual
;

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
