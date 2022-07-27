using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Core.Cache.Interfaces
{
    interface ICacheHelper
    {
        Task<T> Get<T>(string key);
        Task<T> GetFromCacheAsync<T>(Func<T> action, string key, int expirationSeconds) where T : class;
        Task<T> GetFromCacheAsync<T>(Func<Task<T>> action, string key, int expirationSeconds) where T : class;
        Task Set<T>(T item, string key, int expirationSeconds);
    }
}
