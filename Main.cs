using System.Collections.Generic;
using SiteServer.Plugin;
using SS.GovApplication.Core;
using SS.GovApplication.Core.Provider;
using Menu = SiteServer.Plugin.Menu;

namespace SS.GovApplication
{
    public class Main : PluginBase
    {
        public override void Startup(IService service)
        {
            service
                .AddSiteMenu(siteId => new Menu
                {
                    Text = "依申请公开",
                    IconClass = "ion-ios-book",
                    Menus = new List<Menu>
                    {
                        new Menu
                        {
                            Text = "待受理申请",
                            Href = "pages/dataAccept.html"
                        },
                        new Menu
                        {
                            Text = "待办理申请",
                            Href = "pages/dataReply.html"
                        },
                        new Menu
                        {
                            Text = "待审核申请",
                            Href = "pages/dataCheck.html"
                        },
                        new Menu
                        {
                            Text = "所有申请",
                            Href = "pages/dataAll.html"
                        },
                        new Menu
                        {
                            Text = "新增申请",
                            Href = "pages/dataAdd.html"
                        },
                        new Menu
                        {
                            Text = "依申请公开设置",
                            Href = "pages/settings.html"
                        },
                        new Menu
                        {
                            Text = "数据统计分析",
                            Href = "pages/analysis.html"
                        },
                        new Menu
                        {
                            Text = "依申请公开模板",
                            Href = "pages/templates.html"
                        }
                    }
                })
                .AddDatabaseTable(DataDao.TableName, DataDao.Columns)
                .AddDatabaseTable(FieldDao.TableName, FieldDao.Columns)
                .AddDatabaseTable(ItemDao.TableName, ItemDao.Columns)
                .AddStlElementParser(StlGovApplication.ElementName, StlGovApplication.Parse)
                ;
        }
    }
}