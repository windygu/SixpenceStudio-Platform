using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OperationTypeAttribute : Attribute
    {
        public OperationTypeAttribute(params string[] types)
        {
            this.OperationTypes = types;
        }

        public string[] OperationTypes;
    }
}
