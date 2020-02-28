using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        public void LoadPublication(LoadProductXMLFormViewModel publication)
        {
            LoadXML(publication); //estrutura o xml e salva referencia no DB
            LoadJson(XmlObjectByProductId(publication.ID)); //cria o Json e salva no DB
            return;
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
       
            //try {

            //    var getId = _context.XmlProduct.Last(i => i.Id != 0);
            //    xmlProduct.Id = getId.Id++;
            //}

            //catch
            //{
               
            //    xmlProduct.Id = 1;

            //}

            _xMLService.SaveThis(xmlProduct); // salva no DB o objeto que associa o documento xml com o produto

            // já eras!
          
          
        }

        public void LoadJson(XmlProduct xmlProduct)
        {

            // recebe um produtoxml e abre o arquivo xml

            XElement root = XElement.Load(xmlProduct.FileName);


            var queryXML1 =
              from a in root.Element("Sections").Elements("ManualSection").Elements("Chapters").Elements("ManualChapter").
              Elements("Paragraph").Elements("ManualParagraph")

              select a;

            //ele entra nos nós dos paragrafos, onde ele pega todas as infos necessarias para criar o objeto manualparagraph
            List<ManualParagraph> paragraphs = new List<ManualParagraph>();
            foreach (var node in queryXML1)
            {
                ManualParagraph paragraph = new ManualParagraph();
                paragraph.SectionTitle = node.Element("SectionTitle").Value;
                paragraph.ChapterTitle = node.Element("ChapterTitle").Value;
                paragraph.Texts = node.Element("Text").Value;
                paragraphs.Add(paragraph);
            }
            //agora ele vai montar as listas de seções, capítulos e de parágrafos!!!

            //montando a lista de capítulos:
            var queryXML2 =
             from a in root.Element("Sections").Elements("ManualSection").Elements("Chapters").Elements("ManualChapter")
            select a;
            List<ManualChapter> chapters = new List<ManualChapter>();
            foreach(var node2 in queryXML2)
            {
                ManualChapter chapter = new ManualChapter();
                chapter.Title = node2.Element("ChapterTitle").Value;
                chapters.Add(chapter);
            }

            //montando a lista de seções:
            var queryXML3 =
            from a in root.Element("Sections").Elements("ManualSection")
            select a;
            List<ManualSection> sections = new List<ManualSection>();
            foreach (var node3 in queryXML3)
            {
                ManualSection section = new ManualSection();
                section.Title = node3.Element("SectionTitle").Value;
                sections.Add(section);
            }

            //agora termina e continua o restante do código...
            //

            //entrando na parte do código que vai começar a se direcionar pra paginação

            // conta o numero total de paginas do manual
            int totalLines = 0; //Número inicial de páginas do manual

            foreach (var para in paragraphs)
            {
                var p = para.Texts.Count();
                totalLines += p;

            }

            //agora é necessário dividir o numero total de paginas pelo numero total aceitável por página, que é arbitrário.
            //só fazendo testes pra ver mesmo, eu vou colocar um que seja conveniente nesse momento.

            int totalLinesOfAPage = 50; // numero de linhas maximo de uma pagina!


            //var NumberOfPages = totalLines / totalLinesOfAPage;


            //cria listas que vao compor as paginas e que farão parte da instanciação do objeto "publicationProductViewModel"

            var pages = CreatePages(paragraphs, totalLinesOfAPage);
            Manual manual = new Manual("Carro loucaço", pages, chapters, sections);
            var json = JsonConvert.SerializeObject(manual);

            var product = FindProductById(xmlProduct.ProductId);

            product.Json = json;

            _context.Product.Update(product);
            _context.SaveChanges();
      
            return;

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


        private List<ManualParagraph> CreatePages(List<ManualParagraph> paragraphs, int totalLinesOfAPage)
        {
            List<ManualParagraph> list = new List<ManualParagraph>();
            int AlreadyDone_text= 0;
            int AlreadyDone_pages = 0;
            int Page = 1;

            foreach (var page in paragraphs)
            {
                if (AlreadyDone_text < totalLinesOfAPage)
                {
                    ManualParagraph paragraph = page;
                    paragraph.NumberOfPage = Page;
                    AlreadyDone_text += paragraph.Texts.Count();
                    list.Add(paragraph);

                }
                else
                {
                    ManualParagraph paragraph = page;
                    paragraph.NumberOfPage = Page++;
                    AlreadyDone_pages++;
                    AlreadyDone_text = 0;
                    list.Add(paragraph);
                }

            }
                       
            return list;
        }

   
    }
}
