using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlBot.Core.Entities;

namespace ControlBot.DAL.IQueries
{
    public interface IUserQuery : IQuery<ControlUser, Int32>
    {
        Task<IEnumerable<ControlUser>> GetItemsAsync(String[] userNames);
    }
}
