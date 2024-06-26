﻿using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.Models;

public class DeleteAccountModel
{
    [Display(Name = "Yes, I want to delete my account", Order = 5)]
    [CheckBoxRequired(ErrorMessage = "You must check the box to delete")]
    public bool DeleteAccount { get; set; } 
}
