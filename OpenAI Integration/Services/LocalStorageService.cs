using Microsoft.JSInterop;
using Newtonsoft.Json;
using OpenAI_Integration.Interfaces;
using OpenAI_Integration.Models;

namespace OpenAI_Integration.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime jSRuntime;

        public LocalStorageService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async Task<string> Get(string key)
        {
            return await this.jSRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async void Set(string key, string value)
        {
            await this.jSRuntime.InvokeVoidAsync("localStorage.SetItem", key, value);
        }

        public void Append(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
