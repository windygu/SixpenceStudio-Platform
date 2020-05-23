using Newtonsoft.Json;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Platform.WebApi
{
    [Route("api/[controller]/[action]")]
    [WebApiExceptionFilter, WebApiTracker]
    public class EntityController<E, S> : ApiController
        where E : BaseEntity, new()
        where S : EntityService<E>, new()
    {
        [HttpGet]
        public IList<EntityView<E>> GetViewList()
        {
            return new S().GetViewList();
        }

        [HttpGet]
        public IList<E> GetDataList(string searchList = "", string orderBy = "", string viewId = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, viewId);
        }

        [HttpGet]
        public DataModel<E> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, pageSize, pageIndex, viewId);
        }

        [HttpGet]
        public E GetData(string id)
        {
            return new S().GetData(id);
        }

        [HttpPost]
        public string CreateData(E entity)
        {
            return new S().CreateData(entity);
        }

        [HttpPost]
        public void UpdateData(E entity)
        {
            new S().UpdateData(entity);
        }

        [HttpPost]
        public void DeleteData(List<string> ids)
        {
            new S().DeleteData(ids);
        }
    }
}
