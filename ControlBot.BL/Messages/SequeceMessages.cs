using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ControlBot.Core.Entities;

namespace ControlBot.BL.Messages
{
    class SequeceMessages
    {
        
        //----------------------------------------------------------------//

        public static String CaseSequenceGenerated(String name, IEnumerable<UserCaseSequencer> userCaseSequencers)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Sequence for {name} has generated");
            builder.AppendJoin(Environment.NewLine, userCaseSequencers.Select(u => $"{u.User.UserName} : {u.Sequence}"));
            return builder.ToString();
        }

        //----------------------------------------------------------------//

        public static String CaseSequenceNotGenerated(String name) => $"Sequence for {name} hasn't generated";

        //----------------------------------------------------------------//
    }
}
