using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace list.Storage
{
    public static class MongoDbUtil
    {
        public static Task<List<T>> ToMongoListAsync<T>(this IQueryable<T> mongoQueryOnly)
        {
            return ((IMongoQueryable<T>)mongoQueryOnly).ToListAsync();
        }
        //public static IAsyncEnumerable<T> ToAsyncEnumerableA<T>(this IQueryable<T> mongoQueryOnly)
        //{
        //    return ((IMongoQueryable<T>)mongoQueryOnly).ToAsyncEnumerable();
        //}
    }
}
