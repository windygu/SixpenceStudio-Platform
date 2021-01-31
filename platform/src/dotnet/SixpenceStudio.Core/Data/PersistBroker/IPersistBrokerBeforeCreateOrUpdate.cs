using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data
{
    /// <summary>
    /// 创建或更新前
    /// </summary>
    [UnityRegister]
    public interface IPersistBrokerBeforeCreateOrUpdate
    {
        void Execute(Context context);
    }

    public class PersistBrokerBeforeCreateOrUpdate : IPersistBrokerBeforeCreateOrUpdate
    {
        public void Execute(Context context)
        {
            var entity = context.Entity;
            var broker = context.Broker;

            var user = UserIdentityUtil.GetCurrentUser();

            switch (context.Action)
            {
                case EntityAction.PreCreate:
                    {
                        if ((!entity.Attributes.ContainsKey("createdBy") || entity.GetAttributeValue("createdBy") == null) && entity.GetType().GetProperty("createdBy") != null)
                        {
                            entity.SetAttributeValue("createdBy", user.Id);
                            entity.SetAttributeValue("createdByName", user.Name);
                        }
                        if ((!entity.Attributes.ContainsKey("createdOn") || entity.GetAttributeValue("createdOn") == null) && entity.GetType().GetProperty("createdOn") != null)
                        {
                            entity.SetAttributeValue("createdOn", DateTime.Now);
                        }
                        entity.SetAttributeValue("modifiedBy", user.Id);
                        entity.SetAttributeValue("modifiedByName", user.Name);
                        entity.SetAttributeValue("modifiedOn", DateTime.Now);

                        SetBooleanName(entity);
                        CheckDuplicate(entity, broker);
                    }
                    break;
                case EntityAction.PreUpdate:
                    {
                        entity.SetAttributeValue("modifiedBy", user.Id);
                        entity.SetAttributeValue("modifiedByName", user.Name);
                        entity.SetAttributeValue("modifiedOn", DateTime.Now);

                        SetBooleanName(entity);
                        CheckDuplicate(entity, broker);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 重复字段检查
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void CheckDuplicate(BaseEntity entity, IPersistBroker broker)
        {
            var attrs = entity.GetType().GetCustomAttributes(typeof(KeyAttributesAttribute), false);
            if (attrs.Length == 0) return;

            var sql = string.Format(@"SELECT * FROM {0} WHERE {0}Id = @id", entity.EntityName);
            var dt = broker.Query(sql, new Dictionary<string, object> { { "@id", entity.Id } });

            attrs.Select(item => item as KeyAttributesAttribute)
               .Each(item =>
               {
                   if (item.AttributeList == null || item.AttributeList.Count == 0) return;

                   var paramList = new Dictionary<string, object>();
                   var sqlParam = new List<string>();
                   item.AttributeList.Distinct().Each(attr =>
                   {
                       if (dt.Rows[0][attr] == DBNull.Value)
                           sqlParam.Add(attr + " IS NULL");
                       else
                       {
                           var paramKey = "@" + attr;
                           sqlParam.Add(attr + "=" + paramKey);
                           paramList.Add(paramKey, dt.Rows[0][attr]);
                       }
                   });
                   if (sqlParam.Count > 0)
                   {
                       sql = string.Format(@"SELECT {0}Id FROM {0} WHERE ", entity.EntityName) + string.Join(" AND ", sqlParam);
                   }
                   else
                   {
                       sql = string.Format(@"SELECT {0}Id FROM {0} ", entity.EntityName);
                   }

                   AssertUtil.CheckBoolean<SpException>(broker.Query<string>(sql, paramList)?.Count() > 1, "7293452C-AFCA-408D-9EBD-B1CECD206A7D", item.RepeatMessage);
               });
        }

        /// <summary>
        /// 设置布尔值
        /// </summary>
        /// <param name="entity"></param>
        private void SetBooleanName(BaseEntity entity)
        {
            var dic = new Dictionary<string, object>();

            entity.Attributes
                .Where(item => item.Value is bool)
                .Each(item =>
                {
                    if (entity.GetType().GetProperties().Where(p => p.Name == $"{item.Key}Name").FirstOrDefault() != null)
                    {
                        dic.Add($"{item.Key}Name", (bool)item.Value ? "是" : "否");
                    }
                });

            dic.Each(item => entity.SetAttributeValue(item.Key, item.Value));
        }
    }
}
