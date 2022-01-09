using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moglan_Vlad_ProiectEB.Models;

namespace Moglan_Vlad_ProiectEB.Data
{
    public class CarServiceContext : DbContext
    {
        public CarServiceContext(DbContextOptions<CarServiceContext> options) :
       base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ProvidedService> ProvidedServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<ProvidedService>().ToTable("ProvidedService");

            modelBuilder.Entity<ProvidedService>().HasKey(c => new { c.ServiceID, c.LocationID });//configureaza cheia primara compusa

        }

        public DbSet<Moglan_Vlad_ProiectEB.Models.Employee> Employee { get; set; }
    }
}