using System.Collections.Generic;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class ManualParagraph
    {
        public string ChapterTitle { get; set; }

        //public int ChapterID { get; set; }

        public List<string> text = new List<string>();

        public ManualParagraph()
        {
        }
    }



}