using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pouya.Models
{
    [Table("Tbl_Feedback")]
    public class Feedback
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id_Feedback { get; set; }


        //FK*************************************
        //public long Id_User { get; set; }

        //[ForeignKey("Id_User")]
        //public virtual Benutzer Benutzer { get; set; }
        //***************************************
        public string Name { get; set; }

        public string Email { get; set; }


        [Display(Name = "Ihre Meinung")]
        public string Text { get; set; }

        public bool IsApproved { get; set; } = false;


        [Display(Name = "Datum")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        //Virtual Delete
        public bool IDE_Delete_State { get; set; } = false;

    }
}