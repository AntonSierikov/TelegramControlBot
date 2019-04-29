using ControlBot.BL.IServices;
using ControlBot.BL.Messages;
using ControlBot.BL.Models;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.Helpers;
using ControlBot.DAL.ICommands;
using ControlBot.DAL.IQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlBot.BL.Services
{
    internal class SequencerService : BaseService, ISequencerService
    {

        //----------------------------------------------------------------//

        public SequencerService(IServiceProvider provider) : base(provider)
        {}

        //----------------------------------------------------------------//

        public async Task<ProcessResult> TryGenerateOrUpdateSequence(ISession session, String nameCase, String[] userNames)
        {
            String result = null;
            IEnumerable<UserCaseSequencer> sequencers = null;

            ICaseQuery caseQuery = QueryFactory.CreateQuery<ICaseQuery>(session);
            Case @case = await caseQuery.GetCaseByNameAsync(nameCase);

            if(@case != null)
            {
                IUserQuery userQuery = QueryFactory.CreateQuery<IUserQuery>(session);
                IEnumerable<ControlUser> users = await userQuery.GetItemsAsync(userNames);

                if (users.Count() == userNames.Length)
                {
                    ICommand<UserCaseSequencer, UserCaseSequencerId> sequencerCommand = CommandFactory.CreateCommand<ICommand<UserCaseSequencer, UserCaseSequencerId>>(session);
                    ICommand<Case, Int32> caseCommand = CommandFactory.CreateCommand<ICommand<Case, Int32>>(session);
                    IQuery<UserCaseSequencer, UserCaseSequencerId> sequencerQuery = QueryFactory.CreateQuery<IQuery<UserCaseSequencer, UserCaseSequencerId>>(session);

                    sequencers = users.Select((u, i) => new UserCaseSequencer(@case, u, i));
                    sequencers = await sequencerCommand.SaveOrUpdateCollectionAsync(sequencerQuery, sequencers);
                    @case.NextUserId = sequencers.OrderBy(i => i.Sequence).First().Id.UserId;
                    await caseCommand.UpdateAsync(@case);
                }
                else result = UserMessages.UsersNotExist(GetNotExistUsers(users, userNames));

            }
            else result = CaseMessages.CaseNotExist(nameCase);

            return String.IsNullOrEmpty(result)
                   ? new ProcessResult(SequeceMessages.CaseSequenceGenerated(nameCase, sequencers), true)
                   : new ProcessResult(result, false);
        }

        //----------------------------------------------------------------//
            
        private IEnumerable<String> GetNotExistUsers(IEnumerable<ControlUser> existUsers, IEnumerable<String> users)
        {
            return from user in users
                   join existUser in existUsers on user equals existUser.UserName into ue
                   from existUser in ue.DefaultIfEmpty()
                   where existUser == null
                   select user;
        }

        //----------------------------------------------------------------//

    }
}
