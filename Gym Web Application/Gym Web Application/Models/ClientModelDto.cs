namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class ClientModelDto
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name must be between 2 and 50 characters", MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name must be between 2 and 50 characters", MinimumLength = 2)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(18, 80, ErrorMessage = "Age must be between 18 and 80")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }

}