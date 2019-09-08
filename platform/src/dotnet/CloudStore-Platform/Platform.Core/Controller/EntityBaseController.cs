using System;
using System.Collections.Generic;
using System.Text;
using Platform.Core.Entity;
using Platform.Core.Service;

namespace Platform.Core.Controller
{
    public class EntityBaseController<E, S>
        where E : BaseEntity, new()
        where S : EntityService<E>, new() 
    {

    }
}
