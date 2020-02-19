using System.Collections.Generic;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class ManualChapter
    {
        public string Title { get; set; }
        public string SectionTitle { get; set; }
       public List<ManualParagraph> Paragraph { get; set; } = new List<ManualParagraph>();

        public ManualChapter()
        {
        }

        public ManualChapter(string title)
        {
            Title = title;
        }
    }


}