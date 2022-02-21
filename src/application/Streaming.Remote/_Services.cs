﻿using Microsoft.Extensions.DependencyInjection;
using Super.Paula.Application.Streaming;
using System.Diagnostics.CodeAnalysis;

namespace Super.Paula.Application
{
    [SuppressMessage("Style", "IDE1006")]
    public static class _Services
    {
        public static IServiceCollection AddRemoteStreaming(this IServiceCollection services)
            => services
                .AddScoped<IStreamer, RemoteStreamer>();
    }
}
