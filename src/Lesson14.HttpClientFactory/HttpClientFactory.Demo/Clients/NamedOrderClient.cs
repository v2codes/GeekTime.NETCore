using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory.Demo.Clients
{
    public class NamedOrderClient
    {
        const string _clientName = "namedOrderClient";
        private IHttpClientFactory _httpClientFactory;

        public NamedOrderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get()
        {
            // 使用客户端名称获取客户端
            var client = _httpClientFactory.CreateClient(_clientName);

            // 发起 HTTP 请求，doamin已通过基础配置进行了设置，此处就可以直接使用相对路径访问
            return await client.GetStringAsync("/order");
        }
    }
}
