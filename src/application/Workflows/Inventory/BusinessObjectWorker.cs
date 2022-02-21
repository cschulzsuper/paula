﻿using Microsoft.Extensions.DependencyInjection;
using Super.Paula.Application.Administration;
using Super.Paula.Application.Orchestration;
using Super.Paula.Authorization;
using Super.Paula.Data;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Super.Paula.Application.Inventory
{
    public class BusinessObjectWorker : IWorker
    {
        private readonly int timeInspectionAuditIterationDelay = 3600_000; // 1 hour

        public async Task ExecuteAsync(WorkerContext context, CancellationToken cancellationToken)
        {
            var organizations = context.Services.GetRequiredService<IOrganizations>();

            while (!cancellationToken.IsCancellationRequested)
            {
                var organizationUniqueNames = organizations.GetAllUniqueNames();

                foreach (var organizaion in organizationUniqueNames)
                {
                    using var serviceScope = context.Services.CreateScope();
                    SetupScope(organizaion, serviceScope);

                    var businessObjects = serviceScope.ServiceProvider
                        .GetRequiredService<IBusinessObjects>()
                        .GetAllUniqueNames();

                    foreach (var businessObject in businessObjects)
                    {
                        var businessObjectHandler = serviceScope.ServiceProvider
                            .GetRequiredService<IBusinessObjectHandler>();

                        await businessObjectHandler.TimeInspectionAuditAsync(businessObject);
                    }
                }

                await Task.Delay(timeInspectionAuditIterationDelay, cancellationToken);
            }
        }

        private static void SetupScope(string organization, IServiceScope scope)
        {
            scope.ServiceProvider.ConfigureUser(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>
                        {
                                new Claim("Organization", organization)
                        })));

            scope.ServiceProvider.ConfigureData();
        }
    }
}
