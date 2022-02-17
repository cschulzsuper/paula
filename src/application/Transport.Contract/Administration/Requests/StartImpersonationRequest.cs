﻿using System.ComponentModel.DataAnnotations;
using Super.Paula.Validation;

namespace Super.Paula.Application.Administration.Requests
{
    public class StartImpersonationRequest
    {
        [Required]
        [KebabCase]
        [StringLength(140)]
        [UniqueName]

        public string Organization { get; set; } = string.Empty;

        [Required]
        [KebabCase]
        [StringLength(140)]
        [UniqueName]
        public string UniqueName { get; set; } = string.Empty;
    }
}