using System;
using System.Collections.Generic;
using System.Text;

namespace ControlBot.BL.Models
{
    public class ProcessResult
    {

        //----------------------------------------------------------------//

        public String Message { get; }

        public Boolean IsSuccess { get; }


        //----------------------------------------------------------------//

        public ProcessResult(String message, Boolean isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        //----------------------------------------------------------------//
    }

    //----------------------------------------------------------------//

    public class ProcessResult<T> : ProcessResult
    {
        T Result { get;  }


        //----------------------------------------------------------------//

        public ProcessResult(T result, String successMsg)
            : base(successMsg, true)
        {
            Result = result;
        }

        //----------------------------------------------------------------//

    }

    //----------------------------------------------------------------//

}
