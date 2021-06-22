using SixpenceStudio.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    [UnityRegister]
    public interface IEntityInitialDataProvider
    {
        string EntityName { get; }
        IEnumerable<BaseEntity> GetInitialData();
    }
}
