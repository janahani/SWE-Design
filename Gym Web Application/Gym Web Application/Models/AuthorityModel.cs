namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class AuthorityModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Header is required")]
    public string Header { get; set; }

    [Required(ErrorMessage = "Friendly name is required")]
    public string FriendlyName { get; set; }

    [Required(ErrorMessage = "Link address is required")]
    public string LinkAddress { get; set; }

    [Required(ErrorMessage = "Icon is required")]
    public string Icon { get; set; }
}