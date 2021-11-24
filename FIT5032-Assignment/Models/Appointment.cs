namespace FIT5032_Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class Appointment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(128)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        public string DoctorID { get; set; }

        [Required]
        [DisplayName("Clinic")]
        public string Clinic { get; set; }

        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [DisplayName("End Time")]
        public DateTime? EndTime { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }
    }
}
