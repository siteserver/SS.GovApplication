using System;
using System.Collections.Generic;
using System.Data;
using SiteServer.Plugin;
using SS.GovApplication.Core.Model;
using SS.GovApplication.Core.Utils;

/*
 *
CREATE TABLE [dbo].[wcm_GovPublicApply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StyleID] [int] NOT NULL,
	[PublishmentSystemID] [int] NOT NULL,
	[IsOrganization] [varchar](18) NOT NULL,
	[CivicName] [nvarchar](255) NOT NULL,
	[CivicOrganization] [nvarchar](255) NOT NULL,
	[CivicCardType] [nvarchar](255) NOT NULL,
	[CivicCardNo] [nvarchar](255) NOT NULL,
	[CivicPhone] [varchar](50) NOT NULL,
	[CivicPostCode] [varchar](50) NOT NULL,
	[CivicAddress] [nvarchar](255) NOT NULL,
	[CivicEmail] [nvarchar](255) NOT NULL,
	[CivicFax] [varchar](50) NOT NULL,
	[OrgName] [nvarchar](255) NOT NULL,
	[OrgUnitCode] [nvarchar](255) NOT NULL,
	[OrgLegalPerson] [nvarchar](255) NOT NULL,
	[OrgLinkName] [nvarchar](255) NOT NULL,
	[OrgPhone] [varchar](50) NOT NULL,
	[OrgPostCode] [varchar](50) NOT NULL,
	[OrgAddress] [nvarchar](255) NOT NULL,
	[OrgEmail] [nvarchar](255) NOT NULL,
	[OrgFax] [varchar](50) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Content] [ntext] NOT NULL,
	[Purpose] [nvarchar](255) NOT NULL,
	[IsApplyFree] [varchar](18) NOT NULL,
	[ProvideType] [varchar](50) NOT NULL,
	[ObtainType] [varchar](50) NOT NULL,
	[DepartmentName] [nvarchar](255) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[QueryCode] [nvarchar](255) NOT NULL,
	[State] [varchar](50) NOT NULL,
 CONSTRAINT [PK_wcm_GovPublicApply] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[wcm_GovPublicApplyLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[PublishmentSystemID] [int] NOT NULL,
	[ApplyID] [int] NULL,
	[DepartmentID] [int] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[LogType] [varchar](50) NOT NULL,
	[IPAddress] [varchar](50) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[Summary] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_wcm_GovPublicApplyLog] PRIMARY KEY NONCLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[wcm_GovPublicApplyRemark](
	[RemarkID] [int] IDENTITY(1,1) NOT NULL,
	[PublishmentSystemID] [int] NOT NULL,
	[ApplyID] [int] NULL,
	[RemarkType] [varchar](50) NOT NULL,
	[Remark] [nvarchar](255) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[AddDate] [datetime] NOT NULL,
 CONSTRAINT [PK_wcm_GovPublicApplyRemark] PRIMARY KEY NONCLUSTERED 
(
	[RemarkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[wcm_GovPublicApplyReply](
	[ReplyID] [int] IDENTITY(1,1) NOT NULL,
	[PublishmentSystemID] [int] NOT NULL,
	[ApplyID] [int] NULL,
	[Reply] [ntext] NOT NULL,
	[FileUrl] [nvarchar](255) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[AddDate] [datetime] NOT NULL,
 CONSTRAINT [PK_wcm_GovPublicApplyReply] PRIMARY KEY NONCLUSTERED 
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 *
 */

namespace SS.GovApplication.Core.Provider
{
    public static class DataDao
    {
        public const string TableName = "ss_gov_application_data";

        public static List<TableColumn> Columns => new List<TableColumn>
        {
            new TableColumn
            {
                AttributeName = nameof(DataInfo.Id),
                DataType = DataType.Integer,
                IsIdentity = true,
                IsPrimaryKey = true
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.SiteId),
                DataType = DataType.Integer
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.AddDate),
                DataType = DataType.DateTime
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.QueryCode),
                DataType = DataType.VarChar,
                DataLength = 50
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.State),
                DataType = DataType.VarChar,
                DataLength = 50
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.IsReplied),
                DataType = DataType.Boolean
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.ReplyDate),
                DataType = DataType.DateTime
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.ReplyContent),
                DataType = DataType.Text
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.IsOrganization),
                DataType = DataType.Boolean
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicName),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicOrganization),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicCardType),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicCardNo),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicPhone),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicPostCode),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicAddress),
                DataType = DataType.VarChar,
                DataLength = 500
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicEmail),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.CivicFax),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgName),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgUnitCode),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgLegalPerson),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgLinkName),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgPhone),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgPostCode),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgAddress),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgEmail),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.OrgFax),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.Title),
                DataType = DataType.VarChar,
                DataLength = 500
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.Content),
                DataType = DataType.Text
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.Purpose),
                DataType = DataType.VarChar,
                DataLength = 500
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.IsApplyFree),
                DataType = DataType.Boolean
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.ProvideType),
                DataType = DataType.VarChar,
                DataLength = 50
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.ObtainType),
                DataType = DataType.VarChar,
                DataLength = 50
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.DepartmentName),
                DataType = DataType.VarChar,
                DataLength = 500
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.DepartmentId),
                DataType = DataType.Integer
            },
            new TableColumn
            {
                AttributeName = nameof(DataInfo.AttributeValues),
                DataType = DataType.Text
            }
        };

        public static int Insert(DataInfo dataInfo)
        {
            var sqlString = $@"INSERT INTO {TableName}
(
    {nameof(DataInfo.SiteId)},
    {nameof(DataInfo.AddDate)},
    {nameof(DataInfo.QueryCode)},
    {nameof(DataInfo.State)},
    {nameof(DataInfo.IsReplied)},
    {nameof(DataInfo.ReplyDate)},
    {nameof(DataInfo.ReplyContent)},
    {nameof(DataInfo.IsOrganization)},
    {nameof(DataInfo.CivicName)},
    {nameof(DataInfo.CivicOrganization)},
    {nameof(DataInfo.CivicCardType)},
    {nameof(DataInfo.CivicCardNo)},
    {nameof(DataInfo.CivicPhone)},
    {nameof(DataInfo.CivicPostCode)},
    {nameof(DataInfo.CivicAddress)},
    {nameof(DataInfo.CivicEmail)},
    {nameof(DataInfo.CivicFax)},
    {nameof(DataInfo.OrgName)},
    {nameof(DataInfo.OrgUnitCode)},
    {nameof(DataInfo.OrgLegalPerson)},
    {nameof(DataInfo.OrgLinkName)},
    {nameof(DataInfo.OrgPhone)},
    {nameof(DataInfo.OrgPostCode)},
    {nameof(DataInfo.OrgAddress)},
    {nameof(DataInfo.OrgEmail)},
    {nameof(DataInfo.OrgFax)},
    {nameof(DataInfo.Title)},
    {nameof(DataInfo.Content)},
    {nameof(DataInfo.Purpose)},
    {nameof(DataInfo.IsApplyFree)},
    {nameof(DataInfo.ProvideType)},
    {nameof(DataInfo.ObtainType)},
    {nameof(DataInfo.DepartmentName)},
    {nameof(DataInfo.DepartmentId)},
    {nameof(DataInfo.AttributeValues)}
) VALUES (
    @{nameof(DataInfo.SiteId)},
    @{nameof(DataInfo.AddDate)},
    @{nameof(DataInfo.QueryCode)},
    @{nameof(DataInfo.State)},
    @{nameof(DataInfo.IsReplied)},
    @{nameof(DataInfo.ReplyDate)},
    @{nameof(DataInfo.ReplyContent)},
    @{nameof(DataInfo.IsOrganization)},
    @{nameof(DataInfo.CivicName)},
    @{nameof(DataInfo.CivicOrganization)},
    @{nameof(DataInfo.CivicCardType)},
    @{nameof(DataInfo.CivicCardNo)},
    @{nameof(DataInfo.CivicPhone)},
    @{nameof(DataInfo.CivicPostCode)},
    @{nameof(DataInfo.CivicAddress)},
    @{nameof(DataInfo.CivicEmail)},
    @{nameof(DataInfo.CivicFax)},
    @{nameof(DataInfo.OrgName)},
    @{nameof(DataInfo.OrgUnitCode)},
    @{nameof(DataInfo.OrgLegalPerson)},
    @{nameof(DataInfo.OrgLinkName)},
    @{nameof(DataInfo.OrgPhone)},
    @{nameof(DataInfo.OrgPostCode)},
    @{nameof(DataInfo.OrgAddress)},
    @{nameof(DataInfo.OrgEmail)},
    @{nameof(DataInfo.OrgFax)},
    @{nameof(DataInfo.Title)},
    @{nameof(DataInfo.Content)},
    @{nameof(DataInfo.Purpose)},
    @{nameof(DataInfo.IsApplyFree)},
    @{nameof(DataInfo.ProvideType)},
    @{nameof(DataInfo.ObtainType)},
    @{nameof(DataInfo.DepartmentName)},
    @{nameof(DataInfo.DepartmentId)},
    @{nameof(DataInfo.AttributeValues)}
)";

            var parameters = new List<IDataParameter>
            {
                Context.DatabaseApi.GetParameter(nameof(DataInfo.SiteId), dataInfo.SiteId),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.AddDate), dataInfo.AddDate),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.QueryCode), dataInfo.QueryCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.State), dataInfo.State),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.IsReplied), dataInfo.IsReplied),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ReplyDate), dataInfo.ReplyDate),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ReplyContent), dataInfo.ReplyContent),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.IsOrganization), dataInfo.IsOrganization),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicName), dataInfo.CivicName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicOrganization), dataInfo.CivicOrganization),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicCardType), dataInfo.CivicCardType),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicCardNo), dataInfo.CivicCardNo),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicPhone), dataInfo.CivicPhone),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicPostCode), dataInfo.CivicPostCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicAddress), dataInfo.CivicAddress),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicEmail), dataInfo.CivicEmail),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicFax), dataInfo.CivicFax),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgName), dataInfo.OrgName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgUnitCode), dataInfo.OrgUnitCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgLegalPerson), dataInfo.OrgLegalPerson),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgLinkName), dataInfo.OrgLinkName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgPhone), dataInfo.OrgPhone),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgPostCode), dataInfo.OrgPostCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgAddress), dataInfo.OrgAddress),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgEmail), dataInfo.OrgEmail),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgFax), dataInfo.OrgFax),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Title), dataInfo.Title),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Content), dataInfo.Content),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Purpose), dataInfo.Purpose),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.IsApplyFree), dataInfo.IsApplyFree),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ProvideType), dataInfo.ProvideType),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ObtainType), dataInfo.ObtainType),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.DepartmentName), dataInfo.DepartmentName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.DepartmentId), dataInfo.DepartmentId),
                Context.DatabaseApi.GetParameter(nameof(dataInfo.AttributeValues), dataInfo.ToString())
            };

            var dataId = Context.DatabaseApi.ExecuteNonQueryAndReturnId(TableName, nameof(DataInfo.Id),
                Context.ConnectionString, sqlString, parameters.ToArray());

            DataCountManager.ClearCache(dataInfo.SiteId);

            return dataId;
        }

        public static void Update(DataInfo dataInfo)
        {
            var sqlString = $@"UPDATE {TableName} SET
    {nameof(DataInfo.SiteId)} = @{nameof(DataInfo.SiteId)},
    {nameof(DataInfo.AddDate)} = @{nameof(DataInfo.AddDate)},
    {nameof(DataInfo.QueryCode)} = @{nameof(DataInfo.QueryCode)},
    {nameof(DataInfo.State)} = @{nameof(DataInfo.State)},
    {nameof(DataInfo.IsReplied)} = @{nameof(DataInfo.IsReplied)},
    {nameof(DataInfo.ReplyDate)} = @{nameof(DataInfo.ReplyDate)},
    {nameof(DataInfo.ReplyContent)} = @{nameof(DataInfo.ReplyContent)},
    {nameof(DataInfo.IsOrganization)} = @{nameof(DataInfo.IsOrganization)},
    {nameof(DataInfo.CivicName)} = @{nameof(DataInfo.CivicName)},
    {nameof(DataInfo.CivicOrganization)} = @{nameof(DataInfo.CivicOrganization)},
    {nameof(DataInfo.CivicCardType)} = @{nameof(DataInfo.CivicCardType)},
    {nameof(DataInfo.CivicCardNo)} = @{nameof(DataInfo.CivicCardNo)},
    {nameof(DataInfo.CivicPhone)} = @{nameof(DataInfo.CivicPhone)},
    {nameof(DataInfo.CivicPostCode)} = @{nameof(DataInfo.CivicPostCode)},
    {nameof(DataInfo.CivicAddress)} = @{nameof(DataInfo.CivicAddress)},
    {nameof(DataInfo.CivicEmail)} = @{nameof(DataInfo.CivicEmail)},
    {nameof(DataInfo.CivicFax)} = @{nameof(DataInfo.CivicFax)},
    {nameof(DataInfo.OrgName)} = @{nameof(DataInfo.OrgName)},
    {nameof(DataInfo.OrgUnitCode)} = @{nameof(DataInfo.OrgUnitCode)},
    {nameof(DataInfo.OrgLegalPerson)} = @{nameof(DataInfo.OrgLegalPerson)},
    {nameof(DataInfo.OrgLinkName)} = @{nameof(DataInfo.OrgLinkName)},
    {nameof(DataInfo.OrgPhone)} = @{nameof(DataInfo.OrgPhone)},
    {nameof(DataInfo.OrgPostCode)} = @{nameof(DataInfo.OrgPostCode)},
    {nameof(DataInfo.OrgAddress)} = @{nameof(DataInfo.OrgAddress)},
    {nameof(DataInfo.OrgEmail)} = @{nameof(DataInfo.OrgEmail)},
    {nameof(DataInfo.OrgFax)} = @{nameof(DataInfo.OrgFax)},
    {nameof(DataInfo.Title)} = @{nameof(DataInfo.Title)},
    {nameof(DataInfo.Content)} = @{nameof(DataInfo.Content)},
    {nameof(DataInfo.Purpose)} = @{nameof(DataInfo.Purpose)},
    {nameof(DataInfo.IsApplyFree)} = @{nameof(DataInfo.IsApplyFree)},
    {nameof(DataInfo.ProvideType)} = @{nameof(DataInfo.ProvideType)},
    {nameof(DataInfo.ObtainType)} = @{nameof(DataInfo.ObtainType)},
    {nameof(DataInfo.DepartmentName)} = @{nameof(DataInfo.DepartmentName)},
    {nameof(DataInfo.DepartmentId)} = @{nameof(DataInfo.DepartmentId)},
    {nameof(DataInfo.AttributeValues)} = @{nameof(DataInfo.AttributeValues)}
WHERE Id = @Id";

            var parameters = new List<IDataParameter>
            {
                Context.DatabaseApi.GetParameter(nameof(DataInfo.SiteId), dataInfo.SiteId),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.AddDate), dataInfo.AddDate),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.QueryCode), dataInfo.QueryCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.State), dataInfo.State),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.IsReplied), dataInfo.IsReplied),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ReplyDate), dataInfo.ReplyDate),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ReplyContent), dataInfo.ReplyContent),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.IsOrganization), dataInfo.IsOrganization),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicName), dataInfo.CivicName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicOrganization), dataInfo.CivicOrganization),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicCardType), dataInfo.CivicCardType),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicCardNo), dataInfo.CivicCardNo),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicPhone), dataInfo.CivicPhone),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicPostCode), dataInfo.CivicPostCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicAddress), dataInfo.CivicAddress),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicEmail), dataInfo.CivicEmail),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.CivicFax), dataInfo.CivicFax),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgName), dataInfo.OrgName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgUnitCode), dataInfo.OrgUnitCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgLegalPerson), dataInfo.OrgLegalPerson),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgLinkName), dataInfo.OrgLinkName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgPhone), dataInfo.OrgPhone),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgPostCode), dataInfo.OrgPostCode),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgAddress), dataInfo.OrgAddress),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgEmail), dataInfo.OrgEmail),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.OrgFax), dataInfo.OrgFax),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Title), dataInfo.Title),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Content), dataInfo.Content),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Purpose), dataInfo.Purpose),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.IsApplyFree), dataInfo.IsApplyFree),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ProvideType), dataInfo.ProvideType),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ObtainType), dataInfo.ObtainType),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.DepartmentName), dataInfo.DepartmentName),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.DepartmentId), dataInfo.DepartmentId),
                Context.DatabaseApi.GetParameter(nameof(dataInfo.AttributeValues), dataInfo.ToString()),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Id), dataInfo.Id)
            };

            Context.DatabaseApi.ExecuteNonQuery(Context.ConnectionString, sqlString, parameters.ToArray());
        }

        public static void Reply(DataInfo dataInfo)
        {
            var sqlString = $@"UPDATE {TableName} SET
    {nameof(DataInfo.IsReplied)} = @{nameof(DataInfo.IsReplied)},
    {nameof(DataInfo.ReplyDate)} = @{nameof(DataInfo.ReplyDate)},
    {nameof(DataInfo.ReplyContent)} = @{nameof(DataInfo.ReplyContent)}
WHERE Id = @Id";

            var parameters = new List<IDataParameter>
            {
                Context.DatabaseApi.GetParameter(nameof(DataInfo.IsReplied), true),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ReplyDate), DateTime.Now),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.ReplyContent), dataInfo.ReplyContent),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.Id), dataInfo.Id)
            };

            Context.DatabaseApi.ExecuteNonQuery(Context.ConnectionString, sqlString, parameters.ToArray());
        }

        public static void Delete(DataInfo dataInfo)
        {
            var sqlString =
                $"DELETE FROM {TableName} WHERE {nameof(DataInfo.Id)} = {dataInfo.Id}";
            Context.DatabaseApi.ExecuteNonQuery(Context.ConnectionString, sqlString);

            DataCountManager.ClearCache(dataInfo.SiteId);
        }

        private static int GetIntResult(string sqlString, List<IDataParameter> parameters)
        {
            var count = 0;

            using (var conn = Context.DatabaseApi.GetConnection(Context.ConnectionString))
            {
                conn.Open();
                using (var rdr = Context.DatabaseApi.ExecuteReader(conn, sqlString, parameters.ToArray()))
                {
                    if (rdr.Read() && !rdr.IsDBNull(0))
                    {
                        count = rdr.GetInt32(0);
                    }

                    rdr.Close();
                }
            }

            return count;
        }

        private static int GetCount(int siteId, string state)
        {
            var sqlString =
                $"SELECT COUNT(*) FROM {TableName} WHERE {nameof(DataInfo.SiteId)} = @{nameof(DataInfo.SiteId)} AND {nameof(DataInfo.State)} = @{nameof(DataInfo.State)}";

            var parameters = new List<IDataParameter>
            {
                Context.DatabaseApi.GetParameter(nameof(DataInfo.SiteId), siteId),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.State), state)
            };

            return GetIntResult(sqlString, parameters);
        }

        public static List<DataInfo> GetDataInfoList(int siteId, string state, int page)
        {
            List<DataInfo> dataInfoList;
            var totalCount = DataCountManager.GetCount(siteId, state);
            if (totalCount == 0)
            {
                dataInfoList = new List<DataInfo>();
            }
            else if (totalCount <= Utils.Utils.PageSize)
            {
                dataInfoList = GetDataInfoList(siteId, state, 0, totalCount);
            }
            else
            {
                if (page == 0) page = 1;
                var offset = (page - 1) * Utils.Utils.PageSize;
                var limit = totalCount - offset > Utils.Utils.PageSize
                    ? Utils.Utils.PageSize
                    : totalCount - offset;
                dataInfoList = GetDataInfoList(siteId, state, offset, limit);
            }

            return dataInfoList;
        }

        public static List<DataInfo> GetDataInfoList(int siteId, string state, int offset, int limit)
        {
            var dataInfoList = new List<DataInfo>();

            var whereString = $"WHERE {nameof(DataInfo.SiteId)} = @{nameof(DataInfo.SiteId)} AND {nameof(DataInfo.State)} = @{nameof(DataInfo.State)}";

            var sqlString = Context.DatabaseApi.GetPageSqlString(TableName, $@"{nameof(DataInfo.Id)},
{nameof(DataInfo.SiteId)},
{nameof(DataInfo.AddDate)},
{nameof(DataInfo.QueryCode)},
{nameof(DataInfo.State)},
{nameof(DataInfo.IsReplied)},
{nameof(DataInfo.ReplyDate)},
{nameof(DataInfo.ReplyContent)},
{nameof(DataInfo.IsOrganization)},
{nameof(DataInfo.CivicName)},
{nameof(DataInfo.CivicOrganization)},
{nameof(DataInfo.CivicCardType)},
{nameof(DataInfo.CivicCardNo)},
{nameof(DataInfo.CivicPhone)},
{nameof(DataInfo.CivicPostCode)},
{nameof(DataInfo.CivicAddress)},
{nameof(DataInfo.CivicEmail)},
{nameof(DataInfo.CivicFax)},
{nameof(DataInfo.OrgName)},
{nameof(DataInfo.OrgUnitCode)},
{nameof(DataInfo.OrgLegalPerson)},
{nameof(DataInfo.OrgLinkName)},
{nameof(DataInfo.OrgPhone)},
{nameof(DataInfo.OrgPostCode)},
{nameof(DataInfo.OrgAddress)},
{nameof(DataInfo.OrgEmail)},
{nameof(DataInfo.OrgFax)},
{nameof(DataInfo.Title)},
{nameof(DataInfo.Content)},
{nameof(DataInfo.Purpose)},
{nameof(DataInfo.IsApplyFree)},
{nameof(DataInfo.ProvideType)},
{nameof(DataInfo.ObtainType)},
{nameof(DataInfo.DepartmentName)},
{nameof(DataInfo.DepartmentId)},
{nameof(DataInfo.AttributeValues)}", whereString,
                $"ORDER BY {nameof(DataInfo.IsReplied)}, {nameof(DataInfo.Id)} DESC", offset, limit);

            var parameters = new List<IDataParameter>
            {
                Context.DatabaseApi.GetParameter(nameof(DataInfo.SiteId), siteId),
                Context.DatabaseApi.GetParameter(nameof(DataInfo.State), state)
            };

            using (var rdr =
                Context.DatabaseApi.ExecuteReader(Context.ConnectionString, sqlString, parameters.ToArray()))
            {
                while (rdr.Read())
                {
                    var formLogInfo = GetDataInfo(rdr);
                    if (!string.IsNullOrEmpty(formLogInfo.AttributeValues))
                    {
                        dataInfoList.Add(formLogInfo);
                    }
                }

                rdr.Close();
            }

            return dataInfoList;
        }

        public static DataInfo GetDataInfo(int dataId)
        {
            DataInfo dataInfo = null;

            var sqlString =
                $@"SELECT {nameof(DataInfo.Id)},
    {nameof(DataInfo.SiteId)},
    {nameof(DataInfo.AddDate)},
    {nameof(DataInfo.QueryCode)},
    {nameof(DataInfo.State)},
    {nameof(DataInfo.IsReplied)},
    {nameof(DataInfo.ReplyDate)},
    {nameof(DataInfo.ReplyContent)},
    {nameof(DataInfo.IsOrganization)},
    {nameof(DataInfo.CivicName)},
    {nameof(DataInfo.CivicOrganization)},
    {nameof(DataInfo.CivicCardType)},
    {nameof(DataInfo.CivicCardNo)},
    {nameof(DataInfo.CivicPhone)},
    {nameof(DataInfo.CivicPostCode)},
    {nameof(DataInfo.CivicAddress)},
    {nameof(DataInfo.CivicEmail)},
    {nameof(DataInfo.CivicFax)},
    {nameof(DataInfo.OrgName)},
    {nameof(DataInfo.OrgUnitCode)},
    {nameof(DataInfo.OrgLegalPerson)},
    {nameof(DataInfo.OrgLinkName)},
    {nameof(DataInfo.OrgPhone)},
    {nameof(DataInfo.OrgPostCode)},
    {nameof(DataInfo.OrgAddress)},
    {nameof(DataInfo.OrgEmail)},
    {nameof(DataInfo.OrgFax)},
    {nameof(DataInfo.Title)},
    {nameof(DataInfo.Content)},
    {nameof(DataInfo.Purpose)},
    {nameof(DataInfo.IsApplyFree)},
    {nameof(DataInfo.ProvideType)},
    {nameof(DataInfo.ObtainType)},
    {nameof(DataInfo.DepartmentName)},
    {nameof(DataInfo.DepartmentId)},
    {nameof(DataInfo.AttributeValues)}
            FROM {TableName} WHERE {nameof(DataInfo.Id)} = {dataId}";

            using (var rdr = Context.DatabaseApi.ExecuteReader(Context.ConnectionString, sqlString))
            {
                if (rdr.Read())
                {
                    dataInfo = GetDataInfo(rdr);
                }

                rdr.Close();
            }

            return dataInfo;
        }

        public static Dictionary<string, int> GetDataCounts(int siteId)
        {
            var dict = new Dictionary<string, int>();
            foreach (var state in DataState.AllStates)
            {
                dict[state.Value] = GetCount(siteId, state.Value);
            }

            return dict;
        }

        private static DataInfo GetDataInfo(IDataRecord rdr)
        {
            if (rdr == null) return null;

            var dataInfo = new DataInfo();

            var i = 0;
            dataInfo.Id = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            dataInfo.SiteId = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            dataInfo.AddDate = rdr.IsDBNull(i) ? DateTime.Now : rdr.GetDateTime(i);
            i++;
            dataInfo.QueryCode = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.State = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.IsReplied = !rdr.IsDBNull(i) && rdr.GetBoolean(i);
            i++;
            dataInfo.ReplyDate = rdr.IsDBNull(i) ? DateTime.Now : rdr.GetDateTime(i);
            i++;
            dataInfo.ReplyContent = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.IsOrganization = !rdr.IsDBNull(i) && rdr.GetBoolean(i);
            i++;
            dataInfo.CivicName = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicOrganization = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicCardType = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicCardNo = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicPhone = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicPostCode = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicAddress = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicEmail = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.CivicFax = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgName = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgUnitCode = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgLegalPerson = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgLinkName = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgPhone = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgPostCode = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgAddress = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgEmail = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.OrgFax = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.Title = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.Content = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.Purpose = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.IsApplyFree = !rdr.IsDBNull(i) && rdr.GetBoolean(i);
            i++;
            dataInfo.ProvideType = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.ObtainType = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.DepartmentName = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            dataInfo.DepartmentId = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            dataInfo.AttributeValues = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);

            dataInfo.Load(dataInfo.AttributeValues);

            return dataInfo;
        }
    }
}
