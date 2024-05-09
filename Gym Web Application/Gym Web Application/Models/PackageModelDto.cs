namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class PackageModelDto
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(50, ErrorMessage = "Title must be between 2 and 50 characters", MinimumLength = 2)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Visits limit is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Visits limit must be a positive number")]
    public int VisitsLimit { get; set; }

    [Required(ErrorMessage = "Number of invitations is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Number of invitations must be a non-negative number")]
    public int NumOfInvitations { get; set; }

    [Required(ErrorMessage = "Number of InBody sessions is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Number of InBody sessions must be a non-negative number")]
    public int NumOfInbodySessions { get; set; }

    [Required(ErrorMessage = "Number of private training sessions is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Number of private training sessions must be a non-negative number")]
    public int NumOfPrivateTrainingSessions { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number")]
    public double Price { get; set; }
}
