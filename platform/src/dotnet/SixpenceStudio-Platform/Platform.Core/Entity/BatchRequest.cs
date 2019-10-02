using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Core.Entity
{
    public class BatchRequest<E>
    {
        public List<E> BatchList { get; set; }
    }
}
