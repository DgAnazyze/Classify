﻿using System.ComponentModel.DataAnnotations;


namespace Classify.Service.DTOs.Users
{
    public class UserForChangePasswordDto
    {
        [Required(ErrorMessage = "PhoneNumber is requaried!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Old password must not be null or empty!")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password must not be null or empty!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirming password must not be null or empty!")]
        [Compare("NewPassword")]
        public string ComfirmPassword { get; set; }
    }
}
