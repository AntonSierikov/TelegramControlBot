using ControlBot.BL.IServices;
using ControlBot.Core.Entities;
using ControlBot.DAL.ICommands;
using System;
using System.Threading.Tasks;


namespace ControlBot.BL.Services
{
    internal class HistoryService : BaseService, IHistoryService
    {
        public HistoryService(IServiceProvider provider) : base(provider)
        {
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> InsertHistoryNotifyRecord(ICommand<History, Int32> historyCommand, History history)
        {
            history.Id = await historyCommand.InsertAsync(history);
            return history.Id > default(Int32);
        }

        //----------------------------------------------------------------//

    }
}
