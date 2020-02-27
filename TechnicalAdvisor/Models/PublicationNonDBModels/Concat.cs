using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class Concat
    {
        public ManualParagraph Text { get; set; }
        public List<ManualParagraph> Texts { get; set; }

        public Concat()
        {
        }

        public Concat(ManualParagraph text)
        {
            Text = text;
        }

        public Concat(List<ManualParagraph> texts)
        {
            Texts = texts;
        }

        public string WriteText(List<ManualParagraph> texts)
        {
            string fullText = null;

            foreach(var text in texts)
            {
                string thing = text.Texts;
                fullText += thing;
            }
            return fullText;
        }
    }
}
