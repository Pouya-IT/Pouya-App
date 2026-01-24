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
        public long Id_User { get; set; }


        [ForeignKey("Id_User")]
        public virtual Benutzer Benutzer { get; set; }
        //***************************************

        [Display(Name = "Vorschläge_und_Beschwerden")]
        [MinLength(5, ErrorMessage = "Die Zeichenlänge darf 5 Zeichen nicht unterschreiten")]
        [MaxLength(500, ErrorMessage = "Die Zeichenlänge darf 500 Zeichen nicht überschreiten")]
        public string Text { get; set; }




        //Virtual Delete
        public bool IDE_Delete_State { get; set; }

    }
}