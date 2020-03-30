using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto
{
    public class SearchFilterDto
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
    }
}
