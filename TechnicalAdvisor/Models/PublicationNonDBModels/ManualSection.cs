using System.Collections.Generic;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class ManualSection
    {

        public string Title { get; set; }
       // public int ID { get; set; }
        List<ManualChapter> Chapters { get; set; } = new List<ManualChapter>();
    }
}