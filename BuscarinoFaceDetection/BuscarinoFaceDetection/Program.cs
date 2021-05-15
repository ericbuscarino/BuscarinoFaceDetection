using System;
using System.IO;
using System.IO.Abstractions;
using BuscarinoFaceDetection.Models;
using BuscarinoFaceDetection.Services;
using Google.Cloud.Vision.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
