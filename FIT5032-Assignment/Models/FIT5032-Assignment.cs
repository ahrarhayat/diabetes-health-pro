using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment.Models
{
    public partial class FIT5032_Assignment : DbContext
    {
        public FIT5032_Assignment()
            : base("name=FIT5032Assignment")
        {
        }

       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public class Models
        {
            internal class FIT5032Assignment
            {
            }
        }
    }
}
