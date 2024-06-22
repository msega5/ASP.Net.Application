using ASP.Net.Application.Abstractions;
using ASP.Net.Application.Models;

namespace ASP.Net.Application.Mutations
{
    public class AddProductMutation
    {
        public int AddProduct([Service] IProductService service, ProductDto product)
        {
            var id = service.AddProduct(product);
            return id;
        }
    }
}
