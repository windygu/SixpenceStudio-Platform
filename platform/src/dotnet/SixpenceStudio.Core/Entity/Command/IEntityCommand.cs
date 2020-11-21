#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/21 18:26:24
Description：实体操作类
********************************************************/
#endregion

using SixpenceStudio.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    public interface IEntityCommand<E>
        where E : BaseEntity, new()
    {
        IPersistBroker Broker { get; set; }
        IList<E> GetAllEntity();
        IList<E> GetDataList(EntityView view, IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, out int recordCount, string searchValue = "");
        IList<E> GetDataList(EntityView view, IList<SearchCondition> searchList, string orderBy, string searchValue = "");
        E GetEntity(string id);
        string Create(E entity);
        void Update(E entity);
        string CreateOrUpdateData(E entity);
        void Delete(IEnumerable<string> ids);
    }
}
