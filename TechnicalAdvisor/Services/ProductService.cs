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


            // salva no DB o objeto que associa o documento xml com o produto

            _xMLService.SaveThis(xmlProduct);

            // já eras!
          
          
        }

        // Abaixo é um método para pegar arquivo xml já formatado, e montar ele na view.

            public List<ManualParagraph> TakeAndReadXML(XmlProduct xmlProduct)
        {


          
            // recebe um produtoxml e abre o arquivo xml

            XElement root = XElement.Load(xmlProduct.FileName);

            // interpreta o arquivo xml e divide por classes:


            //divisão dos textos por parágrafos:

            //caminho pra chegar no nó em que queremos pegar os textos dos paragrafos
            List<ManualParagraph> infoAboutChaptersAndParagraphs = new List<ManualParagraph>();
            var queryXML1 =
              from a in root.Element("Sections").Elements("ManualSection").Elements("Chapters").Elements("ManualChapter")/*.*/
              //Elements("Paragraph").Elements("ManualParagraph")

              select a;

            //ele entra nos primeiros nós, onde irá pegar o nome do capitulo e o numero de paragrafos.
            ManualChapter chapter = new ManualChapter();
            foreach(var node in queryXML1)
            {
                var chapterTitle = node.Element("ChapterTitle").Value;
                var numberOfParagraphs = int.Parse(node.Element("NumberOfParagraphs").Value);
                
                infoAboutChaptersAndParagraphs.Add(new ManualParagraph(chapterTitle, numberOfParagraphs));
            }

            var queryXML2 =
             from b in root.Element("Sections").Elements("ManualSection").Elements("Chapters").Elements("ManualChapter")
             .Elements("Paragraph").Elements("ManualParagraph")

              select b;

            List<string> texts = new List<string>();
            foreach (var node2 in queryXML2)
            {
                var text = node2.Element("Text").Value;

                texts.Add(text);
            }

            foreach(var paragraph in infoAboutChaptersAndParagraphs)
            {
                int numberPara = paragraph.NumberOfParagraphs;
                for(int i = 0; i < numberPara; i++)
                {
                    var paras = texts.First();

                    texts.Remove(paras);

                    paragraph.Texts = paras;
                }
                
            }

            //int n = 3; //Representa a quantidade de paragrafos do capitulo 
            ////como a lista é de todas as infos do manual, agora precisamos dividir eles por objetos manualparagraph
            //List<ManualParagraph> manualParagraphs = new List<ManualParagraph>();
            //for(int i = 0; i < n; i++)
            //{
            //    var para = texts.First();
            //    texts.Remove(para);

            //    ManualParagraph paragraph_ = new ManualParagraph { Para = para };
            //    manualParagraphs.Add(paragraph_);
            //}


            return infoAboutChaptersAndParagraphs;

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
