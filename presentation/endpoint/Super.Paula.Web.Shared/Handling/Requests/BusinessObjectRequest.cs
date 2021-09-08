﻿using Super.Paula.Shared.Validation;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Super.Paula.Web.Shared.Handling.Requests
{
    public class BusinessObjectRequest
    {
        [Required]
        [KebabCase]
        [StringLength(140)]
        public string UniqueName { get; set; } = string.Empty;

        [KebabCase]
        [StringLength(140)]
        public string Inspector { get; set; } = string.Empty;

        [StringLength(140)]
        public string DisplayName { get; set; } = string.Empty;

    }
}