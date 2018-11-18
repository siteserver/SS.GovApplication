using System;
using System.Collections.Generic;
using System.Web.Http;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Provider;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Controllers
{
    [RoutePrefix("logs")]
    public class LogsController : ApiController
    {
        private const string Route = "";
        private const string RouteActionsExport = "actions/export";
        private const string RouteActionsVisible = "actions/visible";

        [HttpGet, Route(Route)]
        public IHttpActionResult Get()
        {
            try
            {
                var request = Context.GetCurrentRequest();
                var siteId = request.GetPostInt("siteId");
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSitePermissions(siteId, Utils.PluginId)) return Unauthorized();

                var state = request.GetPostString("state");
                var count = DataCountManager.GetCount(siteId, state);
                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);

                var pages = Convert.ToInt32(Math.Ceiling((double)count / Utils.PageSize));
                if (pages == 0) pages = 1;
                var page = request.GetQueryInt("page", 1);
                if (page > pages) page = pages;
                var logInfoList = DataDao.GetDataInfoList(siteId, state, page);

                var logs = new List<Dictionary<string, object>>();
                foreach (var logInfo in logInfoList)
                {
                    logs.Add(logInfo.ToDictionary());
                }

                return Ok(new
                {
                    Value = logs,
                    Count = count,
                    Pages = pages,
                    Page = page,
                    FieldInfoList = fieldInfoList
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

                var logId = request.GetPostInt("logId");
                var logInfo = DataDao.GetDataInfo(logId);
                if (logInfo == null) return NotFound();

                DataDao.Delete(logInfo);

                var state = request.GetPostString("state");
                var count = DataCountManager.GetCount(siteId, state);
                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);

                var pages = Convert.ToInt32(Math.Ceiling((double)count / Utils.PageSize));
                if (pages == 0) pages = 1;
                var page = request.GetQueryInt("page", 1);
                if (page > pages) page = pages;
                var logInfoList = DataDao.GetDataInfoList(siteId, state, page);

                var logs = new List<Dictionary<string, object>>();
                foreach (var info in logInfoList)
                {
                    logs.Add(info.ToDictionary());
                }

                return Ok(new
                {
                    Value = logs,
                    Count = count,
                    Pages = pages,
                    Page = page
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route(RouteActionsExport)]
        public IHttpActionResult Export()
        {
            try
            {
                var request = Context.GetCurrentRequest();
                var siteId = request.GetPostInt("siteId");
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSitePermissions(siteId, Utils.PluginId)) return Unauthorized();

                var state = request.GetPostString("state");
                var count = DataCountManager.GetCount(siteId, state);
                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);
                var logs = DataDao.GetDataInfoList(siteId, state, 0, count);

                var head = new List<string> { "序号" };
                foreach (var fieldInfo in fieldInfoList)
                {
                    head.Add(fieldInfo.Title);
                }
                head.Add("添加时间");

                var rows = new List<List<string>>();

                var index = 1;
                foreach (var log in logs)
                {
                    var row = new List<string>
                    {
                        index++.ToString()
                    };
                    foreach (var fieldInfo in fieldInfoList)
                    {
                        row.Add(log.GetString(fieldInfo.Title));
                    }
                    row.Add(log.AddDate.ToString("yyyy-MM-dd HH:mm"));

                    rows.Add(row);
                }

                var relatedPath = "表单数据.csv";

                CsvUtils.Export(Context.PluginApi.GetPluginPath(Utils.PluginId, relatedPath), head, rows);
                var downloadUrl = Context.PluginApi.GetPluginUrl(Utils.PluginId, relatedPath);

                return Ok(new
                {
                    Value = downloadUrl
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route(RouteActionsVisible)]
        public IHttpActionResult Visible()
        {
            try
            {
                var request = Context.GetCurrentRequest();
                var siteId = request.GetPostInt("siteId");
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSitePermissions(siteId, Utils.PluginId)) return Unauthorized();

                var attributeName = request.GetPostString("attributeName");

                return Ok(new
                {
                    Value = attributeName
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
