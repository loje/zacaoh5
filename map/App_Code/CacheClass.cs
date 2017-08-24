using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace HiFi.Common
{
    public class CacheClass
    {
        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        public static void RemoveAllCache()
        {
            Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            if (_cache.Count > 0)
            {
                ArrayList al = new ArrayList();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
                foreach (string key in al)
                {
                    _cache.Remove(key);
                }
            }
        }

        public static void RemoveOneCache(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }

        public static void SetCache(string CacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject);
        }

        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        public static ArrayList ShowAllCache()
        {
            ArrayList al = new ArrayList();
            Cache _cache = HttpRuntime.Cache;
            if (_cache.Count > 0)
            {
                IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
            }
            return al;
        }
    }
}
