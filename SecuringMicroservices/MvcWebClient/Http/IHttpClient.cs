using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcWebClient.Http
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string url);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);

    }
}
