using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    public class MemcachedHelper
    {
        MemcachedClientConfiguration mcConfig = new MemcachedClientConfiguration();
        private static MemcachedClient mc = null;
        public MemcachedHelper()
        {
            mcConfig.AddServer("localhost:11211");//memcached默认端口为11211
            mc = new MemcachedClient(mcConfig);
        }

        /// <summary>
        /// 向Memcached中添加数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set(string key, object value)
        {
            return mc.Store(StoreMode.Set, key, value);
        }

        public static bool Set(string key, object value, DateTime time)//time为过期时间
        {
            return mc.Store(StoreMode.Set,key, value, time);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            return mc.Remove(key);
        }
       


    }
}
