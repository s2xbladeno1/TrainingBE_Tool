using Data.Entity.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entity.Account
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        [InverseProperty("Users")]
        public virtual IEnumerable<Tool> Tools { get; set; }
        [InverseProperty("Users")]
        public virtual IEnumerable<Rate> Rates { get; set; }
    }
}
