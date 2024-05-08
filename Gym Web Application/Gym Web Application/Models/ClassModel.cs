namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class ClassModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Image Path is required")]
    public string ImgPath { get; set; }
}