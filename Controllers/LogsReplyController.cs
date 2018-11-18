using System;
using System.Web.Http;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Model;
using SS.GovApplication.Core.Provider;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Controllers
{
    [RoutePrefix("logs/reply")]
    public class LogsReplyController : ApiController
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
                var logInfo = DataDao.GetDataInfo(logId);

                //var attributeNames = FormManager.GetAllAttributeNames(formInfo, fieldInfoList);
                //if (!logInfo.IsReplied)
                //{
                //    attributeNames.Remove(nameof(DataInfo.ReplyDate));
                //}
                //attributeNames.Remove(nameof(DataInfo.ReplyContent));

                return Ok(new
                {
                    Value = logInfo.ToDictionary(),
                    //AttributeNames = attributeNames
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
                var logInfo = DataDao.GetDataInfo(logId);
                if (logInfo == null) return NotFound();

                logInfo.ReplyContent = request.GetPostString("replyContent");

                DataDao.Reply(logInfo);

                return Ok(new{});
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
