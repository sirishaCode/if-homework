using HomeWork.Server.Services;
using Microsoft.AspNetCore.Mvc;
using HomeWork.Server.Models;

namespace HomeWork.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;
      
        public ProductController(IProductService productService)
        {
            _productService = productService;
           
        }


        //To fetch products

        [HttpGet("GetProducts")]
       
        public async Task<IActionResult> GetProducts()
        {
           
            try
            {
               var response = await _productService.GetProducts();

                //if response is empty or null retun empty product list else return productlist
                return (response == null || !response.Any())?  Ok(new List<Product>()): Ok(response); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
