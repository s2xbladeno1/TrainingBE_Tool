using Data.Entity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entity.Main
{
    [Table("Rate")]
    public class Rate
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Tool")]
        public int ToolID { get; set; }
        [ForeignKey("Users")]
        public int UserID { get; set; }
        public int RatedNumber { get; set; }
        public int ReplyRate { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual Users Users { get; set; }
    }
}
