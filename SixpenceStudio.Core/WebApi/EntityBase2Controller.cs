using Newtonsoft.Json;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Core.WebApi
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="S"></typeparam>
    public class EntityBase2Controller<E, S> : BaseController
        where E : BaseEntity, new()
        where S : EntityService<E>, new()
    {
        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/[controller]/view")]
        public IList<EntityView> GetViewList()
        {
            return new S().GetViewList();
        }

        /// <summary>
        /// 获取筛选数据
        /// </summary>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="viewId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpGet, Route("api/[controller]/list")]
        public virtual IList<E> GetDataList(string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, viewId, searchValue);
        }

        /// <summary>
        /// 分页获取筛选数据
        /// </summary>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="viewId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpGet, Route("api/[controller]/list")]
        public virtual DataModel<E> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("api/[controller]/data")]
        public virtual E GetData(string id)
        {
            return new S().GetData(id);
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("api/[controller]/data")]
        public string CreateData(E entity)
        {
            return new S().CreateData(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        [HttpPut, Route("api/[controller]/data")]
        public void UpdateData(E entity)
        {
            new S().UpdateData(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids"></param>
        [HttpDelete, Route("api/[controller]/data")]
        public void DeleteData(string ids)
        {
            var idList = JsonConvert.DeserializeObject<List<string>>(ids);
            new S().DeleteData(idList);
        }
    }
}
