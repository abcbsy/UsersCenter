using CacheManager.Core;
using CacheManager.Serialization.Json;
using FrameworkCommon = Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace UsersCenter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => {
                //option.Filters.Add(typeof(UserCenterAuthorizeFilter));//注册身份认证过滤器，然后在需要执行身份认证的Controler或者Action上加[Authorize]特性
            })
            .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });//返回JSON数据属性名和DTO中保持一样的大小写，去掉这行将返回全部小写

            services.AddHttpContextAccessor();
            services.AddCors(option =>
            {
                option.AddPolicy("AllowAllOrigins", builder => {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });//允许跨域
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAllOrigins");//允许跨域
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseAuthentication();//添加认证中间件
            app.ConfigureHttpContextHelper();
            
            FrameworkCommon.ConfigurationManager.Config(new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build());

            FrameworkCommon.CacheManager.ConfigMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            FrameworkCommon.CacheManager.ConfigMultilevelCache(CacheManager.Core.CacheFactory.Build("UsersCenter", settings =>
            {
                settings.WithRedisConfiguration("redis", "10.2.21.216:6380,ssl=false,password=", 10)
                .WithMaxRetries(1000)//尝试次数
                .WithRetryTimeout(100)//尝试超时时间
                                      //.WithRedisBackplane("redis")//redis使用Back plane
                .WithSerializer(typeof(JsonCacheSerializer), null)
                .WithRedisCacheHandle("redis", true)//redis缓存handle
                ;
            })); 
        }
    }
}
