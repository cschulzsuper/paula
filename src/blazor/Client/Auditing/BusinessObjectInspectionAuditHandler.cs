﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Super.Paula.Application.Auditing;
using Super.Paula.Application.Auditing.Requests;
using Super.Paula.Application.Auditing.Responses;
using Super.Paula.Authentication;
using Super.Paula.Client.ErrorHandling;
using Super.Paula.Environment;

namespace Super.Paula.Client.Auditing
{
    internal class BusinessObjectInspectionAuditHandler : IBusinessObjectInspectionAuditHandler
    {
        private readonly PaulaAuthenticationStateManager _paulaAuthenticationStateManager;
        private readonly HttpClient _httpClient;

        public BusinessObjectInspectionAuditHandler(
            HttpClient httpClient,
            PaulaAuthenticationStateManager paulaAuthenticationStateManager,
            AppSettings appSettings)
        {
            _paulaAuthenticationStateManager = paulaAuthenticationStateManager;
            _paulaAuthenticationStateManager.AuthenticationStateChanged += AuthenticationStateChanged;

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.Server);
            SetBearerOnHttpClient();
        }

        private void AuthenticationStateChanged(Task<AuthenticationState> task)
            => task.ContinueWith(_ =>
            {
                SetBearerOnHttpClient();
            });

        private void SetBearerOnHttpClient()
        {
            var bearer = _paulaAuthenticationStateManager.GetAuthenticationBearer();

            _httpClient.DefaultRequestHeaders.Authorization = !string.IsNullOrWhiteSpace(bearer)
                    ? new AuthenticationHeaderValue("Bearer", bearer)
                    : null;
        }

        public async ValueTask<BusinessObjectInspectionAuditResponse> CreateAsync(string businessObject, BusinessObjectInspectionAuditRequest request)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync($"business-objects/{businessObject}/inspection-audits", request);
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            return (await responseMessage.Content.ReadFromJsonAsync<BusinessObjectInspectionAuditResponse>())!;
        }

        public async ValueTask DeleteAsync(string businessObject, string inspection, int date, int time)
        {
            var responseMessage = await _httpClient.DeleteAsync($"business-objects/{businessObject}/inspection-audits/{inspection}/{date}/{time}");
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();
        }

        public async IAsyncEnumerable<BusinessObjectInspectionAuditResponse> GetAll()
        {
            var responseMessage = await _httpClient.GetAsync("inspection-audits");
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            var response = JsonSerializer.DeserializeAsyncEnumerable<BusinessObjectInspectionAuditResponse>(
                responseStream,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultBufferSize = 128
                });

            await foreach (var reponseItem in response)
            {
                yield return reponseItem!;
            }
        }

        public async IAsyncEnumerable<BusinessObjectInspectionAuditResponse> GetAllForBusinessObject(string businessObject)
        {
            var responseMessage = await _httpClient.GetAsync($"business-objects/{businessObject}/inspection-audits");
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            var response = JsonSerializer.DeserializeAsyncEnumerable<BusinessObjectInspectionAuditResponse>(
                responseStream,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultBufferSize = 128
                });

            await foreach (var reponseItem in response)
            {
                yield return reponseItem!;
            }
        }

        public async IAsyncEnumerable<BusinessObjectInspectionAuditResponse> Search(string? businessObject, string? inspector, string? inspection)
        {
            var queryValues = new List<string?>
            {
                businessObject != null ? $"business-object={businessObject}" : null,
                inspector != null ? $"inspector={inspector}" : null,
                inspection != null ? $"inspection={inspection}" : null,
            };

            var query = $"?{string.Join('&', queryValues.Where(x => x != null))}";

            var responseMessage = await _httpClient.GetAsync($"inspection-audits/search{query}");
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            var response = JsonSerializer.DeserializeAsyncEnumerable<BusinessObjectInspectionAuditResponse>(
                responseStream,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultBufferSize = 128
                });

            await foreach (var reponseItem in response)
            {
                yield return reponseItem!;
            }
        }

        public async IAsyncEnumerable<BusinessObjectInspectionAuditResponse> SearchForBusinessObject(string businessObject, string? inspector, string? inspection)
        {
            var queryValues = new List<string?>
            {
                inspector != null ? $"inspector={inspector}" : null,
                inspection != null ? $"inspection={inspection}" : null,
            };

            var query = $"?{string.Join('&', queryValues.Where(x => x != null))}";

            var responseMessage = await _httpClient.GetAsync($"business-objects/{businessObject}/inspection-audits/search{query}");
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            var response = JsonSerializer.DeserializeAsyncEnumerable<BusinessObjectInspectionAuditResponse>(
                responseStream,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultBufferSize = 128
                });

            await foreach (var reponseItem in response)
            {
                yield return reponseItem!;
            }
        }

        public async ValueTask<BusinessObjectInspectionAuditResponse> GetAsync(string businessObject, string inspection, int date, int time)
        {
            var responseMessage = await _httpClient.GetAsync($"business-objects/{businessObject}/inspection-audits/{inspection}/{date}/{time}");
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();

            return (await responseMessage.Content.ReadFromJsonAsync<BusinessObjectInspectionAuditResponse>())!;
        }

        public async ValueTask ReplaceAsync(string businessObject, string inspection, int date, int time, BusinessObjectInspectionAuditRequest request)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync($"business-objects/{businessObject}/inspection-audits/{inspection}/{date}/{time}", request);
            
            responseMessage.RuleOutProblems();
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}