﻿using System.Collections.Generic;

namespace Super.Paula.Application.Inventory
{
    public class BusinessObjectInspection
    {
        public bool Activated { get; set; } = false;
        public bool ActivatedGlobally { get; set; } = false;

        public string UniqueName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public int AssignmentTime { get; set; }
        public int AssignmentDate { get; set; }

        public ISet<BusinessObjectInspectionAuditSchedule> AuditSchedules { get; set; } = new HashSet<BusinessObjectInspectionAuditSchedule>();
        public int AuditDelayThreshold { get; set; }
        public int AuditThreshold { get; set; }

        public int AuditTime { get; set; }
        public int AuditDate { get; set; }
        public string AuditInspector { get; set; } = string.Empty;
        public string AuditResult { get; set; } = string.Empty;
        public string AuditAnnotation { get; set; } = string.Empty;
    }
}