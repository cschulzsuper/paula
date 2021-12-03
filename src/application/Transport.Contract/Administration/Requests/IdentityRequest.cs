﻿using System.ComponentModel.DataAnnotations;
using Super.Paula.Validation;

namespace Super.Paula.Application.Administration.Requests
{
    public class IdentityRequest
    {
        [Required]
        [KebabCase]
        [StringLength(140)]
        public string UniqueName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(140)]
        public string MailAddress { get; set; } = string.Empty;
    }
}