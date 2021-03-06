﻿using System.ComponentModel.DataAnnotations;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.Identity
{
    public class SetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = ValidationErrorMessages.NewPasswordStringLenght)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredF)]
        [Display(Name = "Jaunā parole")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = ValidationErrorMessages.ComparePassword)]
        [Display(Name = "Atkārto jauno paroli")]
        public string ConfirmPassword { get; set; }
    }
}