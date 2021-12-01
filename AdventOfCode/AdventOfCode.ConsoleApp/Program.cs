using AdventOfCode.Core.Builder;
using AdventOfCode.Core.Services;
using AdventOfCode.Domain.Domain;
using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2019;
using AdventOfCode.Year2020;
using AdventOfCode.Year2021;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = SetupDI();
            var logger = serviceProvider.GetService<ILogger<Program>>();
            var assignmentBuilder = serviceProvider.GetService<IAssignmentBuilder>();
            var solutionBuilder = serviceProvider.GetService<ISolutionBuilder>();

            var assignment = GetAssignment(args, assignmentBuilder); // yyyydd(suffix)
            var solution = solutionBuilder.CreateSolution(assignment);

            logger.LogInformation($"Starting");
            var result = solution.GetSolution();
            logger.LogInformation($"Got result: {result}");
            logger.LogInformation($"Done!");
        }

        private static ServiceProvider SetupDI()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        private static Assignment GetAssignment(string[] args, IAssignmentBuilder assignmentBuilder)
        {
            var assignmentString = args.Any() ? args[0] : string.Empty;
            if (string.IsNullOrEmpty(assignmentString))
            {
                Console.WriteLine("What year? (yyyy)");
                var year = Console.ReadLine();
                Console.WriteLine("What day? (d)");
                var day = Console.ReadLine();
                if (day.Length == 1)
                {
                    day = $"0{day}";
                }

                Console.WriteLine("Suffix? (Part2)");
                var suffix = Console.ReadLine();
                assignmentString = $"{year}{day}{suffix}";
            }

            return assignmentBuilder.CreateAssignment(assignmentString);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(configure => configure.AddSerilog())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddSingleton<IAssignmentBuilder, AssignmentBuilder>()
                .AddSingleton<IInputService, InputService>()
                .AddSingleton<ISolutionBuilder, SolutionBuilder>();

            SetupAssembly(services, typeof(Assembly2019));
            SetupAssembly(services, typeof(Assembly2020));
            SetupAssembly(services, typeof(Assembly2021));
        }

        private static void SetupAssembly(IServiceCollection services, Type assemblyType)
        {
            var solutions = Assembly.GetAssembly(assemblyType)
                            .GetTypes()
                            .Where(x => x.GetInterfaces().Contains(typeof(ISolution)))
                            .Where(x => x.IsClass);

            foreach (var solution in solutions)
            {
                services.AddTransient(solution);
            }
        }
    }
}
