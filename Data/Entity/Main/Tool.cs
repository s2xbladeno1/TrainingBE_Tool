using Data.Entity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entity.Main
{
    [Table("Tool")]
    public class Tool
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public int ViewNumbers { get; set; }
        public int ViewDownloads { get; set; }
        [ForeignKey("Users")]
        public int CreatedBy { get; set; }
        public virtual Users Users { get; set; }

        [InverseProperty("Tool")] // tao quan he 1-n
        public virtual IEnumerable<Tool_Tag> Tool_Tags { get; set; }
        [InverseProperty("Tool")]
        public virtual IEnumerable<Rate> Rates { get; set; }
    }
}
