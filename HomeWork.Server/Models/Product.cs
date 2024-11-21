namespace HomeWork.Server.Models
{
    public class Product
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public List<String>? images {  get; set; }
    }
    public class ProductsResponse
    {
        public List<Product>? products { get; set; }
    }
}
