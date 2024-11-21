using HomeWork.Server.Models;

namespace HomeWork.Server.Services
{
    public interface IProductService
    {
         Task<List<Product>> GetProducts();
    }
}
