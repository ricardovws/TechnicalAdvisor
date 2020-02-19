using System.Collections.Generic;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class ManualSection
    {

        public string Title { get; set; }
       // public int ID { get; set; }
        public List<ManualChapter> Chapters { get; set; } 

        public ManualSection()
        {
        }

        public ManualSection(string title)
        {
            Title = title;
        }
    }
}