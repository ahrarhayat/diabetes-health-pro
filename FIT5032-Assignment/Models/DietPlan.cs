namespace FIT5032_Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DietPlan")]
    public partial class DietPlan
    {
       
        public int Id { get; set; }

        [Required]
        public string Sunday { get; set; }

        [Required]
        public string Monday { get; set; }

        [Required]
        public string Tuesday { get; set; }

        [Required]
        public string Wednesday { get; set; }

        [Required]
        public string Thursday { get; set; }

        [Required]
        public string Friday { get; set; }

        [Required]
        public string Saturday { get; set; }

        [DisplayName("Date Added")]
        public DateTime Date_added { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }

        [Required]
        public int? Rating { get; set; }

        [Required]
        [DisplayName("Number of Ratings")]
        public int? RatingNum { get; set; }
       
        /*public string MealType { get; set; }

       
        public string Recipes { get; set; }*/

       /* public DietPlan ()
        {
            *//*if(RatingNum == null)
            {
                RatingNum = 0;
            }*/

            /*if (MealPlan.Equals(null) )
            {

                MealType = "Dinner";
            }*//*
            
            //Recipes = "abc.com";
        }*/

    }
}
