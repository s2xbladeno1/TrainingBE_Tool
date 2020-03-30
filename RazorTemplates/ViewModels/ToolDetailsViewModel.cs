using Data.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazorTemplates.ViewModels
{
    public class ToolDetailsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public int ViewNumbers { get; set; }
        public int ViewDownloads { get; set; }
        public List<string> Tags { get; set; }
        public List<double> RatedNumber { get; set; }
    }
}
