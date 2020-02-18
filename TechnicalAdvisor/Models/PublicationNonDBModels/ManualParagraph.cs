using System.Collections.Generic;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class ManualParagraph
    {
        public string ChapterTitle { get; set; }

        public int NumberOfParagraphs { get; set; }

        public string Texts { get; set; }

        public ManualParagraph()
        {
        }

        public ManualParagraph(string chapterTitle, int numberOfParagraphs)
        {
            ChapterTitle = chapterTitle;
            NumberOfParagraphs = numberOfParagraphs;
        }
    }



}