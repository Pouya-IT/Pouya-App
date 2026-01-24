using System;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


namespace Pouya.Models
{
    public class DatabaseContex : System.Data.Entity.DbContext
    {

        public DatabaseContex() 
            : base("Pouya") 
        {
            Database.SetInitializer<DatabaseContex>(null);
        }

        public System.Data.Entity.DbSet<Benutzer> Benutzer { get; set; }


        public System.Data.Entity.DbSet<Prdukte> Prdukte { get; set; }


        public System.Data.Entity.DbSet<Feedback> Feedback { get; set; }


        public System.Data.Entity.DbSet<Role> Role {  get; set; }

    }
}