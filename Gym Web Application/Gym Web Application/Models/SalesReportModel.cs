namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;

    public class SalesReport
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; } // Assuming you're using DateTime instead of LocalDate

        [Required(ErrorMessage = "Total revenue is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total revenue must be a positive number.")]
        public decimal TotalRevenue { get; set; }

        [Required(ErrorMessage = "Total memberships sold is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total memberships sold must be a non-negative number.")]
        public int TotalMembershipsSold { get; set; }

        [Required(ErrorMessage = "Total classes sold is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total classes attended must be a non-negative number.")]
        public int TotalClassesAttended { get; set; }



    }
