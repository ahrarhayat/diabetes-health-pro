using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment.Models
{
    public partial class Upload : DbContext
    {
        public Upload()
            : base("name=Upload")
        {
        }

        public virtual DbSet<UploadFile> UploadFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UploadFile>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<UploadFile>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
