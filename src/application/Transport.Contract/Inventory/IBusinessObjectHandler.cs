﻿using Super.Paula.Application.Inventory.Requests;
using Super.Paula.Application.Inventory.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Super.Paula.Application.Inventory
{
    public interface IBusinessObjectHandler
    {
        ValueTask<BusinessObjectResponse> GetAsync(string businessObject);
        
        IAsyncEnumerable<BusinessObjectResponse> GetAll();
        IAsyncEnumerable<BusinessObjectResponse> GetAllForInspector(string inspector);
        IAsyncEnumerable<BusinessObjectResponse> Search(string? businessObject, string? inspector);

        ValueTask<BusinessObjectResponse> CreateAsync(BusinessObjectRequest request);
        ValueTask ReplaceAsync(string businessObject, BusinessObjectRequest request);
        ValueTask DeleteAsync(string businessObject);

        ValueTask AssignInspectionAsync(string businessObject, AssignInspectionRequest request);
        ValueTask CancelInspectionAsync(string businessObject, CancelInspectionRequest request);
        ValueTask ScheduleInspectionAuditAsync(string businessObject, string inspection, ScheduleInspectionAuditRequest request);
        ValueTask PostponeInspectionAuditAsync(string businessObject, string inspection, PostponeInspectionAuditRequest request);

        ValueTask CreateInspectionAuditAsync(string businessObject, CreateInspectionAuditRequest request);
        ValueTask ChangeInspectionAuditAsync(string businessObject, string inspection, ChangeInspectionAuditRequest request);
        ValueTask AnnotateInspectionAuditAsync(string businessObject, string inspection, AnnotateInspectionAuditRequest request);
    }
}