using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Core.Contracts;

namespace DataLayer
{
    public class XplicitDbContext : DbContext
    {
        public XplicitDbContext():base("name=xplicit")
        {
            Database.SetInitializer<XplicitDbContext>(null);
        }

        public DbSet<Account> AccountSet { get; set; }
        public DbSet<Developer> DeveloperSet { get; set; }
        public DbSet<Hired> HiredSet { get; set; }
        public DbSet<Booking> BookingSet { get; set; }

        // set ORM rules
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // take of any events, properties, methods, interface implementations that entities use
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            // mapping rules
            modelBuilder.Entity<Account>().HasKey<int>(k => k.AccountId).Ignore(k => k.EntityId);
            modelBuilder.Entity<Developer>().HasKey<int>(k => k.DeveloperId).Ignore(k => k.EntityId);
            modelBuilder.Entity<Hired>().HasKey<int>(k => k.HiredId).Ignore(k => k.EntityId);
            modelBuilder.Entity<Booking>().HasKey<int>(k => k.BookedId).Ignore(k => k.EntityId);
            modelBuilder.Entity<Developer>().Ignore(k => k.CurrentlyHired);



        }
    }
}
