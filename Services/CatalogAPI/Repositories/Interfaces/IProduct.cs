namespace CatalogAPI.Repositories.Interfaces
{
    public interface IProduct
    {
        Task<APIResponseDto?> CreateProductAsync(AddProductDto addProductDto);
        Task<APIResponseDto?> GetProductsAsync();
        Task<APIResponseDto?> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<APIResponseDto?> DeleteProductAsync(int id);
        Task<APIResponseDto?> GetProductAsync(int id);
    }
}
