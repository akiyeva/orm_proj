using orm_proj.Repositories.Implementations;
using orm_proj.Repositories.Interfaces;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(ProductPostDto newProduct)
        {
            var isExist = await _productRepository.IsExistAsync(x => x.Name == newProduct.Name);

            if (isExist)
            {
                Console.WriteLine("This product already exists");
                return;
            }

            if (newProduct.Name == null || newProduct.Price < 0)
            {
                throw new InvalidProductException();
            }

            Product product = new Product()
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _getProductById(id);

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<List<ProductGetDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();

            List<ProductGetDto> result = new List<ProductGetDto>();

            products.ForEach(product =>
            {
                ProductGetDto productGet = new ProductGetDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock
                };

                result.Add(productGet);
            });

            return result;
        }

        public async Task<ProductGetDto> GetProductById(int id)
        {
            var product = await _getProductById(id);

            ProductGetDto dto = new ProductGetDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
            return dto;
        }

        public async Task<List<ProductGetDto>> SearchProducts(string term)
        {
            var products = await _productRepository.SearchAsync(x => x.Name.Contains(term));

            List<ProductGetDto> result = new List<ProductGetDto>();

            products.ForEach(product =>
            {
                ProductGetDto productGet = new ProductGetDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock
                };

                result.Add(productGet);
            });

            return result;

        }

        public async Task UpdateProductAsync(ProductPutDto newProduct)
        {
            Product? product = await _getProductById(newProduct.Id);

            var isExist = await _productRepository.IsExistAsync(x => x.Name == newProduct.Name);

            if (isExist)
            {
                Console.WriteLine("This product already exists");
                return;
            }

            if (newProduct.Name == null || newProduct.Price < 0)
            {
                throw new InvalidProductException();
            }

            product.Name = newProduct.Name;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;
            product.Stock = newProduct.Stock;
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }
        public async Task<Product> _getProductById(int id)
        {
            var product = await _productRepository.GetSingleAsync(x => x.Id == id);

            if (product == null)

                throw new NotFoundException("Product not found");

            return product;
        }
    }
}
