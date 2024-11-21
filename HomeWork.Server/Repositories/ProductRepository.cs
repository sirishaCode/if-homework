using HomeWork.Server.Configurations;
using HomeWork.Server.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HomeWork.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ProductRepository(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiSettings.ProductApiUrl);

                // Ensure the response is successful (status code 2xx)
                response.EnsureSuccessStatusCode();

               
                var responseData = await response.Content.ReadAsStringAsync();

                
                if (string.IsNullOrEmpty(responseData))
                {
                    return new List<Product>();
                }


                var productsResponse = JsonSerializer.Deserialize<ProductsResponse>(responseData);

                // Return the list of products or an empty list if no products are found
                return productsResponse?.products ?? new List<Product>();
            }
            catch (Exception ex)
            {

                throw new Exception("Error while fetching products", ex);
            }
        }
    }
}
