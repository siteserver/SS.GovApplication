using System;
using System.Collections.Generic;
using System.Web.Http;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Provider;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Controllers
{
    [RoutePrefix("fields")]
    public class FieldsController : ApiController
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

                var list = new List<object>();
                foreach (var fieldInfo in FieldManager.GetFieldInfoList(siteId))
                {
                    list.Add(new
                    {
                        fieldInfo.Id,
                        fieldInfo.Title,
                        InputType = Utils.GetFieldTypeText(fieldInfo.FieldType),
                        fieldInfo.Validate,
                        fieldInfo.Taxis
                    });
                }

                return Ok(new
                {
                    Value = list
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete, Route(Route)]
        public IHttpActionResult Delete()
        {
            try
            {
                var request = Context.GetCurrentRequest();
                var siteId = request.GetPostInt("siteId");
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSitePermissions(siteId, Utils.PluginId)) return Unauthorized();

                var fieldId = request.GetPostInt("fieldId");
                FieldDao.Delete(fieldId);

                var list = new List<object>();
                foreach (var fieldInfo in FieldManager.GetFieldInfoList(siteId))
                {
                    list.Add(new
                    {
                        fieldInfo.Id,
                        fieldInfo.Title,
                        InputType = Utils.GetFieldTypeText(fieldInfo.FieldType),
                        fieldInfo.Validate,
                        fieldInfo.Taxis
                    });
                }

                return Ok(new
                {
                    Value = list
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
