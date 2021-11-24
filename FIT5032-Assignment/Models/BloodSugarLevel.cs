namespace FIT5032_Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BloodSugarLevel")]
    public partial class BloodSugarLevel
    {
        public int Id { get; set; }

        [Column("BloodSugarLevel")]
        [DisplayName("Blood Sugar Level")]
        public double BloodSugarLevel1 { get; set; }

        [DisplayName("Date of test")]
        public DateTime DateOfTest { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }
    }
}
