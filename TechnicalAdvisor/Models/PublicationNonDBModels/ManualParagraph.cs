﻿using System.Collections.Generic;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class ManualParagraph
    {
        public string ChapterTitle { get; set; }

        public string SectionTitle { get; set; }

        public string Texts { get; set; }

        public int NumberOfPage { get; set; }

        public int TotalPages { get; set; }

        public int Times { get; set; }

        public ManualParagraph()
        {
        }

    
    }



}