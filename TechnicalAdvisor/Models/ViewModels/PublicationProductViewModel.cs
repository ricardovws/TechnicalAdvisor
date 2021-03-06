﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAdvisor.Models.PublicationNonDBModels;

namespace TechnicalAdvisor.Models.ViewModels
{
    public class PublicationProductViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int NumberOfPage { get; set; }
        public int TotalPages { get; set; }
        public List<ManualSection> Sections { get; set; }
        public List<ManualChapter> Chapters { get; set; }
        public List<ManualParagraph> Paragraphs { get; set; }

        public string Texts { get; set; }

        public string json { get; set; }




    }
}