using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pouya.Models
{
    public class FeedbackModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(30, ErrorMessage = "Die Zeichenlänge darf 30 Zeichen nicht überschreiten")]
        [MinLength(2, ErrorMessage = "Die Zeichenlänge darf 2 Zeichen nicht unterschreiten")]
        public string Name { get; set; }

        [Display(Name = "E_Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Bitte geben Sie eine gültige E_Mail_Adresse ein")]
        public string Email { get; set; }



        [Display(Name = "Ihre Meinung")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MinLength(5, ErrorMessage = "Die Zeichenlänge darf 5 Zeichen nicht unterschreiten")]
        [MaxLength(500, ErrorMessage = "Die Zeichenlänge darf 500 Zeichen nicht überschreiten")]
        public string Text { get; set; }
    }
}