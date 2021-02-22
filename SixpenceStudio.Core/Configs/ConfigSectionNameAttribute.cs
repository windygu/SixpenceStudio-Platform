using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Configs
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ConfigSectionNameAttribute : Attribute
    {
        public string Name { get; }

        public ConfigSectionNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
