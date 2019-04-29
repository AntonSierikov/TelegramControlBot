using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Abstract;
using ControlBot.Core.Indentifiers;

namespace ControlBot.Core.Entities
{
    public class CaseDate : DbObject<CaseDateId>
    {

        //----------------------------------------------------------------//

        public Case Case { get; set; }

        //----------------------------------------------------------------//

        public CaseDate(Int32 caseId, DateTime dateTime)
        {
            Id.Case_Id = caseId;
            Id.Specific_Date = dateTime;
        }

        //----------------------------------------------------------------//

        public CaseDate(Case @case, DateTime dateTime)
            : this(@case.Id, dateTime)
        {}

        //----------------------------------------------------------------//

    }
}
