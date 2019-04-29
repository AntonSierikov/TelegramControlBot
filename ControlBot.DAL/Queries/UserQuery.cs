using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using ControlBot.Core.Entities;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.IQueries;

namespace ControlBot.DAL.Queries
{
    internal class UserQuery : BaseQuery<ControlUser>, IUserQuery
    {

        //----------------------------------------------------------------//

        public UserQuery(ISession session) : base(session)
        {}

        //----------------------------------------------------------------//

        public async Task<ControlUser> GetItemAsync(Int32 id)
        {
            String getUser = $"SELECT * FROM {TableName} WHERE Id = @{nameof(id)}";
            return await Connection.QueryFirstOrDefaultAsync(getUser, new { id }, Transaction);
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> IsExistAsync(ControlUser item)
        {
            String isExist = $"SELECT Count(*)::integer FROM {TableName} WHERE Id = @{nameof(item.Id)}";
            return await Connection.ExecuteScalarAsync<Int32>(isExist, item, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//

        public Task<IEnumerable<ControlUser>> GetItemsAsync(String[] userNames)
        {
            String getUsers = $"SELECT * FROM {TableName} WHERE UserName = ANY(@{nameof(userNames)})";
            return Connection.QueryAsync<ControlUser>(getUsers, new { userNames }, Transaction);
        }

        //----------------------------------------------------------------//

    }
}
