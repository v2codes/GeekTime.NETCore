using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory.Demo.Clients
{
    public class OrderClient
    {
        private IHttpClientFactory _httpClientFactory;
        public OrderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get()
        {
            // 创建 HttpClient
            var client = _httpClientFactory.CreateClient();

            //发起 HTTP 请求
            return await client.GetStringAsync("https://localhost:5004/order"); 
        }
    }
}
