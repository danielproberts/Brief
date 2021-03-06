﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Brief.Models.Manage
{
    public class EmailModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }

        [Display(Name = "Confirm New Email")]
        public string ConfirmEmail { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

    }
}
