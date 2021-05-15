using System.IO.Abstractions;
using BuscarinoFaceDetection.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuscarinoFaceDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<Application>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IHelperServices, HelperServices>();
            services.AddTransient<Application>();
            return services;
        }
    }
}
