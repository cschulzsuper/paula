﻿using Blazored.LocalStorage;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Super.Paula.Client.Storage
{
    public class DefaultLocalStorage : ILocalStorage
    {
        private readonly ILocalStorageService _localStorageService;

        public DefaultLocalStorage(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
            => _localStorageService.ContainKeyAsync(key);

        public async ValueTask<T?> GetItemAsync<T>(string key, CancellationToken? cancellationToken = null)
        {
            var result = await _localStorageService.GetItemAsStringAsync(key);
            
            var deserializedResult = result != null
                ? JsonSerializer.Deserialize<T>(result)
                : default;

            return deserializedResult;
        }

        public ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
            => _localStorageService.RemoveItemAsync(key);

        public async ValueTask SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null)
        {
            var serializedData = JsonSerializer.Serialize(data);
            await _localStorageService.SetItemAsStringAsync(key, serializedData);
        }
    }
}