using System;
using System.Web.Http;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Model;
using SS.GovApplication.Core.Provider;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Controllers
{
    public class MainController : ApiController
    {
        [HttpPost, Route("{siteId:int}/actions/get")]
        public IHttpActionResult GetForm(int siteId)
        {
            try
            {
                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);
                var settings = Context.ConfigApi.GetConfig<Settings>(Utils.PluginId, siteId);

                return Ok(new
                {
                    Value = fieldInfoList,
                    settings.IsCaptcha
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("{siteId:int}")]
        public IHttpActionResult Submit(int siteId)
        {
            try
            {
                var request = Context.GetCurrentRequest();

                var logInfo = new DataInfo
                {
                    SiteId = siteId,
                    AddDate = DateTime.Now
                };

                var fieldInfoList = FieldManager.GetFieldInfoList(siteId);
                foreach (var fieldInfo in fieldInfoList)
                {
                    var value = request.GetPostString(fieldInfo.Title);
                    logInfo.Set(fieldInfo.Title, value);
                    if (FieldManager.IsExtra(fieldInfo))
                    {
                        foreach (var item in fieldInfo.Items)
                        {
                            var extrasId = FieldManager.GetExtrasId(fieldInfo.Id, item.Id);
                            var extras = request.GetPostString(extrasId);
                            if (!string.IsNullOrEmpty(extras))
                            {
                                logInfo.Set(extrasId, extras);
                            }
                        }
                    }
                }

                logInfo.Id = DataDao.Insert(logInfo);

                return Ok(logInfo);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
