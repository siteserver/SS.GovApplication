using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteServer.Plugin;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Core
{
    public static class TemplateManager
    {
        public static string GetTemplateHtml(string templateType, string directoryName)
        {
            var htmlPath = Context.PluginApi.GetPluginPath(Utils.Utils.PluginId, $"templates/{directoryName}/index.html");

            var html = CacheUtils.Get<string>(htmlPath);
            if (html != null) return html;

            html = Utils.Utils.ReadText(htmlPath);
            var startIndex = html.IndexOf("<body", StringComparison.Ordinal) + 5;
            var length = html.IndexOf("</body>", StringComparison.Ordinal) - startIndex;
            html = html.Substring(startIndex, length);
            html = html.Substring(html.IndexOf('\n'));

            //            var jsPath = Context.PluginApi.GetPluginPath(Utils.PluginId, $"assets/js/{templateType}.js");
            //            var javascript = Utils.ReadText(jsPath);
            //            html = html.Replace(
            //                $@"<script src=""../../assets/js/{templateType}.js"" type=""text/javascript""></script>",
            //                $@"<script type=""text/javascript"">
            //{javascript}
            //</script>");
            html = html.Replace("../../", "{stl.siteUrl}/sitefiles/plugins/ss.form/");
            html = html.Replace("../", "{stl.siteUrl}/sitefiles/plugins/ss.form/templates/");

            CacheUtils.InsertHours(htmlPath, html, 1);
            return html;
        }
    }
}
