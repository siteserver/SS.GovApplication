using System;
using System.Collections.Generic;

namespace SS.GovApplication.Core.Model
{
    public class DataInfo : AttributesImpl
    {
        public int Id { get; set; }

        public int SiteId { get; set; }

        public DateTime AddDate { get; set; }

        public string QueryCode { get; set; }

        public string State { get; set; }

        public bool IsReplied { get; set; }

        public DateTime ReplyDate { get; set; }

        public string ReplyContent { get; set; }

        public bool IsOrganization { get; set; }

        public string CivicName { get; set; }

        public string CivicOrganization { get; set; }

        public string CivicCardType { get; set; }

        public string CivicCardNo { get; set; }

        public string CivicPhone { get; set; }

        public string CivicPostCode { get; set; }

        public string CivicAddress { get; set; }

        public string CivicEmail { get; set; }

        public string CivicFax { get; set; }

        public string OrgName { get; set; }

        public string OrgUnitCode { get; set; }

        public string OrgLegalPerson { get; set; }

        public string OrgLinkName { get; set; }

        public string OrgPhone { get; set; }

        public string OrgPostCode { get; set; }

        public string OrgAddress { get; set; }

        public string OrgEmail { get; set; }

        public string OrgFax { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Purpose { get; set; }

        public bool IsApplyFree { get; set; }

        public string ProvideType { get; set; }

        public string ObtainType { get; set; }

        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }

        public string AttributeValues { get; set; }

        public override Dictionary<string, object> ToDictionary()
        {
            var dict = base.ToDictionary();
            dict[nameof(Id)] = Id;
            dict[nameof(AddDate)] = AddDate;
            dict[nameof(QueryCode)] = QueryCode;
            dict[nameof(State)] = State;
            dict[nameof(IsReplied)] = IsReplied;
            dict[nameof(ReplyDate)] = ReplyDate;
            dict[nameof(ReplyContent)] = ReplyContent;
            dict[nameof(IsOrganization)] = IsOrganization;
            dict[nameof(CivicName)] = CivicName;
            dict[nameof(CivicOrganization)] = CivicOrganization;
            dict[nameof(CivicCardType)] = CivicCardType;
            dict[nameof(CivicCardNo)] = CivicCardNo;
            dict[nameof(CivicPhone)] = CivicPhone;
            dict[nameof(CivicPostCode)] = CivicPostCode;
            dict[nameof(CivicAddress)] = CivicAddress;
            dict[nameof(CivicEmail)] = CivicEmail;
            dict[nameof(CivicFax)] = CivicFax;
            dict[nameof(OrgName)] = OrgName;
            dict[nameof(OrgUnitCode)] = OrgUnitCode;
            dict[nameof(OrgLegalPerson)] = OrgLegalPerson;
            dict[nameof(OrgLinkName)] = OrgLinkName;
            dict[nameof(OrgPhone)] = OrgPhone;
            dict[nameof(OrgPostCode)] = OrgPostCode;
            dict[nameof(OrgAddress)] = OrgAddress;
            dict[nameof(OrgEmail)] = OrgEmail;
            dict[nameof(OrgFax)] = OrgFax;
            dict[nameof(Title)] = Title;
            dict[nameof(Content)] = Content;
            dict[nameof(Purpose)] = Purpose;
            dict[nameof(IsApplyFree)] = IsApplyFree;
            dict[nameof(ProvideType)] = ProvideType;
            dict[nameof(ObtainType)] = ObtainType;
            dict[nameof(DepartmentName)] = DepartmentName;
            dict[nameof(DepartmentId)] = DepartmentId;

            return dict;
        }
    }
}
