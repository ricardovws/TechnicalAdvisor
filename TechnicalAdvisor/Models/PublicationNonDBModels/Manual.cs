﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class Manual
    {
        public string Name { get; set; }
        
        public List<ManualParagraph> Paragraphs { get; set; }

        public List<ManualChapter> Chapters { get; set; }

        public List<ManualSection> Sections { get; set; }

        public string Json { get; set; }

        public int TotalPages { get; set; }


        public Manual()
        {
        }

        public Manual(string name, List<ManualParagraph> paragraphs)
        {
            Name = name;
            Paragraphs = paragraphs;
        }

        public Manual(string name, List<ManualParagraph> paragraphs, List<ManualChapter> chapters, List<ManualSection> sections)
        {
            Name = name;
            Paragraphs = paragraphs;
            Chapters = chapters;
            Sections = sections;
        }

    }
}
