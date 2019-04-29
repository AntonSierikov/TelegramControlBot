using System;
using System.Threading.Tasks;
using ControlBot.Core.Entities;
using ControlBot.DAL.ICommands;

namespace ControlBot.BL.IServices
{
    public interface IHistoryService
    {
        Task<Boolean> InsertHistoryNotifyRecord(ICommand<History, Int32> command, History history);
    }
}
