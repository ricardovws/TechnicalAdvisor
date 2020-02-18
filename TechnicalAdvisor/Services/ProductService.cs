using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // **************************

            //string path = "C:\\Users\\Ricardo\\Documents\\TechnicalAdvisor\\TechnicalAdvisor\\XMLFiles\\";


            ////int ID = 0; -->Quando o banco é gerado novamente e recebe o seeding service, as vezes o Entity se perde, e não cria ID nas tabelas. Aí é preciso add esse trecho de código para funcionar.
            //string extensionFile = ".xml";

            //string fullXMLPath = path + xmlName + extensionFile;

            //XElement root = XElement.Load(fullXMLPath);

            //List<XmlProduct> xmlProducts = new List<XmlProduct>();

            //var queryXML =
            //    from g in root.Element("XXX").Elements("YYY")
            //    select g;


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

            //foreach (var nOME in queryXML)
            //    {
            //    //int xmlId = ID++; --> Quando o banco é gerado novamente e recebe o seeding service, as vezes o Entity se perde, e não cria ID nas tabelas. Aí é preciso add esse trecho de código para funcionar.

            //    var tituloDoBloco = nOME.Element("Description").Value;
            //    var infosDiversas = nOME.Element("LittleText").Value;



            //    XmlProduct xmlproduct = new XmlProduct(xmlName, produtoId
            //            ,
            //            tituloDoBloco,
            //            infosDiversas
            //            );

            //        xmlProducts.Add(xmlproduct);
            //    }
            //var xml1 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml1);

            //var queryXML2 =
            //   from g in root.Element("ZZZ").Elements("YYY")
            //   select g;
            //foreach (var nOME in queryXML2)
            //{
            //    //int xmlId = ID++; --> Quando o banco é gerado novamente e recebe o seeding service, as vezes o Entity se perde, e não cria ID nas tabelas. Aí é preciso add esse trecho de código para funcionar.

                
            //    var maisInfos = nOME.Element("Text").Value;
            //    xml1.MaisInfos = maisInfos;
            //}

            //var xml1 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml1);
            //var xml2 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml2);
            //var xml3 = xmlProducts.FirstOrDefault();
            //xmlProducts.Remove(xml3);

            //xml1.InfosDiversas += xml2.InfosDiversas;
            //xml1.InfosDiversas += xml3.InfosDiversas;

            //xml1.LinkDaImagem = "car.svg";

            //_xMLService.SaveThis(xml1);
          
        }

        // Abaixo é um método para pegar arquivo xml já formatado, e montar ele na view.

            public ManualParagraph TakeAndReadXML(XmlProduct xmlProduct)
        {


            // ESSE MÉTODO PRECISA FAZER ISSO!!!
            // recebe um produtoxml e abre o arquivo xml

            XElement root = XElement.Load(xmlProduct.FileName);

            // interpreta o arquivo xml e divide por classes:

            //divisão dos capítulos

            ManualParagraph paragraph = new ManualParagraph();

            //var queryXML1 =
            //  from a in root.Element("Sections").Elements("ManualSection").Elements("Chapters").Elements("ManualChapter").
            //  Elements("Paragraph").Elements("ManualParagraph")
            //  .Elements("Text").Elements("string")
            //  select a;

            Manual manual = new Manual();
            List<ManualSection> Sections = new List<ManualSection>();

            //seção
            //var queryXML1 =
            // from a in root.Element("Sections").Elements("ManualSection")
            // select a;
            
            //foreach (var node1 in queryXML1)
            //{
            //    var sectionTitle = node1.Element("SectionTitle").Value;
            //    ManualSection section = new ManualSection(sectionTitle);
                
            //    Sections.Add(section);
                
            //}


            //capítulo

            var queryXML2 =
            from a in root.Element("Sections").Elements("ManualSection").Elements("SectionTitle")
            select a;

            queryXML2.Where(x => x.Value == "Secao de Limpagem a seco");

            //var queryXML2 =
            //from a in root.Element("Sections").Elements("ManualSection").Elements("").
            //Elements("Chapters").Elements("ManualChapter")

            //select a;

            //foreach (var node2 in queryXML2)
            //{
            //    var chapterTitle = node2.Element("ChapterTitle").Value;
            //    ManualChapter chapter = new ManualChapter(chapterTitle);
            //    Sections.Add(chapter);

            //}


            return paragraph;



            //   cria uma lista de strings com o nome do capitulo + nome da seção
            //   cria lista de secoes com as infos dos capitulos
            //   cria manual
            //   manda manual pra controller

            // **************************














            

            //List < XmlProduct > xmlProducts = new List<XmlProduct>();

            //int ID = 0;

            //var queryXML =
            //    from g in root.Element("Secoes").Elements("Secao")
            //    select g;
            //foreach (var node in queryXML)
            //{
            //    int xmlID = ID++;

            //    var tituloDoBloco = node.Element("TituloSecao").Value;


            //    XmlProduct xmlproduct = new XmlProduct(xmlID, tituloDoBloco);
            //    xmlProducts.Add(xmlproduct);

            //    var queryXML2 =
            //    from f in root.Element("Secoes").Elements("Secao").Elements("Capitulos").Elements("Capitulo").Elements("Infos")
            //    select f;
            //    foreach(var node2 in queryXML2)
            //    {
            //        var infosDiversas = node2.Element("string").Value;
            //        xmlproduct.InfosDiversas = infosDiversas;
            //    }


            //    xmlProducts.Add(xmlproduct);
            //}
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
