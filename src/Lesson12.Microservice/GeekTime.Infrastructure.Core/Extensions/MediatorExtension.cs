using GeekTime.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekTime.Infrastructure.Core.Extensions
{
    /// <summary>
    /// 中间者，领域事件发布扩展类
    /// </summary>
    public static class MediatorExtension
    {
        /// <summary>
        /// 领域事件发布，执行事件发送
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator,DbContext ctx)
        {
            // 1. 从当前要保存的 EntityContext 里面去跟踪实体
            //    从跟踪到的实体对象中，获取到我们当前的 Event
            var domainEntities = ctx.ChangeTracker
                    .Entries<Entity>()
                    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            // Events 类型转换
            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

            // 2. 将实体内的 Events 清除 
            domainEntities.ToList()
                    .ForEach(entity => entity.Entity.ClearDomainEvents());

            // 3. 将所有的 Event 通过中间者发送出去
            //    发出后，并找到响应的 handle 进行处理
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
