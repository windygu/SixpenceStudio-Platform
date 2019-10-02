using System;
using System.Collections.Generic;
using System.Text;
using Platform.Core.Command;
using Platform.Core.Entity;
using Platform.Core.Service;

namespace Platform.Core.Controller
{
    public class EntityBaseController<E, S> : BaseApiController
        where E : BaseEntity, new()
        where S : EntityService<E>, new() 
    {
        public string CreateData(E model)
        {
            return new S().CreateData(model);
        }

        public string CreateOrUpdateData(E model)
        {
            return new S().CreateOrUpdateData(model);
        }

        public void DeleteData(List<string> idList)
        {
            new S().DeleteData(idList);
        }

        public E GetData(string id)
        {
            return new S().GetData(id);
        }

        public DataListModel<E> GetDataList(string viewId, string queryValue, int pageIndex, int pageSize)
        {
            return new S().GetDataList(viewId, queryValue, pageIndex, pageSize);
        }

        public void UpdateData(E model)
        {
            new S().UpdateData(model);
        }

        public DataListModel<E> GetDataList(List<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex)
        {
            return new S().GetDataList(searchList, orderBy, pageSize, pageIndex);
        }

    }
}
