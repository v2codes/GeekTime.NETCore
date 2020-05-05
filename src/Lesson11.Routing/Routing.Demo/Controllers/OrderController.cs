using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Routing.Demo.Controllers
{
    /// <summary>
    /// 订单接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">必须可以转为long型</param>
        /// <returns></returns>
        [HttpGet("{id:CustomConstraint}")]
        public bool OrderExist(object id)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">最大20</param>
        /// <param name="linkGenerator"></param>
        /// <returns></returns>
        [HttpGet("{id:max(20)}")]
        public bool Max(long id, [FromServices]LinkGenerator linkGenerator)
        {
            var a = linkGenerator.GetPathByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" }
                );

            var uri = linkGenerator.GetUriByAction(HttpContext,
                action: "Reque",
                controller: "Order",
                values: new { name = "abc" });
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">必填</param>
        /// <returns></returns>
        [HttpGet("name:required")]
        //[Obsolete] // 废弃的接口标记
        public bool Reque()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">三个数字开始</param>
        /// <returns></returns>
        [HttpGet("{number:regex(^\\d{{3}}$)}")]
        public bool Number(string number)
        {
            return true;
        }
    }
}