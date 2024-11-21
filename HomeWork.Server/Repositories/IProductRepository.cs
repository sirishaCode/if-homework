using HomeWork.Server.Models;

namespace HomeWork.Server.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
    }
}
