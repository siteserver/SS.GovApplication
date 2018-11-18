using System.Collections.Generic;
using System.Data;
using SiteServer.Plugin;
using SS.GovApplication.Core.Model;

namespace SS.GovApplication.Core.Provider
{
    public static class ItemDao
    {
        public const string TableName = "ss_gov_application_item";

        public static List<TableColumn> Columns => new List<TableColumn>
        {
            new TableColumn
            {
                AttributeName = nameof(ItemInfo.Id),
                DataType = DataType.Integer,
                IsIdentity = true,
                IsPrimaryKey = true
            },
            new TableColumn
            {
                AttributeName = nameof(ItemInfo.SiteId),
                DataType = DataType.Integer
            },
            new TableColumn
            {
                AttributeName = nameof(ItemInfo.FieldId),
                DataType = DataType.Integer
            },
            new TableColumn
            {
                AttributeName = nameof(ItemInfo.Value),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(ItemInfo.IsSelected),
                DataType = DataType.Boolean
            },
            new TableColumn
            {
                AttributeName = nameof(ItemInfo.IsExtras),
                DataType = DataType.Boolean
            }
        };

        public static void Insert(IDbTransaction trans, ItemInfo itemInfo)
        {
            var sqlString = $@"INSERT INTO {TableName} (
    {nameof(ItemInfo.SiteId)},
    {nameof(ItemInfo.FieldId)},
    {nameof(ItemInfo.Value)},
    {nameof(ItemInfo.IsSelected)},
    {nameof(ItemInfo.IsExtras)}
) VALUES (
    @{nameof(ItemInfo.SiteId)},
    @{nameof(ItemInfo.FieldId)},
    @{nameof(ItemInfo.Value)},
    @{nameof(ItemInfo.IsSelected)},
    @{nameof(ItemInfo.IsExtras)}
)";

            var parameters = new[]
            {
                Context.DatabaseApi.GetParameter(nameof(ItemInfo.SiteId), itemInfo.SiteId),
                Context.DatabaseApi.GetParameter(nameof(ItemInfo.FieldId), itemInfo.FieldId),
                Context.DatabaseApi.GetParameter(nameof(ItemInfo.Value), itemInfo.Value),
                Context.DatabaseApi.GetParameter(nameof(ItemInfo.IsSelected), itemInfo.IsSelected),
                Context.DatabaseApi.GetParameter(nameof(ItemInfo.IsExtras), itemInfo.IsExtras)
            };

            Context.DatabaseApi.ExecuteNonQuery(trans, sqlString, parameters);
        }

        public static void InsertItems(int siteId, int fieldId, List<ItemInfo> items)
        {
            if (siteId <= 0 || fieldId <= 0 || items == null || items.Count == 0) return;
            
            using (var conn = Context.DatabaseApi.GetConnection(Context.ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var itemInfo in items)
                        {
                            itemInfo.SiteId = siteId;
                            itemInfo.FieldId = fieldId;
                            Insert(trans, itemInfo);
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void DeleteBySiteId(int siteId)
        {
            if (siteId == 0) return;

            var sqlString = $"DELETE FROM {TableName} WHERE {nameof(ItemInfo.SiteId)} = @{nameof(ItemInfo.SiteId)}";

            var parms = new []
			{
				Context.DatabaseApi.GetParameter(nameof(ItemInfo.SiteId), siteId)
			};

            Context.DatabaseApi.ExecuteNonQuery(Context.ConnectionString, sqlString, parms);
        }

        public static void DeleteByFieldId(int fieldId)
        {
            if (fieldId == 0) return;

            var sqlString = $"DELETE FROM {TableName} WHERE {nameof(ItemInfo.FieldId)} = @{nameof(ItemInfo.FieldId)}";

            var parms = new[]
            {
                Context.DatabaseApi.GetParameter(nameof(ItemInfo.FieldId), fieldId)
            };

            Context.DatabaseApi.ExecuteNonQuery(Context.ConnectionString, sqlString, parms);
        }

        public static List<ItemInfo> GetItemInfoList(int fieldId)
        {
            var items = new List<ItemInfo>();

            var sqlString =
                $@"SELECT {nameof(ItemInfo.Id)}, {nameof(ItemInfo.SiteId)}, {nameof(ItemInfo.FieldId)}, {nameof(ItemInfo.Value)}, {nameof(ItemInfo.IsSelected)}, {nameof(ItemInfo.IsExtras)} FROM {TableName} WHERE ({nameof(ItemInfo.FieldId)} = @{nameof(ItemInfo.FieldId)})";

            var parms = new []
			{
                Context.DatabaseApi.GetParameter(nameof(ItemInfo.FieldId), fieldId)
			};

            using (var rdr = Context.DatabaseApi.ExecuteReader(Context.ConnectionString, sqlString, parms))
            {
                while (rdr.Read())
                {
                    items.Add(GetFieldItemInfo(rdr));
                }
                rdr.Close();
            }

            return items;
        }

        private static ItemInfo GetFieldItemInfo(IDataRecord rdr)
        {
            if (rdr == null) return null;

            var itemInfo = new ItemInfo();

            var i = 0;
            itemInfo.Id = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            itemInfo.SiteId = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            itemInfo.FieldId = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            itemInfo.Value = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            itemInfo.IsSelected = !rdr.IsDBNull(i) && rdr.GetBoolean(i);
            i++;
            itemInfo.IsExtras = !rdr.IsDBNull(i) && rdr.GetBoolean(i);

            return itemInfo;
        }

        public static Dictionary<int, List<ItemInfo>> GetAllItems()
        {
            var allDict = new Dictionary<int, List<ItemInfo>>();

            using (var rdr = Context.DatabaseApi.ExecuteReader(Context.ConnectionString, $"SELECT {nameof(ItemInfo.Id)}, {nameof(ItemInfo.SiteId)}, {nameof(ItemInfo.FieldId)}, {nameof(ItemInfo.Value)}, {nameof(ItemInfo.IsSelected)}, {nameof(ItemInfo.IsExtras)} FROM {TableName}"))
            {
                while (rdr.Read())
                {
                    var item = GetFieldItemInfo(rdr);

                    allDict.TryGetValue(item.FieldId, out var list);

                    if (list == null)
                    {
                        list = new List<ItemInfo>();
                    }

                    list.Add(item);

                    allDict[item.FieldId] = list;
                }
                rdr.Close();
            }

            return allDict;
        }
    }
}
