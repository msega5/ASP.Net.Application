
using Microsoft.Extensions.DependencyInjection;

namespace ASP.Net.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.GetConnectionString("db");

            builder.Services
                .AddGraphQLServer();

            var app = builder.Build();

            app.Run();
        }
    }
}
