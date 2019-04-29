using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ControlBot.DAL.Abstract
{
    public interface ISession : IDisposable
    {
        IDbConnection Connection { get;  } 

        IDbTransaction Transaction { get;  }

    }
}
