using Newtonsoft.Json;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Platform.WebApi
{
    public class EntityBase2Controller<E, S> : BaseController
        where E : BaseEntity, new()
        where S : EntityService<E>, new()
    {
        [HttpGet, Route("api/[controller]/viewlist")]
        public IList<EntityView<E>> GetViewList()
        {
            return new S().GetViewList();
        }

        [HttpGet, Route("api/[controller]/datalist")]
        public virtual IList<E> GetDataList(string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, viewId, searchValue);
        }

        [HttpGet, Route("api/[controller]/datalist")]
        public virtual DataModel<E> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }

        [HttpGet, Route("api/[controller]/data")]
        public virtual E GetData(string id)
        {
            return new S().GetData(id);
        }

        [HttpPost, Route("api/[controller]/data")]
        public string CreateData(E entity)
        {
            return new S().CreateData(entity);
        }

        [HttpPut, Route("api/[controller]/data")]
        public void UpdateData(E entity)
        {
            new S().UpdateData(entity);
        }

        [HttpDelete, Route("api/[controller]/data")]
        public void DeleteData(List<string> ids)
        {
            new S().DeleteData(ids);
        }
    }
}
