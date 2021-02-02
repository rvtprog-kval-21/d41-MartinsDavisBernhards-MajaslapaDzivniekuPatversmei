﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class NewsViewModel
    {
        [Key]
        public string NewsID { set; get; }

        [Required(ErrorMessage = "Ziņu teksts ir obligāts.")]
        [Display(Name = "Teksts")]
        public string Text { set; get; }

        public DateTime DateCreated { set; get; }

        public string ImagePath { set; get; }
    }
}