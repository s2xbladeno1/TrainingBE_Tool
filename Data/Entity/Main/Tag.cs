using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity.Main
{
    [Table("Tag")]
    public class Tag
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [InverseProperty("Tag")]
        public virtual IEnumerable<Tool_Tag> Tool_Tags { get; set; }
    }
}
