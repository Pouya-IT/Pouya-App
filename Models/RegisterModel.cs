using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pouya.Models
{
    public class RegisterModel
    {
        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(20, ErrorMessage = "Die Zeichenlänge darf 20 Zeichen nicht überschreiten")]
        [MinLength(2, ErrorMessage = "Die Zeichenlänge darf 2 Zeichen nicht unterschreiten")]
        public string Vorname { get; set; }



        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(20, ErrorMessage = "Die Zeichenlänge darf 20 Zeichen nicht überschreiten")]
        [MinLength(2, ErrorMessage = "Die Zeichenlänge darf 2 Zeichen nicht unterschreiten")]
        public string Nachname { get; set; }




        [Display(Name = "Benutzername")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(10, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        [MinLength(6, ErrorMessage = "Die Zeichenlänge darf 6 Zeichen nicht unterschreiten")]
        public string Benutzername { get; set; }




        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Bitte verwenden Sie eine Kombination von Zeichnen")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Passwort muss Buchstaben und Zahlen enthalten")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(10, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        [MinLength(6, ErrorMessage = "Die Zeichenlänge darf 6 Zeichen nicht unterschreiten")]
        public string Passwort { get; set; }


        [Display(Name = "Passwort Bestätigen")]
        [NotMapped]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [DataType(DataType.Password)]
        [Compare("Passwort", ErrorMessage = "Die Passwörter müssen übereinstimmen")]
        public string PasswortBestätigen { get; set; }



        [Display(Name = "E_Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Bitte geben Sie eine gültige E_Mail_Adresse ein")]
        public string E_Mail { get; set; }



        [Display(Name = "Land")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(10, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        [MinLength(2, ErrorMessage = "Die Zeichenlänge darf 2 Zeichen nicht unterschreiten")]
        public string Land { get; set; }



        [Display(Name = "Stadt")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(10, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        [MinLength(2, ErrorMessage = "Die Zeichenlänge darf 2 Zeichen nicht unterschreiten")]
        public string Stadt { get; set; }



        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [MaxLength(50, ErrorMessage = "Die Zeichenlänge darf 50 Zeichen nicht überschreiten")]
        [MinLength(5, ErrorMessage = "Die Zeichenlänge darf 5 Zeichen nicht unterschreiten")]
        public string Adresse { get; set; }



        [Display(Name = "Postleitzahl")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "PLZ muss genau 5 Ziffern haben")]
        //[MaxLength(10, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        //[MinLength(5, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht unterschreiten")]
        public string Postleitzahl { get; set; }



        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Bitte füllen Sie dieses Feld aus")]
        [RegularExpression(@"^(\+49|0)[0-9\s]{7,15}$", ErrorMessage = "Ungültige Telefonnummer")]
        //[MaxLength(12, ErrorMessage = "Die Zeichenlänge darf 10 Zeichen nicht überschreiten")]
        //[MinLength(4, ErrorMessage = "Die Zeichenlänge darf 4 Zeichen nicht unterschreiten")]
        public string Telefon { get; set; }

    }
}