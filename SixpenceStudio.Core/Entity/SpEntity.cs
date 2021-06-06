using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    public class SpEntity : BaseEntity
    {
        public SpEntity() { }
        public SpEntity(string entityName) : base(entityName) { }
        public SpEntity(string entityName, string id)
        {
            this.EntityName = entityName;
            this.Id = id;
        }
    }
}
