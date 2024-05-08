namespace Gym_Web_Application.Models;
using System;
using System.ComponentModel.DataAnnotations;
public class ScheduledUnfreezeModel
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "MembershipID is required")]
    public int MembershipID { get; set; }

    [Required(ErrorMessage = "FreezeStartDate is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FreezeStartDate { get; set; }

    [Required(ErrorMessage = "FreezeEndDate is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FreezeEndDate { get; set; }

    [Required(ErrorMessage = "FreezeCount is required")]
    public int FreezeCount { get; set; }
}