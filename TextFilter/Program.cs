using Microsoft.Extensions.DependencyInjection;
using System;
using TextFilter.Factories;
using TextFilter.Services;

namespace TextFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();
            var commandLineParser = serviceProvider.GetService<ICommandLineProcessorService>();
            commandLineParser.ProcessCommandLineArguments(args);
        }

        static IServiceProvider CreateServiceProvider()
        {
            return new ServiceCollection()
            .AddLogging()
            .AddScoped<ICommandLineProcessorService, CommandLineProcessorService>()
            .AddScoped<IOutputService, ConsoleOutputService>()
            .AddScoped<IFilterFactory, FilterFactory>()
            .BuildServiceProvider();
        }
    }
}
