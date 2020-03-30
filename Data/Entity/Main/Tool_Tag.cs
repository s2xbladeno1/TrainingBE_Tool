using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entity.Main
{
    [Table("Tool_Tag")]
    public class Tool_Tag
    {
        [Key]
        [ForeignKey("Tool")]
        public int ToolID { get; set; }
        [Key]
        [ForeignKey("Tag")]
        public int TagID { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
