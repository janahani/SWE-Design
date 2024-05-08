namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class JobTitlesModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}