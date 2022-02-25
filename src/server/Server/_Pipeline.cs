﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Super.Paula.Application;
using Super.Paula.Application.Operation;
using Super.Paula.Application.Orchestration;
using Super.Paula.Authorization;
using Super.Paula.Data;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Super.Paula
{
    [SuppressMessage("Style", "IDE1006")]
    public static class _Pipeline
    {
        public static IApplicationBuilder ConfigureEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Configure((context) => context.Services.ConfigureData());
            eventBus.ConfigureTransport();

            return app;
        }

        public static IApplicationBuilder ConfigureWorker(this IApplicationBuilder app)
        {
            var workerRegistry = app.ApplicationServices.GetRequiredService<IWorkerRegistry>();

            workerRegistry.ConfigureWorkflows();
            workerRegistry.ConfigureIntegration();

            return app;
        }

        public static IApplicationBuilder UseRequestUser(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.RequestServices.ConfigureUser(context.User);

                await next.Invoke(context);
            });

            return app;
        }

        public static IApplicationBuilder UseBlacklist(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.Use(async (context, next) =>
            {
                var connectionViolationManager = context.RequestServices.GetRequiredService<IConnectionViolationManager>();
                var connectionIpAddress = context.Connection.RemoteIpAddress?.ToString();

                var blacklisted =
                    connectionIpAddress != null &&
                    connectionViolationManager.Verify(connectionIpAddress);

                if (blacklisted)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }

                await next.Invoke(context);
            });

            return app;
        }

        public static IApplicationBuilder UseEndpointState(this IApplicationBuilder app)
            => app.Use((context, next) =>
            {
                var endpoint = context.GetEndpoint();
                var useOrganizationFromRoute = endpoint?.Metadata.GetMetadata<UseOrganizationFromRouteAttribute>() != null;

                if (useOrganizationFromRoute)
                {
                    context.Request.RouteValues.TryGetValue("organization", out var organization);
                    context.RequestServices.ConfigureData($"{organization}", string.Empty);
                }
                else
                {
                    context.RequestServices.ConfigureData();
                }

                return next();

            });
    }
}