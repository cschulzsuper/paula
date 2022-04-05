﻿using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Super.Paula.Authorization;

namespace Super.Paula.Application.Orchestration
{
    public class PersistentContinuationStorage : IContinuationStorage
    {
        private readonly ILogger<PersistentContinuationStorage> _logger;
        private readonly IContinuationManager _continuationManager;
        private readonly IContinuationRegistry _continuationRegistry;

        public PersistentContinuationStorage(
            ILogger<PersistentContinuationStorage> logger,
            IContinuationManager continuationManager,
            IContinuationRegistry continuationRegistry)
        {
            _logger = logger;
            _continuationManager = continuationManager;
            _continuationRegistry = continuationRegistry;
        }

        public async ValueTask AddAsync(ContinuationBase continuation, ClaimsPrincipal user)
        {
            var entity = new Continuation
            {
                Name = continuation.Name,
                CreationTime = continuation.CreationTime,
                CreationDate = continuation.CreationDate,
                Id = continuation.Id.ToString(),
                Data = Base64Encoder.ObjectToBase64(continuation),
                User = Base64Encoder.ObjectToBase64(user.ToToken()),
                OperationId = Activity.Current?.RootId ?? Guid.NewGuid().ToString(),
            };

            await _continuationManager.InsertAsync(entity);

            _logger.LogInformation("A continuation has been added ({continuation}, {user})", continuation, user);
        }

        public async IAsyncEnumerable<(ContinuationBase, ClaimsPrincipal)> GetPendingContinuationsAsync()
        {
            var continuations = _continuationManager
                .GetAsyncEnumerable(query => query
                    .Where(x => x.State == string.Empty));

            await foreach(var continuation  in continuations)
            {
                var ContinuationsType = _continuationRegistry.GetContinuationsType(continuation.Name);

                if (ContinuationsType == null)
                {
                    _logger.LogWarning("Could not get Continuations type for a Continuations ({ContinuationsName},{user}).", continuation);
                    continue;
                }

                var data = (ContinuationBase)Base64Encoder.Base64ToObject(continuation.Data, ContinuationsType);

                var user = new ClaimsPrincipal(
                    new ClaimsIdentity(
                        Base64Encoder.Base64ToObject<Token>(continuation.User).ToClaims()));

                yield return (data, user);
            }
        }

        public async ValueTask SetContinuationAsInProgressAsync(Guid continuationId)
        {
            var continuation = _continuationManager.GetQueryable()
                .SingleOrDefault(x => x.Id == continuationId.ToString());

            if (continuation != null)
            {
                continuation.State = "in-progress";
                await _continuationManager.UpdateAsync(continuation);

                _logger.LogInformation("A continuation has been marked as in progress ({continuationId})", continuationId);
            }
            else
            {
                _logger.LogWarning("An unkown continuation has been marked as in progress ({continuationId})", continuationId);
            }
        }

        public async ValueTask SetContinuationCompletionAsync(Guid continuationId)
        {
            var continuation = _continuationManager.GetQueryable()
                .SingleOrDefault(x => x.Id == continuationId.ToString());

            if (continuation != null)
            {
                await _continuationManager.DeleteAsync(continuation);

                _logger.LogInformation("A continuation has been marked as completed ({continuationId})", continuationId);
            }
            else
            {
                _logger.LogWarning("A unkown continuation has been marked as completed ({continuationId})", continuationId);
            }
        }

        public async ValueTask SetContinuationFailureAsync(Guid continuationId, Exception? exception)
        {
            var continuation = _continuationManager.GetQueryable()
                .SingleOrDefault(x => x.Id == continuationId.ToString());

            if (continuation != null)
            {
                continuation.State = "failed";
                await _continuationManager.UpdateAsync(continuation);

                _logger.LogWarning(exception, "An continuation has been marked as failed ({continuationId})", continuationId);

            }
            else
            {
                _logger.LogWarning(exception, "An unkown continuation has been marked as failed ({continuationId})", continuationId);
            }
        }
    }
}
