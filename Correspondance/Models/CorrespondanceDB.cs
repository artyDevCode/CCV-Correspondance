using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CCVCorrespondance.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CCVCorrespondance.DAL
{
    public class CorrespondanceDB : DbContext
    {

        public CorrespondanceDB() : base("name=CorrespondanceDB")
        {
        }
        
        public DbSet<Correspondance> Correspondances { get; set; }
        public DbSet<CorrespondanceType> CorrespondanceTypes { get; set; }
        public DbSet<CorrespondanceYear> CorrespondanceYears { get; set; }
        public DbSet<CorrespondanceReport> CorrespondanceReport { get; set; }
        public DbSet<Log> tblLog { get; set; }
        public DbSet<Access> tblAccess { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Correspondance>()
            //.HasOptional(WillCascadeOnDelete(false);

        }
       
    }
}

