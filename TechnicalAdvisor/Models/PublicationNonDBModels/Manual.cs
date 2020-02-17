using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAdvisor.Models.PublicationNonDBModels
{
    public class Manual
    {
        public string Name { get; set; }
        List<ManualSection> Sections { get; set; } = new List<ManualSection>();
    }
}
