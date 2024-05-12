namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class AssignedClassModel
{
    [Key]
    public int ID { get; set; }
    
    [Required(ErrorMessage = "Class ID is required")]
    public int ClassID { get; set; }
    
    [Required(ErrorMessage = "Coach ID is required")]
    public int CoachID { get; set; }
    
    [Required(ErrorMessage = "Date is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
    
    [Required(ErrorMessage = "Start time is required")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime StartTime { get; set; }
    
    [Required(ErrorMessage = "End time is required")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime EndTime { get; set; }
    
    [Required(ErrorMessage = "isFree is required")]
    public bool IsFree { get; set; }
    
   [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public double Price { get; set; } = 0; 
    
    [Required(ErrorMessage = "Number of attendants is required")]
    [Range(5, int.MaxValue, ErrorMessage = "Number of attendants must be a non-negative number")]
    public int NumOfAttendants { get; set; }
    
    [Required(ErrorMessage = "Available places is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Available places must be a non-negative number")]
    public int AvailablePlaces { get; set; }
}