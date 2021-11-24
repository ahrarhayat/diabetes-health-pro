using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment.Models
{
    public partial class BloodSugarLevelModels : DbContext
    {
        public BloodSugarLevelModels()
            : base("name=BloodSugarLevelModels")
        {
        }

        public virtual DbSet<BloodSugarLevel> BloodSugarLevels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
