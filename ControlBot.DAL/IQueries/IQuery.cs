using System;
using System.Threading.Tasks;
using ControlBot.Core.Abstract;

namespace ControlBot.DAL.IQueries
{
    public interface IQuery<T, TId> where T: DbObject<TId>
    {
        Task<T> GetItemAsync(TId id);

        Task<Int32> CountAsync();

        Task<Boolean> IsExistAsync(T item);
    }
}
