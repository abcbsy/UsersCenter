using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersCenter.Api
{
    public class HttpContextHelper
    {
        private static IHttpContextAccessor _accessor;

        public static HttpContext HttpContext => _accessor.HttpContext;

        /// <summary>
        /// 注入IHttpContextAccessor
        /// </summary>
        /// <param name="accessor"></param>
        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
    public static class StaticHttpContextExtensions
    {
        /// <summary>
        /// 给IServiceCollection添加HttpContextAccessor
        /// </summary>
        /// <param name="services"></param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        /// <summary>
        /// 配置HttpContextHelper，注入IHttpContextAccessor
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureHttpContextHelper(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContextHelper.Configure(httpContextAccessor);
            return app;
        }
    }
}
