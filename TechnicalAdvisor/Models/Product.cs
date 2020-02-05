using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfProduct { get; set; }
        public string PublicationCode { get; set; }
        public double PublicationVersion { get; set; }
        public Company Company { get; set; }

        public Product()
        {
        }

        public Product(int id, string name, string typeOfProduct, string publicationCode, Company company)
        {
            Id = id;
            Name = name;
            TypeOfProduct = typeOfProduct;
            PublicationCode = publicationCode;
            PublicationVersion = 0.0;
            Company = company;
        }
    }
}
