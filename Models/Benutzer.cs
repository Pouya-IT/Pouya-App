using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace Pouya.Models
{
    [Table("Tbl_Benutzer")]
    public class Benutzer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id_User { get; set; }


        //FK****************************************
        public int? Id_Role { get; set; }

        //Navigation Property
        [ForeignKey("Id_Role")]
        public virtual Role Role { get; set; }
        //*******************************************
        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Benutzername { get; set; }

        public string Passwort { get; set; }

        public string E_Mail { get; set; }

        public string Land { get; set; }

        public string Stadt { get; set; }

        public string Adresse { get; set; }

        public string Postleitzahl { get; set; }

        public string Telefon { get; set; }


        //Virtual Delete
        public bool IDE_Delete_State { get; set; } = false;

        //***********************************************************
        //Navigation To Children
        public virtual ICollection<Prdukte> Prdukte { get; set; }

        //public virtual ICollection<Feedback> Feedback { get; set; }
        //***********************************************************
    }
}