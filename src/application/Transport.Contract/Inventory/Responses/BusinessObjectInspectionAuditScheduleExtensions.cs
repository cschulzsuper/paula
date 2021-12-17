﻿using System.Collections.Generic;
using System.Linq;

namespace Super.Paula.Application.Inventory.Responses
{
    public static class BusinessObjectInspectionAuditScheduleExtensions
    {
        public static BusinessObjectInspectionAuditScheduleResponse ToResponse(this BusinessObjectInspectionAuditSchedule auditSchedule)
        {
            var response = new BusinessObjectInspectionAuditScheduleResponse
            {
                CronExpression = auditSchedule.CronExpression,
            };

            return response;
        }

        public static ISet<BusinessObjectInspectionAuditScheduleResponse> ToResponse(this IEnumerable<BusinessObjectInspectionAuditSchedule> auditSchedule)
            => auditSchedule
                .Select(ToResponse)
                .ToHashSet();
    }
}
