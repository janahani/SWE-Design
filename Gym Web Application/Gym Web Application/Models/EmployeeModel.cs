namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class EmployeeModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be between 2 and 50 characters", MinimumLength = 2)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public int PhoneNumber { get; set; }

    [Required(ErrorMessage = "Salary is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
    public double Salary { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Job title is required")]
    public int JobTitleID { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
    public string Password { get; set; }
}