﻿using System.ComponentModel.DataAnnotations;
using Super.Paula.Validation;

namespace Super.Paula.Application.Inventory.Events
{
    public class BusinessObjectInspectionAuditEvent
    {
        [StringLength(140)]
        public string? BusinessObjectDisplayName { get; set; }

        [KebabCase]
        [StringLength(140)]
        public string AuditInspector { get; set; } = string.Empty;

        [KebabCase] 
        [StringLength(140)] 
        public string Inspection { get; set; } = string.Empty;

        [StringLength(140)]
        public string? InspectionDisplayName { get; set; }

        [StringLength(4000)]
        public string? AuditAnnotation { get; set; }

        [AuditResult]
        public string? AuditResult { get; set; }

        [DayNumber]
        public int AuditDate { get; set; }

        [Milliseconds]
        public int AuditTime { get; set; }
    }
}