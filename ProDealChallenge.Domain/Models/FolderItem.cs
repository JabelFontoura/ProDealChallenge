namespace ProDealChallenge.Domain.Models
{
    public class FolderItem
    {
        public int Id { get; private set; }
        public int ExternalId { get; private set; }
        public int? ParentExternalId { get; private set; }
        public string ItemName { get; private set; }
        public int Priority { get; private set; }

        public virtual FolderItem Parent { get; set; }
        public virtual List<FolderItem>? Children { get; set; }

        public FolderItem(int externalId, int? parentExternalId, string itemName, int priority)
        {
            ExternalId = externalId;
            ParentExternalId = parentExternalId;
            ItemName = itemName;
            Priority = priority;
        }
    }
}
