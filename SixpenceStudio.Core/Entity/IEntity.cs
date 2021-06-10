using SixpenceStudio.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    public delegate void PropertyChangedHandler(object sender, EventArgs e);

    [UnityRegister]
    public interface IEntity
    {
        string GetEntityName();
        string GetLogicalName();
        object GetAttributeValue(string attributeLogicalName);
        void SetAttributeValue(string attributeLogicalName, object value);
        bool IsSystemEntity();
        IEnumerable<Attr> GetAttrs();
        IEnumerable<BaseEntity> GetInitialData();
        event PropertyChangedHandler OnPropertyChanged;
    }
}
