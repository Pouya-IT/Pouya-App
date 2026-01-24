using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pouya.Models
{
    public class Login
    {
        [Display(Name = "Benutzername")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(10, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        [MinLength(6, ErrorMessage = "Die Zeichenlänge darf 6 Zeichen nicht unterschreiten")]
        public string Benutzername { get; set; }



        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(10, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        [MinLength(6, ErrorMessage = "Die Zeichenlänge darf 6 Zeichen nicht unterschreiten")]
        public string Passwort { get; set; }
    }
}