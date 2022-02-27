﻿using Super.Paula.Application.Administration.Requests;
using Super.Paula.Application.Administration.Responses;
using Super.Paula.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Super.Paula.Application.Administration
{
    public class InspectorHandler : IInspectorHandler
    {
        private readonly IInspectorManager _inspectorManager;
        private readonly IOrganizationManager _organizationManager;
        private readonly IIdentityInspectorManager _identityInspectorManager;
        private readonly ClaimsPrincipal _user;
        private readonly IInspectorAnnouncer _inspectorAnnouncer;

        public InspectorHandler(
            IInspectorManager inspectorManager,
            IOrganizationManager organizationManager,
            IIdentityInspectorManager identityInspectorManager,
            ClaimsPrincipal claimsPrincipal,
            IInspectorAnnouncer inspectorAnnouncer)
        {
            _inspectorManager = inspectorManager;
            _organizationManager = organizationManager;
            _identityInspectorManager = identityInspectorManager;
            _user = claimsPrincipal;
            _inspectorAnnouncer = inspectorAnnouncer;
        }

        public async ValueTask<InspectorResponse> CreateAsync(InspectorRequest request)
        {
            var organization = await _organizationManager.GetAsync(_user.GetOrganization());

            var entity = new Inspector
            {
                Identity = request.Identity,
                UniqueName = request.UniqueName,
                Activated = request.Activated,
                Organization = organization.UniqueName,
                OrganizationActivated = organization.Activated,
                OrganizationDisplayName = organization.DisplayName
            };

            await _inspectorManager.InsertAsync(entity);

            var identity = new IdentityInspector
            {
                Activated = entity.Activated && organization.Activated,
                Inspector = entity.UniqueName,
                Organization = organization.UniqueName,
                UniqueName = entity.Identity
            };

            await _identityInspectorManager.InsertAsync(identity);

            return new InspectorResponse
            {
                Identity = entity.Identity,
                UniqueName = entity.UniqueName,
                Activated = entity.Activated,
                BusinessObjects = entity.BusinessObjects.ToResponse(),
                ETag = entity.ETag
            };
        }

        public async ValueTask DeleteAsync(string inspector, string etag)
        {
            var entity = await _inspectorManager.GetAsync(inspector);

            entity.ETag = etag;

            await _inspectorManager.DeleteAsync(entity);

            var identity = await _identityInspectorManager.GetAsync(
                entity.Identity,
                entity.Organization,
                entity.UniqueName);
            await _identityInspectorManager.DeleteAsync(identity);
        }

        public IAsyncEnumerable<InspectorResponse> GetAll()
            => _inspectorManager
                .GetAsyncEnumerable(query => query
                .Select(entity => new InspectorResponse
                {
                    Identity = entity.Identity,
                    UniqueName = entity.UniqueName,
                    Activated = entity.Activated,
                    BusinessObjects = entity.BusinessObjects.ToResponse(),
                    ETag = entity.ETag
                }));

        public async ValueTask<InspectorResponse> GetAsync(string inspector)
        {
            var entity = await _inspectorManager.GetAsync(inspector);

            return new InspectorResponse
            {
                Identity = entity.Identity,
                UniqueName = entity.UniqueName,
                Activated = entity.Activated,
                BusinessObjects = entity.BusinessObjects.ToResponse(),
                ETag = entity.ETag
            };
        }

        public async ValueTask<InspectorResponse> GetCurrentAsync()
        {
            var entity = await _inspectorManager.GetAsync(_user.GetInspector());

            return new InspectorResponse
            {
                Identity = entity.Identity,
                UniqueName = entity.UniqueName,
                Activated = entity.Activated,
                BusinessObjects = entity.BusinessObjects.ToResponse(),
                ETag = entity.ETag
            };
        }

        public async ValueTask ReplaceAsync(string inspector, InspectorRequest request)
        {
            var entity = await _inspectorManager.GetAsync(inspector);

            var oldIdentity = entity.Identity;

            entity.Identity = request.Identity;
            entity.UniqueName = request.UniqueName;
            entity.Activated = request.Activated;
            entity.ETag = request.ETag;

            await _inspectorManager.UpdateAsync(entity);

            var identity = await _identityInspectorManager.GetAsync(
                oldIdentity,
                entity.Organization,
                entity.UniqueName);

            await _identityInspectorManager.DeleteAsync(identity);

            identity = new IdentityInspector
            {
                Activated = entity.Activated && entity.OrganizationActivated,
                Inspector = entity.UniqueName,
                Organization = entity.Organization,
                UniqueName = entity.Identity
            };

            await _identityInspectorManager.InsertAsync(identity);
        }

        public async ValueTask<ActivateInspectorResponse> ActivateAsync(string inspector, string etag)
        {
            var entity = await _inspectorManager.GetAsync(inspector);

            entity.Activated = true;
            entity.ETag = etag;

            await _inspectorManager.UpdateAsync(entity);

            var identity = await _identityInspectorManager.GetAsync(
                entity.Identity,
                entity.Organization,
                entity.UniqueName);

            identity.Activated = entity.OrganizationActivated;

            await _identityInspectorManager.UpdateAsync(identity);

            return new ActivateInspectorResponse
            {
                ETag = entity.ETag
            };
        }

        public async ValueTask<DeactivateInspectorResponse> DeactivateAsync(string inspector, string etag)
        {
            var entity = await _inspectorManager.GetAsync(inspector);

            entity.Activated = false;
            entity.ETag = etag;

            await _inspectorManager.UpdateAsync(entity);

            var identity = await _identityInspectorManager.GetAsync(
                entity.Identity,
                entity.Organization,
                entity.UniqueName);

            identity.Activated = false;

            await _identityInspectorManager.UpdateAsync(identity);

            return new DeactivateInspectorResponse
            {
                ETag = entity.ETag
            };
        }

        public IAsyncEnumerable<InspectorResponse> GetAllForOrganization(string organization)
            => _inspectorManager
               .GetAsyncEnumerable(query => query
                   .Where(x => x.Organization == organization)
                   .Select(entity => new InspectorResponse
                   {
                       Identity = entity.Identity,
                       UniqueName = entity.UniqueName,
                       Activated = entity.Activated,
                       BusinessObjects = entity.BusinessObjects.ToResponse(),
                       ETag = entity.ETag
                   }));

        public IAsyncEnumerable<IdentityInspectorResponse> GetAllForIdentity(string identity)
            => _identityInspectorManager
                .GetIdentityBasedAsyncEnumerable(identity,
                    query => query
                       .Select(entity => new IdentityInspectorResponse
                       {
                           Identity = entity.UniqueName,
                           UniqueName = entity.Inspector,
                           Activated = entity.Activated,
                           Organization = entity.Organization
                       }));

        public Task<IDisposable> OnBusinessObjectCreationAsync(Func<string, InspectorBusinessObjectResponse, Task> handler)
            => _inspectorAnnouncer.OnBusinessObjectCreationAsync(handler);

        public Task<IDisposable> OnBusinessObjectUpdateAsync(Func<string, InspectorBusinessObjectResponse, Task> handler)
            => _inspectorAnnouncer.OnBusinessObjectUpdateAsync(handler);

        public Task<IDisposable> OnBusinessObjectDeletionAsync(Func<string, string, Task> handler)
            => _inspectorAnnouncer.OnBusinessObjectDeletionAsync(handler);
    }
}