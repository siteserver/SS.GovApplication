using System;
using System.Web.Http;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Model;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Controllers
{
    [RoutePrefix("settings")]
    public class SettingsController : ApiController
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

                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);

                //try
                //{
                //    var smsPlugin = Context.PluginApi.GetPlugin<SmsPlugin>();
                //    if (smsPlugin != null)
                //    {
                //        isSmsAvaliable = true;
                //    }
                //}
                //catch
                //{
                //    // ignored
                //}

                return Ok(new
                {
                    FieldInfoList = fieldInfoList
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

                var settings = Context.ConfigApi.GetConfig<Settings>(Utils.PluginId, siteId);

                var type = request.GetPostString("type");
                if (Utils.EqualsIgnoreCase(type, nameof(Settings.IsCaptcha)))
                {
                    settings.IsCaptcha = request.GetPostBool(nameof(Settings.IsCaptcha).ToCamelCase());
                    Context.ConfigApi.SetConfig(Utils.PluginId, siteId, settings);
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
