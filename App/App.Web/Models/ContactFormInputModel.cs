﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
    public class ContactFormInputModel
    {
        [Required(ErrorMessage = " Моля попълнете вашето собствено и фамилно име!")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = " Максимална дължина 250 символа, минимална 3")]
        [Display(Name = "Имена:")]
        public string Name { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = " Моля предоставете валиден email!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Телефон:")]
        [Required(ErrorMessage = " Моля предоставете валиден телефонен номер!")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = " Максимална дължина 250 символа, минимална 3")]
		public string Phone { get; set; }


        [Required(ErrorMessage = " * Задължително!")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = " Максимална дължина 250 символа, минимална 3")]
        [Display(Name = "Относно:")]
        public string Subject { get; set; }


        [Required(ErrorMessage = " * Задължително!")]
        [StringLength(5000, MinimumLength = 3, ErrorMessage = " Максимална дължина 5000 символа, минимална 3")]
        [Display(Name = "Съдържание:")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}