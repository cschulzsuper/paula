﻿using Super.Paula.Aggregates.Auditing;
using Super.Paula.Aggregates.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Paula.Management.Contract
{
    public interface IBusinessObjectInspectionAuditManager
    {
        ValueTask<BusinessObjectInspectionAudit> GetAsync(string businessObject, string inspection, int date, int time);

        IQueryable<BusinessObjectInspectionAudit> GetQueryable();
        IAsyncEnumerable<BusinessObjectInspectionAudit> GetAsyncEnumerable();
        IAsyncEnumerable<TResult> GetAsyncEnumerable<TResult>(Func<IQueryable<BusinessObjectInspectionAudit>, IQueryable<TResult>> query);

        IQueryable<BusinessObjectInspectionAudit> GetDateBasedQueryable(int date);
        IAsyncEnumerable<BusinessObjectInspectionAudit> GetDateBasedAsyncEnumerable(int date);
        IAsyncEnumerable<TResult> GetDateBasedAsyncEnumerable<TResult>(int date, Func<IQueryable<BusinessObjectInspectionAudit>, IQueryable<TResult>> query);

        ValueTask InsertAsync(BusinessObjectInspectionAudit businessObjectInspectionAudit);
        ValueTask UpdateAsync(BusinessObjectInspectionAudit businessObjectInspectionAudit);
        ValueTask DeleteAsync(BusinessObjectInspectionAudit businessObjectInspectionAudit);
    }
}
