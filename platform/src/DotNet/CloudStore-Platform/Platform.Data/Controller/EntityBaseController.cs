using System;
using System.Collections.Generic;
using System.Text;
using Platform.Data.Entity;

namespace Platform.Data.Controller
{
    public class EntityBaseController<E, S>
        where E : BaseEntity, new()
        where S : EntityService<E>, new() 
    {

    }
}
