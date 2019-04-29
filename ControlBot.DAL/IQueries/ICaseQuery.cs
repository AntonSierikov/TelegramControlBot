using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlBot.Core.Entities;

namespace ControlBot.DAL.IQueries
{
    public interface ICaseQuery : IQuery<Case, Int32>
    {
        Task<IEnumerable<Case>> GetDailyActiveCasesAsync();

        Task<Case> GetCaseByNameAsync(String caseName);
    }
}
