using SixpenceStudio.Core.Auth.SysRolePrivilege;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.SysEntity;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data
{
    /// <summary>
    /// PersistBroker 权限扩展
    /// </summary>
    public static class IPersistBrokerAccessExtension
    {
        /// <summary>
        /// 权限创建
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string FilteredCreate(this IPersistBroker broker, BaseEntity entity)
        {
            //var sysEntity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @name", new Dictionary<string, object>() { { "@name", entity.EntityName } });
            //AssertUtil.CheckBoolean<SpException>(!new SysRolePrivilegeService(broker).CheckWriteAccess(sysEntity.Id), $"用户没有实体{sysEntity.name}的创建权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Create(entity);
        }

        /// <summary>
        /// 权限更新
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int FiltededUpdate(this IPersistBroker broker, BaseEntity entity)
        {
            //var sysEntity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @name", new Dictionary<string, object>() { { "@name", entity.EntityName } });
            //AssertUtil.CheckBoolean<SpException>(!new SysRolePrivilegeService(broker).CheckWriteAccess(sysEntity.Id), $"用户没有实体{sysEntity.name}的更新权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Update(entity);
        }

        /// <summary>
        /// 权限查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FilteredRetrieve<T>(this IPersistBroker broker, string id) where T : BaseEntity, new()
        {
            //var sysEntity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @name", new Dictionary<string, object>() { { "@name", new T().EntityName } });
            //AssertUtil.CheckBoolean<SpException>(!new SysRolePrivilegeService(broker).CheckReadAccess(sysEntity.Id), $"用户没有实体{sysEntity.name}的查询权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Retrieve<T>(id);
        }

        /// <summary>
        /// 权限差选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static IList<T> FilteredRetrieveMultiple<T>(this IPersistBroker broker, string sql, Dictionary<string, object> paramList = null) where T : BaseEntity, new()
        {
            //var sysEntity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @name", new Dictionary<string, object>() { { "@name", new T().EntityName } });
            //AssertUtil.CheckBoolean<SpException>(!new SysRolePrivilegeService(broker).CheckReadAccess(sysEntity.Id), $"用户没有实体{sysEntity.name}的查询权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.RetrieveMultiple<T>(sql, paramList);
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int FilteredDelete(this IPersistBroker broker, BaseEntity entity)
        {
            //var sysEntity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @name", new Dictionary<string, object>() { { "@name", entity.EntityName } });
            //AssertUtil.CheckBoolean<SpException>(!new SysRolePrivilegeService(broker).CheckDeleteAccess(sysEntity.Id), $"用户没有实体{sysEntity.name}的删除权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Delete(entity);
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int FilteredDelete(this IPersistBroker broker, string entityName, string id)
        {
            //var sysEntity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @name", new Dictionary<string, object>() { { "@name", entityName } });
            //AssertUtil.CheckBoolean<SpException>(!new SysRolePrivilegeService(broker).CheckDeleteAccess(sysEntity.Id), $"用户没有实体{sysEntity.name}的删除权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Delete(entityName, id);
        }
    }
}
