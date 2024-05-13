namespace CatalogAPI.Repositories.SQLCode
{
    public class ProductRepo(CatalogDbContext context) : IProduct
    {
        private readonly CatalogDbContext _context = context;

        public async Task<APIResponseDto?> CreateProductAsync(AddProductDto addProductDto)
        {
            try
            {
                var product = addProductDto.Adapt<PRODUCT>();
                APIResponseDto response = new();
                await _context.PRODUCTS.AddAsync(product);
                await _context.SaveChangesAsync();

                response.isSuccess = true;
                response.displayMessage = "Product Added Successfully";
                response.responseBody = product.Adapt<ResponseProductDto>();

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<APIResponseDto?> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.PRODUCTS.FirstOrDefaultAsync(x => x.Id == id);
                APIResponseDto response = new();
                if (product == null)
                {
                    throw new CustomExceptionHandler(404, $"Product with Id - {id} not found");
                }
                else
                {
                    _context.PRODUCTS.Remove(product);
                    await _context.SaveChangesAsync();
                    response.isSuccess = true;
                    response.displayMessage = "Product Deleted Successfully";
                    return response;
                }
            }
            catch (CustomExceptionHandler ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<APIResponseDto?> GetProductAsync(int id)
        {
            try
            {
                var product = await _context.PRODUCTS.FirstOrDefaultAsync(x => x.Id == id);
                APIResponseDto response = new();
                if (product == null)
                {
                    throw new CustomExceptionHandler(404, $"Product with Id - {id} not found");
                }
                else
                {
                    response.isSuccess = true;
                    response.responseBody = product.Adapt<ResponseProductDto>();
                }
                return response;
            }
            catch (CustomExceptionHandler ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<APIResponseDto?> GetProductsAsync()
        {
            try
            {
                var products = await _context.PRODUCTS.ToListAsync();
                APIResponseDto response = new()
                {
                    isSuccess = true,
                    responseBody = products.Adapt<List<ResponseProductDto>>()
                };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<APIResponseDto?> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            try
            {
                var product = await _context.PRODUCTS.FirstOrDefaultAsync(x => x.Id == updateProductDto.Id);
                APIResponseDto response = new();
                if (product == null)
                {
                    throw new CustomExceptionHandler(404, $"Product with Id - {updateProductDto.Id} not found");
                }
                else
                {
                    _context.Entry(product).CurrentValues.SetValues(updateProductDto);
                    await _context.SaveChangesAsync();
                    response.isSuccess = true;
                    response.displayMessage = "Product Updated Successfully";
                    response.responseBody = product.Adapt<UpdateProductDto>();
                    return response;
                }
            }
            catch (CustomExceptionHandler ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
