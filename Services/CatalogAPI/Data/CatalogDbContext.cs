namespace CatalogAPI.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<PRODUCT> PRODUCTS { get; set; }
        public DbSet<CATEGORY> CATEGORIES { get; set; }
    }
}
