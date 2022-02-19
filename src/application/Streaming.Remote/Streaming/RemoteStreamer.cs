﻿using Microsoft.AspNetCore.SignalR.Client;
using Super.Paula.Application.Administration.Responses;
using Super.Paula.Application.Communication.Responses;
using Super.Paula.Authorization;
using Super.Paula.Environment;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Super.Paula.Application.Streaming
{
    public sealed class RemoteStreamer : IStreamer, IDisposable
    {
        private readonly HubConnection _connection;
        private readonly AppSettings _appSettings;
        private readonly ClaimsPrincipal _claimsPrincipal;

        private bool _disposed;

        public RemoteStreamer(AppSettings appSettings, ClaimsPrincipal claimsPrincipal)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(
                    new Uri(new Uri(appSettings.Server), "/stream"),
                    options => options.AccessTokenProvider = CreateTokenAsync)
                .Build();

            _appSettings = appSettings;
            _claimsPrincipal = claimsPrincipal;
        }

        [SuppressMessage("Reliability", "CA2012")]
        public void Dispose()
        {
            _ = _connection.DisposeAsync();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private Task<string?> CreateTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(_appSettings.StreamerSecret))
            {
                Task.FromResult((string?)null);
            }

            var token = new Token
            {
                StreamerSecret = _appSettings.StreamerSecret
            };

            return Task.FromResult(token.ToBase64String())!;
        }

        private async Task EnsureStartedAsync()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync();
            }
        }

        public async Task StreamNotificationCreationAsync(NotificationResponse response)
        {
            if (_disposed)
            {
                return;
            }

            await EnsureStartedAsync();
            var userId = $"{_claimsPrincipal.GetOrganization()}:{response.Inspector}";
            await _connection.SendAsync("Stream1", userId, "NotificationCreation", response);
        }

        public async Task StreamNotificationDeletionAsync(string inspector, int date, int time)
        {
            if (_disposed)
            {
                return;
            }

            await EnsureStartedAsync();
            var userId = $"{_claimsPrincipal.GetOrganization()}:{inspector}";
            await _connection.SendAsync("Stream3", userId, "NotificationDeletion", inspector, date, time);
        }

        public async Task StreamInspectorBusinessObjectCreationAsync(string inspector, InspectorBusinessObjectResponse response)
        {
            if (_disposed)
            {
                return;
            }

            await EnsureStartedAsync();
            var userId = $"{_claimsPrincipal.GetOrganization()}:{inspector}";
            await _connection.SendAsync("Stream2", userId, "InspectorBusinessObjectCreation", inspector, response);
        }

        public async Task StreamInspectorBusinessObjectUpdateAsync(string inspector, InspectorBusinessObjectResponse response)
        {
            if (_disposed)
            {
                return;
            }

            await EnsureStartedAsync();
            var userId = $"{_claimsPrincipal.GetOrganization()}:{inspector}";
            await _connection.SendAsync("Stream2", userId, "InspectorBusinessObjectUpdate", inspector, response);
        }

        public async Task StreamInspectorBusinessObjectDeletionAsync(string inspector, string businessObject)
        {
            if (_disposed)
            {
                return;
            }

            await EnsureStartedAsync();
            var userId = $"{_claimsPrincipal.GetOrganization()}:{inspector}";
            await _connection.SendAsync("Stream2", userId, "InspectorBusinessObjectDeletion", inspector, businessObject);
        }
    }
}
