using ASP.Net.Application.Models;

namespace ASP.Net.Application.Abstractions
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        int AddCategory(CategoryDto category);
    }
}
