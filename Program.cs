using ASP.Net.Application;
using ASP.Net.Application.Abstractions;
using ASP.Net.Application.Mapper;
using ASP.Net.Application.Query;
using ASP.Net.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.Net.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.GetConnectionString("db");
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IStorageService, StorageService>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();
            builder.Services.AddSingleton<AppDbContext>();


            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>();


            var app = builder.Build();

            app.MapGraphQL();

            app.Run();
        }
    }
}
