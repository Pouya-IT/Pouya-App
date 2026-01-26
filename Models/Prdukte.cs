using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pouya.Models
{
    [Table("Tbl_Prdukte")]
    public class Prdukte
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id_Prdukte { get; set; }


        //FK*******************************************
        public long Id_User { get; set; }


        [ForeignKey("Id_User")]
        public virtual Benutzer Benutzer { get; set; }
        //*********************************************



        [Display(Name = "Produktnummer")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MinLength(1, ErrorMessage = "Die Zeichenlänge darf 1 Zeichen nicht unterschreiten")]
        [MaxLength(100, ErrorMessage = "Die Zeichenlänge darf 100 Zeichen nicht überschreiten")]
        public string Produktnummer { get; set; }




        [Display(Name = "Prduktename")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MinLength(2, ErrorMessage = "Die Zeichenlänge darf 2 Zeichen nicht unterschreiten")]
        [MaxLength(20, ErrorMessage = "Die Zeichenlänge darf 20 Zeichen nicht überschreiten")]
        public string Prduktename { get; set; }




        [Display(Name = "Bild")]
        public byte Bild { get; set; }





        [Display(Name = "Beschreibung")]
        public string Beschreibung { get; set; }





        [Display(Name = "Preis")]
        public string Preis { get; set; }




        //Virtual Delete
        public bool IDE_Delete_State { get; set; } = false;

    }
}