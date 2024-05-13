namespace BuildingBlock.Catalog.DTOs
{
    public class ResponseProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageURL { get; set; }
        public int? Category_Id { get; set; }
    }
}
