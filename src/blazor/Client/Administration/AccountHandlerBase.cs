﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Super.Paula.Application.Administration;
using Super.Paula.Application.Administration.Requests;
using Super.Paula.Application.Administration.Responses;
using Super.Paula.Client.ErrorHandling;
using Super.Paula.Environment;

namespace Super.Paula.Client.Administration
{
    public class AccountHandlerBase : IAccountHandler
    {
        private readonly HttpClient _httpClient;

        public AccountHandlerBase(
            HttpClient httpClient,
            AppSettings appSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.Server);
        }

        public async ValueTask ChangeSecretAsync(ChangeSecretRequest request)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("account/change-secret", request);
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();
        }

        public async ValueTask RegisterOrganizationAsync(RegisterOrganizationRequest request)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("account/register-organization", request);
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();
        }

        public async ValueTask<string> SignInInspectorAsync(SignInInspectorRequest request)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("account/sign-in-inspector", request);
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            return (await responseMessage.Content.ReadAsStringAsync())!;
        }

        public async ValueTask<string> StartImpersonationAsync(StartImpersonationRequest request)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("account/start-impersonation", request);
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            return (await responseMessage.Content.ReadAsStringAsync())!;
        }

        public async ValueTask<string> StopImpersonationAsync()
        {
            var responseMessage = await _httpClient.PostAsync("account/stop-impersonation", null);

            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            return (await responseMessage.Content.ReadAsStringAsync())!;
        }
    }
}