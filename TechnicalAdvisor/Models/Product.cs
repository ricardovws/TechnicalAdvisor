using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class Product
    {
        //################------->Atributos necessarios para a criação do produto no sistema
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfProduct { get; set; }
        public string PublicationCode { get; set; }
        public double PublicationVersion { get; set; }
        public Company Company { get; set; }
        public string PictureName { get; set; } //* Só esse que é optativo. Mas é aconselhavel que seja utilizado.
        //################

        //################------->Atributos utilizados depois que o produto foi criado no sistema
        public int XmlProductId { get; set; }

        public string Json { get; set; }
        //################

        public Product()
        {
        }

        public Product(string name, string typeOfProduct, string publicationCode, Company company, string pictureName)
        {
            Name = name;
            TypeOfProduct = typeOfProduct;
            PublicationCode = publicationCode;
            Company = company;
            PictureName = pictureName;
        }

        public Product(int id, string name, string typeOfProduct, string publicationCode, Company company, string pictureName)
        {
            Id = id;
            Name = name;
            TypeOfProduct = typeOfProduct;
            PublicationCode = publicationCode;
            Company = company;
            PictureName = pictureName;
            PublicationVersion = 0.0;
        }
    }
}
