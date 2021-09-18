﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace Super.Paula.Authentication
{
    public class PaulaAuthenticationStateManager : AuthenticationStateProvider
    {
        private ISet<string> _authorizationFilter = ImmutableHashSet.Create<string>();
        private string _authenticationBrearer = string.Empty;

        public PaulaAuthenticationStateManager()
        {

        }

        public ISet<string> GetAuthorizationsFilter()
            => _authorizationFilter;

        public void SetAuthorizationsFilter(params string[] authorizations)
        {
            _authorizationFilter = ImmutableHashSet.CreateRange(authorizations);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public string GetAuthenticationBearer()
            => _authenticationBrearer;

        public void SetAuthenticationBearer(string brearer)
        {
            _authenticationBrearer = brearer;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
            => Task.FromResult(
                new AuthenticationState(
                    new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new[] {
                                new Claim(
                                    Guid.NewGuid().ToString(),
                                    Guid.NewGuid().ToString())
                            }))));
    }
}
