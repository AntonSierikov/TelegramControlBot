using ControlBot.Core.Abstract;
using ControlBot.Core.Extensions;
using ControlBot.DAL.ICommands;
using ControlBot.DAL.IQueries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlBot.DAL.Helpers
{
    public static class QueryHelper
    {

        //----------------------------------------------------------------//

        public static IEnumerable<TEntity>
         SaveOrUpdateCollection<TEntity, TKey>(
                this ICommand<TEntity,
                TKey> command,
                IEnumerable<TEntity> entities
        ) where TEntity: DbObject<TKey>
        {
            entities.ForEach(e => command.SaveOrUpdate(e));
            return entities;
        }

        //----------------------------------------------------------------//
        //need perfomance investigation // have alternative solution //
        public static async Task<IEnumerable<TEntity>> SaveOrUpdateCollectionAsync<TEntity, TKey>(this ICommand<TEntity, TKey> command, 
                                                                                                       IQuery<TEntity, TKey> query,
                                                                                                       IEnumerable<TEntity> entities) where TEntity : DbObject<TKey>
        {
            await entities.ForEachAsync(e => command.SaveOrUpdateAsync(query, e));
            return entities;
        }

        //----------------------------------------------------------------//

        public static void SaveOrUpdate<TEn, TKey>(this ICommand<TEn, TKey> command, TEn entity) where TEn: DbObject<TKey>
        {
            if (entity.Id.Equals(default(TKey)))
            {
                entity.Id = command.Insert(entity);
            }
            else 
            {
                command.Update(entity);
            }
        }

        //----------------------------------------------------------------//
            
        public static async Task SaveOrUpdateAsync<TEn, TKey>(this ICommand<TEn, TKey> command, IQuery<TEn, TKey> query, TEn entity) where TEn : DbObject<TKey>
        {
            if(!await command.SaveIfNotExist(query, entity))
            {
                await command.UpdateAsync(entity);
            }
        }

        //----------------------------------------------------------------//

        public static async Task<bool> SaveIfNotExist<TEn, TKey>(this ICommand<TEn, TKey> command, 
                                                                     IQuery<TEn, TKey> query,   
                                                                     TEn entity) where TEn : DbObject<TKey>
        {
            if (!await query.IsExistAsync(entity))
            {
                entity.Id = await command.InsertAsync(entity);
                return true;
            }

            return false;
        }

        //----------------------------------------------------------------//
        
        public static async Task<List<TEn>> SaveIfNotExistCollection<TEn, TKey>
                            (this ICommand<TEn, TKey> command, 
                             IQuery<TEn, TKey> query, 
                             IEnumerable<TEn> entities) where TEn: DbObject<TKey>
        {
            List<TEn> collection = new List<TEn>();
            foreach(TEn entity in entities)
            {
                if(await SaveIfNotExist(command, query, entity))
                {
                    collection.Add(entity);
                }
            }
            return collection;
        }

        //----------------------------------------------------------------//

    }
}
