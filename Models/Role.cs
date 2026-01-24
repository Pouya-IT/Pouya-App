using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pouya.Models
{
    [Table("Tbl_Roles")]
    public class Role
    {

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Role { get; set; }



        [Required]
        public string RoleName { get; set; }


        //Navigation To Children
        public virtual ICollection <Benutzer> Benutzers { get; set; }

    }
}