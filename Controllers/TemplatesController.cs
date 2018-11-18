using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Controllers
{
    [RoutePrefix("templates")]
    public class TemplatesController : ApiController
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

                var templateType = request.GetQueryString("templateType");
                var directoryPath = Context.PluginApi.GetPluginPath(Utils.PluginId, "templates");
                var templateUrl = Context.PluginApi.GetPluginUrl(Utils.PluginId, "templates");
                
                var templates = new List<object>();
                foreach (var directoryName in Utils.GetDirectoryNames(directoryPath).OrderBy(x => x.Length))
                {
                    if (Utils.StartsWithIgnoreCase(directoryName, templateType))
                    {
                        var html = TemplateManager.GetTemplateHtml(templateType, directoryName);
                        templates.Add(new
                        {
                            Id = directoryName,
                            TemplateUrl = templateUrl,
                            Html = html
                        });
                    }
                }
                
                return Ok(new
                {
                    Value = templates
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
