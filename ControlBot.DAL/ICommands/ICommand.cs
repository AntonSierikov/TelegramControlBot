using System.Threading.Tasks;
using ControlBot.Core.Abstract;

namespace ControlBot.DAL.ICommands
{
    public interface ICommand<T, TId> where T : DbObject<TId>
    {
        TId Insert(T entity);
        Task<TId> InsertAsync(T entity);
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity);
        bool Delete(T entity);
    }
}
