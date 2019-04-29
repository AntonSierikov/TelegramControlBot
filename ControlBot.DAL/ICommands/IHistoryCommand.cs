using ControlBot.Core.Entities;
using System;
using System.Threading.Tasks;


namespace ControlBot.DAL.ICommands
{
    public interface IHistoryCommand : ICommand<History, Int32>
    {
        Task<Boolean> SetHistoryAsSuccess(Int32 historyId);
    }
}
