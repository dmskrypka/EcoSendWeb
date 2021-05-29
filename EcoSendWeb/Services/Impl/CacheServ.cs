using EcoSendWeb.Services.Api;
//using Pythagoras.Threading.Interlocking;
using System;
using System.Web;
using System.Web.Caching;

namespace EcoSendWeb.Services.Impl
{
    public class CacheServ : ICacheServ
    {
        private object lockObj = new object();
        //private LockPool cacheConstructionLockPool = new LockPool();

        public void SetObject<T>(string strKey, T value, TimeSpan timeout)
        {
            lock (lockObj)
            {
                HttpContext.Current.Cache.Insert(strKey, value, null, DateTime.Now + timeout, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            //using (this.cacheConstructionLockPool.LockKey(strKey))
            //{
            //    HttpContext.Current.Cache.Insert(strKey, value, null, DateTime.Now + timeout, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            //}
        }

        public void SetObject<T>(string strKey, T value)
        {
            lock (lockObj)
            {
                HttpContext.Current.Cache.Insert(strKey, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            //using (this.cacheConstructionLockPool.LockKey(strKey))
            //{
            //    HttpContext.Current.Cache.Insert(strKey, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            //}
        }

        public T GetObject<T>(string strKey)
        {
            object val = HttpContext.Current.Cache[strKey];
            if (val != null)
            {
                return (T)val;
            }
            else
            {
                lock (lockObj)
                {
                    val = HttpContext.Current.Cache[strKey];
                    if (val != null)
                    {
                        return (T)val;
                    }

                }
                //using (this.cacheConstructionLockPool.LockKey(strKey))
                //{
                //    val = HttpContext.Current.Cache[strKey];
                //    if (val != null)
                //    {
                //        return (T)val;
                //    }
                //}

                return default(T);
            }
        }

        public void RemoveObject(string strKey)
        {
            lock (lockObj)
            {
                HttpContext.Current.Cache.Remove(strKey);
            }
            //using (this.cacheConstructionLockPool.LockKey(strKey))
            //{
            //    HttpContext.Current.Cache.Remove(strKey);
            //}
        }
    }
}