namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class EmployeeAuthorityModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Job title ID is required")]
    public int JobTitleID { get; set; }

    [Required(ErrorMessage = "Authority ID is required")]
    public int AuthorityID { get; set; }
}