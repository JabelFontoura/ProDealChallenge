namespace ProDeal.Application.Dtos
{
    public  class FolderItemDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string ItemName { get; set; }
        public int Priority { get; set; }
        public string PathName { get; set; }
    }
}
