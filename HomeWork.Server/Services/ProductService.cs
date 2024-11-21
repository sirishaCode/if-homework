using HomeWork.Server.Models;
using HomeWork.Server.Repositories;

namespace HomeWork.Server.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProducts()
        {
           
            return await _productRepository.GetProducts();  
        }
    }
}
