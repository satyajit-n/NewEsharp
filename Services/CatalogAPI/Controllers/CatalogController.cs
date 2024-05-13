namespace CatalogAPI.Controllers
{
    [ApiController]
    public class CatalogController(IProduct productRepo) : ControllerBase
    {
        private readonly IProduct _productRepo = productRepo;

        [HttpPost]
        [Route("/api/product")]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto product)
        {
            try
            {
                var response = await _productRepo.CreateProductAsync(product);
                if (response == null)
                {
                    throw new Exception();
                }
                else
                {
                    return StatusCode(201, response);
                }
            }
            catch (CustomExceptionHandler ex)
            {
                var response = new APIResponseDto
                {
                    isSuccess = false,
                    displayMessage = ex.StatusMessage,
                    supportMessage = new
                    {
                        ex.Message,
                        ex.StackTrace
                    }
                };
                Console.WriteLine(ex);
                return StatusCode(ex.StatusCode, response);
            }
        }

        [HttpGet]
        [Route("/api/products")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var response = await _productRepo.GetProductsAsync();
                if (response == null)
                {
                    throw new Exception();
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new APIResponseDto
                {
                    isSuccess = false,
                    displayMessage = "Something Went Wrong",
                    supportMessage = ex.StackTrace
                };
                Console.WriteLine(ex);
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        [Route("/api/product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productRepo.GetProductAsync(id);
                return Ok(product);

            }
            catch (CustomExceptionHandler ex)
            {
                var response = new APIResponseDto
                {
                    isSuccess = false,
                    displayMessage = ex.StatusMessage,
                    supportMessage = new
                    {
                        ex.Message,
                        ex.StackTrace
                    }
                };
                Console.WriteLine(ex);
                return StatusCode(ex.StatusCode, response);
            }
        }

        [HttpPut]
        [Route("/api/product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProduct)
        {
            try
            {
                var response = await _productRepo.UpdateProductAsync(updateProduct);
                if (response == null)
                {
                    throw new Exception();
                }
                else if (response.isSuccess == false)
                {
                    return NotFound(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new APIResponseDto
                {
                    isSuccess = false,
                    displayMessage = "Something Went Wrong",
                    supportMessage = ex.StackTrace
                };
                Console.WriteLine(ex);
                return StatusCode(500, response);
            }
        }
    }
}
