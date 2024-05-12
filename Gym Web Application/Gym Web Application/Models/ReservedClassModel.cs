namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class ReservedClassModel
{
    [Key]
    public int ID { get; set; }
    
    [Required(ErrorMessage = "Assigned Class ID is required")]
    public int AssignedClassID { get; set; }
    
    [Required(ErrorMessage = "Coach ID is required")]
    public int CoachID { get; set; }
    
    [Required(ErrorMessage = "Client ID is required")]
    public int ClientID { get; set; }
}