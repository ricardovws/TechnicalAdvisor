using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models.ViewModels
{
    public class SearchInManualViewModel
    {
        public string ChapterTitle { get; set; }
        public string SectionTitle { get; set; }
        public int NumberPage { get; set; }

        public string WordSearch { get; set; }
        public int Times { get; set; }
        public string Texts { get; set; }

        public int MyProperty { get; set; }
    }
}
