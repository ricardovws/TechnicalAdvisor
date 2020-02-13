using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models
{
    public class XmlProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public XmlProduct()
        {
        }

        public XmlProduct(string name)
        {
            Name = name;
        }

        public XmlProduct(int iD, string name)
        {
            ID = iD;
            Name = name;
        }
    }
}
