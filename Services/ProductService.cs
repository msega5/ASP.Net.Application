using ASP.Net.Application.Abstractions;
using ASP.Net.Application.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Net.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(ProductDto product)
        {
            using (_context)
            {
                var entity = _mapper.Map<ProductEntity>(product);

                _context.Products.Add(entity);
                _context.SaveChanges();
                _cache.Remove("products");

                return entity.Id;
            }
        }

        
        public IEnumerable<ProductDto> GetProducts()
        {
            using (_context)
            {
                if (_cache.TryGetValue("products", out List<ProductDto> products))
                    return products;

                products =_context.Products.Select(x=>_mapper.Map<ProductDto>(x)).ToList();
                _cache.Set("products", products, TimeSpan.FromMinutes(30));

                return products;

            }
        }

        public IEnumerable<ProductEntity> GetProductsFromStorage()
        {
            using (_context)
            {
                if (_cache.TryGetValue("productsFromStorage", out List<StorageEntity> productsFromStorage))
                    return (IEnumerable<ProductEntity>)productsFromStorage;

                productsFromStorage = _context.Storages.Include(x => x.Products).ToList();
                _cache.Set("productsFromStorage", productsFromStorage, TimeSpan.FromMinutes(30));

                return (IEnumerable<ProductEntity>)productsFromStorage;

            }
        }
    }
}
