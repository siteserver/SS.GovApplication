namespace SS.GovApplication.Core.Model
{
	public class ItemInfo
	{
        public int Id { get; set; }

	    public int SiteId { get; set; }

        public int FieldId { get; set; }

        public string Value { get; set; }

	    public bool IsSelected { get; set; }

        public bool IsExtras { get; set; }
    }
}
