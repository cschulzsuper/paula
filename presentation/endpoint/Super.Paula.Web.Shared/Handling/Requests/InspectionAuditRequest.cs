﻿using Super.Paula.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Super.Paula.Web.Shared.Handling.Requests
{
    public class InspectionAuditRequest
    {
        [StringLength(4000)]
        public string Annotation { get; set; } = string.Empty;

        public int AuditDate { get; set; }

        public int AuditTime { get; set; }

        [Required]
        [KebabCase]
        [StringLength(140)]
        public string BusinessObject { get; set; } = string.Empty;

        [Required]
        [KebabCase]
        [StringLength(140)]
        public string Inspection { get; set; } = string.Empty;

        [Required]
        [KebabCase]
        [StringLength(140)]
        public string Inspector { get; set; } = string.Empty;

        [Required]
        [StringRange("satisfying", "insufficient", "failed")]
        public string Result { get; set; } = string.Empty;

        [StringLength(140)]
        public string BusinessObjectDisplayName { get; set; } = string.Empty;

        [StringLength(140)]
        public string InspectionDisplayName { get; set; } = string.Empty;
    }
}