using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfProduct { get; set; }
        public string PublicationCode { get; set; }
        public double PublicationVersion { get; set; }

        //public string ImagePath { get; set; }

        public ProductIndexViewModel()
        {
        }

        public ProductIndexViewModel(int id, string name, string typeOfProduct, string publicationCode, double publicationVersion)
        {
            Id = id;
            Name = name;
            TypeOfProduct = typeOfProduct;
            PublicationCode = publicationCode;
            PublicationVersion = publicationVersion;
        }
    }
}
