using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using UsersCenter.Api.Filters;

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
            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            //{
            //    o.Cookie.Name = "_UsersCenterCookie";
            //    o.LoginPath = new PathString("/api/Users/SignIn");
            //    o.LogoutPath = new PathString("/api/Users/SignOut");
            //    o.AccessDeniedPath = new PathString("/Error/Forbidden");
            //});
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

        }
    }
}
