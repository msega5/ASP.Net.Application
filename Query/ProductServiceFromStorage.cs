using ASP.Net.Application.Abstractions;
using ASP.Net.Application.Models;

namespace ASP.Net.Application.Query
{
    public class ProductServiceFromStorage
    {               
        public IEnumerable<ProductDto> GetProductsFromStorage([Service] IProductServiceFromStorage service)
        {
            return service.GetProductsFromStorage();
        }
    }
}
