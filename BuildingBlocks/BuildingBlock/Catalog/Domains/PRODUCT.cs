using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingBlock.Catalog.Domains
{
    public class PRODUCT
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("image_url")]
        public string? ImageURL { get; set; }

        [ForeignKey("CATEGORY")]
        [Column("category_Id")]
        public int? Category_Id { get; set; }
        public CATEGORY? CATEGORY { get; set; }
    }
}
