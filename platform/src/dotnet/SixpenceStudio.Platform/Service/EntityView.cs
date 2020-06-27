using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Service
{
    public class EntityView<T>
        where T : BaseEntity, new()
    {
        public string Name { get; set; }
        public IList<string> CustomFilter { get; set; }
        public string ViewId { get; set; }
        public string Sql { get; set; }
        public string OrderBy { get; set; }
    }
}
