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

        public List<ManualChapter> Chapters { get; set; }

        public List<ManualSection> Sections { get; set; }

        public PublicationProductViewModel()
        {
        }

        public PublicationProductViewModel(string name, List<ManualParagraph> paragraphs, List<ManualChapter> chapters, List<ManualSection> sections)
        {
            Name = name;
            Paragraphs = paragraphs;
            Chapters = chapters;
            Sections = sections;
        }
    }
}