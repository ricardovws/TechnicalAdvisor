using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class Page
    {
        public int number { get; set; }
        public List<ManualParagraph> pages { get; set; }



    }
}
