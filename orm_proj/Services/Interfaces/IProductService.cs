namespace orm_proj.Services.Interfaces
{
    public interface IProductService
    {
        Task AddProductAsync(ProductPostDto newProduct);
        Task UpdateProductAsync(ProductPutDto newProduct);
        Task DeleteProductAsync(int id);
        Task<ProductGetDto> GetProductById(int id);
        Task<List<ProductGetDto>> GetAllProducts();
        Task<List<ProductGetDto>> SearchProducts(string term);
        Task<decimal> GetProductPrice(int productId);
    }
}
