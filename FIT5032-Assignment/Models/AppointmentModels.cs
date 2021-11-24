using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment.Models
{
    public partial class AppointmentModels : DbContext
    {
        public AppointmentModels()
            : base("name=AppointmentModels")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.DoctorID)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.Clinic)
                .IsUnicode(false);
        }
    }
}
