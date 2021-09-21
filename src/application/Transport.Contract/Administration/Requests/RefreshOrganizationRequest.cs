﻿using System.ComponentModel.DataAnnotations;

namespace Super.Paula.Application.Administration.Requests
{
    public class RefreshOrganizationRequest
    {
        [Required]
        [StringLength(140)]
        public string DisplayName { get; set; } = string.Empty;

        public bool Activated { get; set; }
    }
}