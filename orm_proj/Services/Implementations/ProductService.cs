using orm_proj.Models;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class ProductService : IProductService
    {
        public Task AddProductAsync(Product product)
        {
            var
        }

        public Task DeleteProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> SearchProducts(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
