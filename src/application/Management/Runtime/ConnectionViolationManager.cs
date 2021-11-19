﻿using Super.Paula.RuntimeData;
using System.Collections.Concurrent;
using System.Net;

namespace Super.Paula.Application.Runtime
{
    public class ConnectionViolationManager : IConnectionViolationManager
    {
        private readonly IRuntimeCache<ConnectionViolation> _connectionViolationRuntimeCache;

        public ConnectionViolationManager(IRuntimeCache<ConnectionViolation> connectionViolationRuntimeCache)
        {
            _connectionViolationRuntimeCache=connectionViolationRuntimeCache;
        }

        public void Trace(string violator)
        {
            var violation = _connectionViolationRuntimeCache.Get(violator)
                ?? new ConnectionViolation
                {
                    Violator = violator,
                    ViolationCounter = 0
                };

            violation.ViolationCounter++;

            _connectionViolationRuntimeCache.Set(violation);
        }

        public bool Verify(string violator)
            => _connectionViolationRuntimeCache.Get(violator)?.ViolationCounter >= 3;

    }
}