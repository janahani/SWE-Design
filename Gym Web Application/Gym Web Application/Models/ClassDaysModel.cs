namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class ClassDaysModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Class ID is required")]
    public int ClassID { get; set; }

    [Required(ErrorMessage = "Days field cannot be empty")]
    public string Days { get; set; }
}