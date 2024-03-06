using AM.ApplicationCore.domain;
using AM.Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infra
{
    public class AMContext:DbContext
    {
        //prop + enter
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Traveller> Travellers { get; set; }


        //override void onConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb; 
Initial Catalog=AirportManagement4SE2;Integrated Security=true");

            base.OnConfiguring(optionsBuilder);
        }


        // This is being used to execute the Configuration files
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Lines 37 and 38 are used to apply the configuration of the classes PlaneConfiguration and FlightConfiguration
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            modelBuilder.ApplyConfiguration(new PlaneConfiguration());

           // modelBuilder.ApplyConfiguration(new ReservationTicket());

            // configuration du type complexe( ou detenu ) FullName 
            //La propriété FirstName a une longueur maximale de 30 et le nom de la colonne 
            //correspondante à cette propriété dans la base de données doit être PassFirstName
            // la propriété LastName est obligatoire + column name est PassLastName
            modelBuilder.Entity<Passenger>().OwnsOne(fn => fn.FullName,

               full =>
            {
                full.Property(p => p.FirstName).HasMaxLength(30).HasColumnName("PassFirstName");
                full.Property(p => p.LastName).IsRequired().HasColumnName("PassLastName");
            } // => owned + contraintes de max length et obligation + changement de nom du column
            //TP5Q1
            );/*.HasDiscriminator<int>("IsTraveller").HasValue<Passenger>(2)
                                                  .HasValue<Traveller>(1)
                                                  .HasValue<Staff>(0);*/
            /*.HasDiscriminator<char>("IsTraveller").HasValue<Passenger>('p')
                                                  .HasValue<Traveller>('t')
                                                  .HasValue<Staff>('s');*/

            //Configuration de TPT (Table Per Type) 
            //TP5Q2
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            modelBuilder.Entity<Traveller>().ToTable("Travellers");
            base.OnModelCreating(modelBuilder);
        }
        //override configure + enter
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("Date");
            //configurationBuilder.Properties<String>().HaveMaxLength(100);
            base.ConfigureConventions(configurationBuilder);
        }

    }
}
