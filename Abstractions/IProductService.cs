using ASP.Net.Application.Models;

namespace ASP.Net.Application.Abstractions
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        int AddProduct(ProductDto product);
    }   
}
