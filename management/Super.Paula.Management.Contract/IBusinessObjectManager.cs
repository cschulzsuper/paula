﻿using Super.Paula.Aggregates.Inventory;

namespace Super.Paula.Management.Contract
{
    public interface IBusinessObjectManager
    {
        ValueTask<BusinessObject> GetAsync(string businessObject);
        IQueryable<BusinessObject> GetQueryable();
        IAsyncEnumerable<BusinessObject> GetAsyncEnumerable();
        IAsyncEnumerable<TResult> GetAsyncEnumerable<TResult>(Func<IQueryable<BusinessObject>, IQueryable<TResult>> query);

        ValueTask InsertAsync(BusinessObject businessObject);
        ValueTask UpdateAsync(BusinessObject businessObject);
        ValueTask DeleteAsync(BusinessObject businessObject);
    }
}