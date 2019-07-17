using System;
using System.Collections.Generic;
using System.Text;
using Platform.Data.Command;

namespace Platform.Data.Entity
{
    public class EntityService<E>
        where E : BaseEntity, new()
    {
        protected EntityCommand<E> _cmd;
    }
}
