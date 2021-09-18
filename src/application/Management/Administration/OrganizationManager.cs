﻿using Super.Paula.Data;
using Super.Paula.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Super.Paula.Application.Administration
{
    public class  OrganizationManager : IOrganizationManager
    {
        private readonly IRepository<Organization> _organizationRepository;

        public OrganizationManager(IRepository<Organization> organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public ValueTask<Organization> GetAsync(string organization)
        {
            EnsureGetable(organization);
            return _organizationRepository.GetByIdAsync(organization);
        }

        public ValueTask InsertAsync(Organization organization)
        {
            EnsureInsertable(organization);
            return _organizationRepository.InsertAsync(organization);
        }

        public ValueTask UpdateAsync(Organization organization)
        {
            EnsureUpdateable(organization);
            return _organizationRepository.UpdateAsync(organization);
        }

        public ValueTask DeleteAsync(Organization organization)
        {
            EnsureDeleteable(organization);
            return _organizationRepository.DeleteAsync(organization);
        }

        public IQueryable<Organization> GetQueryable()
            => _organizationRepository.GetQueryable();

        public IAsyncEnumerable<Organization> GetAsyncEnumerable()
            => _organizationRepository.GetAsyncEnumerable();

        public IAsyncEnumerable<TResult> GetAsyncEnumerable<TResult>(Func<IQueryable<Organization>, IQueryable<TResult>> query)
            => _organizationRepository.GetAsyncEnumerable(query);

        private void EnsureGetable(string organization)
            => Validator.Ensure(
                OrganizationValidator.OrganizationHasValue(organization),
                OrganizationValidator.OrganizationHasKebabCase(organization),
                OrganizationValidator.OrganizationExists(organization, GetQueryable()));

        private void EnsureInsertable(Organization organization)
            => Validator.Ensure(
                OrganizationValidator.UniqueNameHasValue(organization),
                OrganizationValidator.UniqueNameHasKebabCase(organization),
                OrganizationValidator.ChiefInspectorIsNotNull(organization),
                OrganizationValidator.ChiefInspectorHasKebabCase(organization),
                OrganizationValidator.UniqueNameIsUnqiue(organization, GetQueryable()));

        private void EnsureUpdateable(Organization organization)
            => Validator.Ensure(
                OrganizationValidator.UniqueNameHasValue(organization),
                OrganizationValidator.UniqueNameHasKebabCase(organization),
                OrganizationValidator.ChiefInspectorIsNotNull(organization),
                OrganizationValidator.ChiefInspectorHasKebabCase(organization),
                OrganizationValidator.UniqueNameExists(organization, GetQueryable()));

        private void EnsureDeleteable(Organization organization)
            => Validator.Ensure(
                OrganizationValidator.UniqueNameHasValue(organization),
                OrganizationValidator.UniqueNameHasKebabCase(organization),
                OrganizationValidator.UniqueNameExists(organization, GetQueryable()));
    }
}