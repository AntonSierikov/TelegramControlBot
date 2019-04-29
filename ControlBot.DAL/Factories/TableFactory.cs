using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Entities;
using ControlBot.DAL.Constants;

namespace ControlBot.DAL.Factories
{
    internal static class TableFactory
    {

        //----------------------------------------------------------------//

        private static Dictionary<Type, String> tableDictionary;

        //----------------------------------------------------------------//

        static TableFactory()
        {
            tableDictionary = new Dictionary<Type, String>();
            InitTables();
        }

        //----------------------------------------------------------------//

        private static void InitTables()
        {
            tableDictionary.Add(typeof(ControlUser), TableConstants.CONTROL_USER);
            tableDictionary.Add(typeof(Case), TableConstants.CASE);
            tableDictionary.Add(typeof(CaseDayOfWeek), TableConstants.CASE_DAY_OF_WEEK);
            tableDictionary.Add(typeof(CaseDate), TableConstants.CASE_SPECIFIC_DATE);
            tableDictionary.Add(typeof(UserCaseSequencer), TableConstants.USER_CASE_SEQUENCER);
        }

        //----------------------------------------------------------------//

        public static string GetNameTable<T>()
        {
            return tableDictionary.GetValueOrDefault(typeof(T));
        }

        //----------------------------------------------------------------//

    }
}
