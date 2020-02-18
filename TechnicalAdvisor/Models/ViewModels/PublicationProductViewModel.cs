using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Models.PublicationNonDBModels;

namespace TechnicalAdvisor.Models.ViewModels
{
    public class PublicationProductViewModel
    {
        public string Name { get; set; }
        public List<ManualParagraph> Paragraphs { get; set; }

        public PublicationProductViewModel(string name, List<ManualParagraph> paragraphs)
        {
            Name = name;
            Paragraphs = paragraphs;
        }
    }
}