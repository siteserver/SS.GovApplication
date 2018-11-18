using System;
using System.Collections.Generic;
using System.Web.Http;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Model;
using SS.GovApplication.Core.Provider;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Controllers
{
    [RoutePrefix("data/add")]
    public class DataAddController : ApiController
    {
        private const string Route = "";

        [HttpGet, Route(Route)]
        public IHttpActionResult Get()
        {
            try
            {
                var request = Context.GetCurrentRequest();
                var siteId = request.GetQueryInt("siteId");
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSitePermissions(siteId, Utils.PluginId)) return Unauthorized();

                var logId = request.GetQueryInt("logId");
                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);

                if (logId > 0)
                {
                    var logInfo = DataDao.GetDataInfo(logId);
                    foreach (var fieldInfo in fieldInfoList)
                    {
                        if (fieldInfo.FieldType == InputType.CheckBox.Value || fieldInfo.FieldType == InputType.SelectMultiple.Value)
                        {
                            fieldInfo.Value = Utils.JsonDeserialize<List<string>>(logInfo.GetString(fieldInfo.Title));
                        }
                        else if (fieldInfo.FieldType == InputType.Date.Value || fieldInfo.FieldType == InputType.DateTime.Value)
                        {
                            fieldInfo.Value = logInfo.GetDateTime(fieldInfo.Title);
                        }
                        else
                        {
                            fieldInfo.Value = logInfo.GetString(fieldInfo.Title);
                        }
                    }
                }
                else
                {
                    foreach (var fieldInfo in fieldInfoList)
                    {
                        if (fieldInfo.FieldType == InputType.CheckBox.Value || fieldInfo.FieldType == InputType.SelectMultiple.Value)
                        {
                            fieldInfo.Value = new List<string>();
                        }
                        else if (fieldInfo.FieldType == InputType.Date.Value || fieldInfo.FieldType == InputType.DateTime.Value)
                        {
                            fieldInfo.Value = null;
                        }
                        else
                        {
                            fieldInfo.Value = string.Empty;
                        }
                    }
                }

                return Ok(new
                {
                    Value = fieldInfoList
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route(Route)]
        public IHttpActionResult Submit()
        {
            try
            {
                var request = Context.GetCurrentRequest();
                var siteId = request.GetPostInt("siteId");
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSitePermissions(siteId, Utils.PluginId)) return Unauthorized();

                var logId = request.GetPostInt("logId");

                var logInfo = logId > 0
                    ? DataDao.GetDataInfo(logId)
                    : new DataInfo
                    {
                        SiteId = siteId,
                        AddDate = DateTime.Now
                    };
                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);
                foreach (var fieldInfo in fieldInfoList)
                {
                    if (request.IsPostExists(fieldInfo.Title))
                    {
                        var value = request.GetPostString(fieldInfo.Title);
                        if (fieldInfo.FieldType == InputType.Date.Value || fieldInfo.FieldType == InputType.DateTime.Value)
                        {
                            var dt = Utils.ToDateTime(request.GetPostString(fieldInfo.Title));
                            logInfo.Set(fieldInfo.Title, dt.ToLocalTime());
                        }

                        else
                        {
                            logInfo.Set(fieldInfo.Title, value);
                        }
                    }
                    
                }

                if (logId == 0)
                {
                    DataDao.Insert(logInfo);
                }
                else
                {
                    DataDao.Update(logInfo);
                }

                return Ok(new{});
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
