using CacheManager.Core;
using CacheManager.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsersCenter.Common
{
    public static class CacheManager
    {
        private static ICacheManager<object> _ICacheManager;
        public static ICacheManager<object> Instance
        {
            get
            {
                if (_ICacheManager == null)
                    _ICacheManager = CacheFactory.Build("UsersCenter", settings =>
                    {
                        settings.WithRedisConfiguration("redis", "10.2.21.216:6380,ssl=false,password=", 10)
                        .WithMaxRetries(1000)//尝试次数
                        .WithRetryTimeout(100)//尝试超时时间
                                              //.WithRedisBackplane("redis")//redis使用Back plane
                        .WithSerializer(typeof(JsonCacheSerializer), null)
                        .WithRedisCacheHandle("redis", true)//redis缓存handle
                        ;
                    });
                return _ICacheManager;
            }
        }
    }
    public static class CacheHelper
    {

        public static void ToCache(this object value, string key, double seconds = 0)
        {
            var manager = CacheManager.Instance;
            manager.AddOrUpdate(key, value, v => v);
            if(seconds > 0)
            {
                manager.Expire(key, TimeSpan.FromSeconds(seconds));
            }
        }

        public static void KeyExpire(this string key, TimeSpan timeSpan)
        {
            CacheManager.Instance.Expire(key, timeSpan);
        }

        public static T FromCache<T>(this string key)
        {
            return CacheManager.Instance.Get<T>(key);
        }
        public static void RemoveCache(this string key)
        {
            CacheManager.Instance.Remove(key);
        }
        //public static void BatchRemoveCache(this string context)
        //{
        //    var manager = CacheManager.Instance;
        //    var list = manager.SearchKeys(context);
        //    foreach (string i in list)
        //    {
        //        Instance.Remove(i.Substring(i.IndexOf(':') + 1));
        //    }
        //}
        public static long Increment(this string key, long increment = 1)
        {
            var v = key.FromCache<long>();
            v += increment;
            v.ToCache(key);
            return v;
        }
    }
}
