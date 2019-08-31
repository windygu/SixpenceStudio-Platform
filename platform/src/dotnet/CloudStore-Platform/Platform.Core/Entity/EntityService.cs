using System;
using System.Collections.Generic;
using System.Text;
using Platform.Core.Command;

namespace Platform.Core.Entity
{
    public class EntityService<E>
        where E : BaseEntity, new()
    {
        protected EntityCommand<E> _cmd;
    }
}
