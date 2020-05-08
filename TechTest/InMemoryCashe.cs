using System;
using System.Runtime.Caching;

namespace TechTest
{
    public class InMemoryCashe
    {
        public object Get(string key)
        {
            var mc = MemoryCache.Default;
            return mc.Get(key);
        }
        public bool Add(string key, object value)
        {
            var mc = MemoryCache.Default;
            var offset = DateTimeOffset.Now.AddHours(3);
            return mc.Add(key, value, offset);
        }
        public void Delete(string key)
        {
            var mc = MemoryCache.Default;
            if (mc.Contains(key))
                mc.Remove(key);
        }
    }
}