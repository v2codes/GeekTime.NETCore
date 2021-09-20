using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientFactory.Demo.DelegatingHandlers
{
    public class RequestIdDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage>  SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            // 处理请求
            request.Headers.Add("x-guid", Guid.NewGuid().ToString());

            // 调用内部 handler
            var result = await base.SendAsync(request, cancellationToken);

            // 处理相应
            return result;
        }
    }
}
