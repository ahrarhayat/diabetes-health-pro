using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment.Models
{
    public partial class HealthPro : DbContext
    {
        public HealthPro()
            : base("name=HealthPro")
        {
        }

        public virtual DbSet<DietPlan> DietPlans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        
    }
}
