using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory.Demo.Clients
{
    public class TypedOrderClient
    {
        private HttpClient _client;
        public TypedOrderClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> Get()
        {
            return await _client.GetStringAsync("/order");
        }
    }
}
