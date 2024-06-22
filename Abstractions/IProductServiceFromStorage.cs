using ASP.Net.Application.Models;

namespace ASP.Net.Application.Abstractions
{
    public interface IProductServiceFromStorage
    {
        IEnumerable<ProductDto> GetProductsFromStorage();
        int GetProductsFromStorage(StorageDto Id, ProductDto product);        
    }
}
