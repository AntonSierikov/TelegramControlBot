using System;
using System.Collections.Generic;
using System.Text;

namespace ControlBot.Core.Abstract
{
    public abstract class DbObject<TID>
    {
        public TID Id { get; set; }
    }
}
