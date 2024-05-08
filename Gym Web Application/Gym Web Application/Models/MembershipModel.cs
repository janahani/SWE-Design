namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class MembershipModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Client ID is required")]
    public int ClientID { get; set; }

    [Required(ErrorMessage = "Package ID is required")]
    public int PackageID { get; set; }

    [Required(ErrorMessage = "Start date is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Visits count is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Visits count must be a non-negative number")]
    public int VisitsCount { get; set; }

    [Required(ErrorMessage = "Invitations count is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Invitations count must be a non-negative number")]
    public int InvitationsCount { get; set; }

    [Required(ErrorMessage = "InBody sessions count is required")]
    [Range(0, int.MaxValue, ErrorMessage = "InBody sessions count must be a non-negative number")]
    public int InbodySessionsCount { get; set; }

    [Required(ErrorMessage = "Private training sessions count is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Private training sessions count must be a non-negative number")]
    public int PrivateTrainingSessionsCount { get; set; }

    [Required(ErrorMessage = "Freeze count is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Freeze count must be a non-negative number")]
    public int FreezeCount { get; set; }

    [Required(ErrorMessage = "Freeze status is required")]
    public string Freezed { get; set; }

    [Required(ErrorMessage = "Activation status is required")]
    public string IsActivated { get; set; }
}