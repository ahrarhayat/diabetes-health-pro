namespace FIT5032_Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UploadFile")]
    public partial class UploadFile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Path { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
