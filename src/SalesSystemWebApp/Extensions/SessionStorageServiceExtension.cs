using Blazored.SessionStorage;
using Microsoft.JSInterop;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SalesSystemWebApp.Extensions
{
    public static class SessionStorageServiceExtension
    {
        private static readonly string KeyBase64; 

        static SessionStorageServiceExtension()
        {
            var keyBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            KeyBase64 = Convert.ToBase64String(keyBytes);
        }

        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService, string key, T item, IJSRuntime jsRuntime)
        {
            if (sessionStorageService == null)
                throw new ArgumentNullException(nameof(sessionStorageService));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (jsRuntime == null)
                throw new ArgumentNullException(nameof(jsRuntime));

            try
            {
                var itemJson = JsonSerializer.Serialize(item);

                var encryptedBase64 = await jsRuntime.InvokeAsync<string>("encryptData", itemJson, KeyBase64);

                await sessionStorageService.SetItemAsync(key, encryptedBase64);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to encrypt and save item.", ex);
            }
        }

        public static async Task<T?> ReadEncryptedItemAsync<T>(this ISessionStorageService sessionStorageService, string key, IJSRuntime jsRuntime)
        {
            if (sessionStorageService == null)
                throw new ArgumentNullException(nameof(sessionStorageService));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (jsRuntime == null)
                throw new ArgumentNullException(nameof(jsRuntime));

            try
            {
                var encryptedBase64 = await sessionStorageService.GetItemAsync<string>(key);
                if (string.IsNullOrEmpty(encryptedBase64))
                {
                    return default;
                }

                var decryptedJson = await jsRuntime.InvokeAsync<string>("decryptData", encryptedBase64, KeyBase64);

                return JsonSerializer.Deserialize<T>(decryptedJson);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to read and decrypt item.", ex);
            }
        }
    }
}