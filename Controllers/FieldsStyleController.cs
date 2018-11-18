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
    [RoutePrefix("fields/style")]
    public class FieldsStyleController : ApiController
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

                var fieldId = request.GetQueryInt("fieldId");
                var fieldInfo = FieldManager.GetFieldInfo(fieldId) ?? new FieldInfo();

                var isRapid = true;
                var rapidValues = string.Empty;
                if (fieldInfo.Items.Count == 0)
                {
                    fieldInfo.Items.Add(new ItemInfo
                    {
                        Value = string.Empty,
                        IsSelected = false
                    });
                }
                else
                {
                    var isSelected = false;
                    var list = new List<string>();
                    foreach (var item in fieldInfo.Items)
                    {
                        list.Add(item.Value);
                        if (item.IsSelected)
                        {
                            isSelected = true;
                        }
                    }

                    isRapid = !isSelected;
                    rapidValues = string.Join("\r\n", list);
                }

                return Ok(new
                {
                    Value = fieldInfo,
                    IsRapid = isRapid,
                    RapidValues = rapidValues
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
                var siteId = request.GetQueryInt("siteId");
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSitePermissions(siteId, Utils.PluginId)) return Unauthorized();

                var fieldId = request.GetPostInt("fieldId");
                var isRapid = request.GetPostBool("isRapid");
                var rapidValues = request.GetPostString("rapidValues");
                var rapidValueArray = rapidValues.Split('\n');
                var rapidValueList = new List<string>();
                foreach (var item in rapidValueArray)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        rapidValueList.Add(item.Trim());
                    }
                }

                var body = request.GetPostObject<FieldInfo>("fieldInfo");

                var fieldInfoDatabase =
                    FieldManager.GetFieldInfo(fieldId) ??
                    new FieldInfo();

                string errorMessage;
                var isSuccess = fieldInfoDatabase.Id == 0 ? InsertFieldInfo(siteId, body, isRapid, rapidValueList, out errorMessage) : UpdateFieldInfo(fieldInfoDatabase, body, isRapid, rapidValueList, out errorMessage);

                if (!isSuccess)
                {
                    return BadRequest(errorMessage);
                }

                return Ok(new{});
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private bool InsertFieldInfo(int siteId, FieldInfo body, bool isRapid, List<string> rapidValues, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(body.Title))
            {
                errorMessage = "操作失败，字段名不能为空！";
                return false;
            }

            if (FieldDao.IsTitleExists(siteId, body.Title))
            {
                errorMessage = $@"显示样式添加失败：字段名""{body.Title}""已存在";
                return false;
            }

            var fieldInfo = new FieldInfo
            {
                SiteId = siteId,
                Title = body.Title,
                Taxis = body.Taxis,
                FieldType = body.FieldType,
                Items = new List<ItemInfo>()
            };

            if (body.FieldType == InputType.CheckBox.Value || body.FieldType == InputType.Radio.Value || body.FieldType == InputType.SelectMultiple.Value || body.FieldType == InputType.SelectOne.Value)
            {
                if (isRapid)
                {
                    foreach (var rapidValue in rapidValues)
                    {
                        var itemInfo = new ItemInfo
                        {
                            SiteId = siteId,
                            Value = rapidValue
                        };
                        fieldInfo.Items.Add(itemInfo);
                    }
                }
                else
                {
                    var isHasSelected = false;
                    foreach (var styleItem in body.Items)
                    {
                        if (body.FieldType != InputType.SelectMultiple.Value && body.FieldType != InputType.CheckBox.Value && isHasSelected && styleItem.IsSelected)
                        {
                            errorMessage = "操作失败，只能有一个初始化时选定项！";
                            return false;
                        }
                        if (styleItem.IsSelected) isHasSelected = true;

                        var itemInfo = new ItemInfo
                        {
                            Value = styleItem.Value,
                            IsSelected = styleItem.IsSelected
                        };
                        fieldInfo.Items.Add(itemInfo);
                    }
                }
            }

            FieldDao.Insert(siteId, fieldInfo);

            return true;
        }

        private bool UpdateFieldInfo(FieldInfo fieldInfo, FieldInfo body, bool isRapid, List<string> rapidValues, out string errorMessage)
        {
            errorMessage = string.Empty;

            fieldInfo.Title = body.Title;
            fieldInfo.Taxis = body.Taxis;
            fieldInfo.FieldType = body.FieldType;
            fieldInfo.Items = new List<ItemInfo>();

            if (body.FieldType == InputType.CheckBox.Value || body.FieldType == InputType.Radio.Value || body.FieldType == InputType.SelectMultiple.Value || body.FieldType == InputType.SelectOne.Value)
            {
                if (isRapid)
                {
                    foreach (var rapidValue in rapidValues)
                    {
                        var itemInfo = new ItemInfo
                        {
                            FieldId = fieldInfo.Id,
                            Value = rapidValue
                        };
                        fieldInfo.Items.Add(itemInfo);
                    }
                }
                else
                {
                    var isHasSelected = false;
                    foreach (var styleItem in body.Items)
                    {
                        if (body.FieldType != InputType.SelectMultiple.Value && body.FieldType != InputType.CheckBox.Value && isHasSelected && styleItem.IsSelected)
                        {
                            errorMessage = "操作失败，只能有一个初始化时选定项！";
                            return false;
                        }
                        if (styleItem.IsSelected) isHasSelected = true;

                        var itemInfo = new ItemInfo
                        {
                            FieldId = fieldInfo.Id,
                            Value = styleItem.Value,
                            IsSelected = styleItem.IsSelected
                        };
                        fieldInfo.Items.Add(itemInfo);
                    }
                }
            }

            FieldDao.Update(fieldInfo, true);
            
            return true;
        }
    }
}
