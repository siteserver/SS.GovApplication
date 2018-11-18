using SiteServer.Plugin;
using SS.GovApplication.Core.Utils;

namespace SS.GovApplication.Core
{
    public static class StlGovApplication
    {
        public const string ElementName = "stl:govApplication";

        private const string AttributeTitle = "title";

        public static string Parse(IParseContext context)
        {
            var title = string.Empty;

            foreach (var name in context.StlAttributes.AllKeys)
            {
                var value = context.StlAttributes[name];

                if (Utils.Utils.EqualsIgnoreCase(name, AttributeTitle))
                {
                    title = Context.ParseApi.ParseAttributeValue(value, context);
                }
            }

            return $@"
<script>
var $config = {{
    apiUrl: '{Context.UtilsApi.GetApiUrl()}',
    siteId: {context.SiteId}
}};
</script>
{context.StlInnerHtml}
";
        }
    }
}
