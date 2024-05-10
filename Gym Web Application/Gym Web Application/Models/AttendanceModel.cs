namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class AttendanceModel
{
    [Key]
    public int ID { get; set; }
    
    [Required(ErrorMessage = "Employee ID is required")]
    public int EmployeeID { get; set; }

    [Required(ErrorMessage = "Attended is required")]
    public bool Attended { get; set; }

    [Required(ErrorMessage = "Date is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

}