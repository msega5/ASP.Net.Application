using ASP.Net.Application;
using ASP.Net.Application.Abstractions;
using ASP.Net.Application.Mapper;
using ASP.Net.Application.Mutations;
using ASP.Net.Application.Query;
using ASP.Net.Application.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.Net.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Configuration.GetConnectionString("db");
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddPooledDbContextFactory<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("db")));

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IStorageService, StorageService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });
            //builder.Services.AddDbContext<AppDbContext>(conf=>conf.UseNpgsql(builder.Configuration.GetConnectionString("db")));


            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>()
                .AddMutationType<MySimpleMutation>();


            var app = builder.Build();

            app.MapGraphQL();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();
        }
    }
}
