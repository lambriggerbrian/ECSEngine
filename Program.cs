using System;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Simulation;
using Simulation.CommandSystem;

namespace Engine
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Hello World!");
            IHost host = CreateHostBuilder(args).Build();
            host.Services.GetService<App>().Run();
            return;
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(provider =>
                    {
                        return new CancellationTokenSource();
                    });
                    services.AddSingleton(provider =>
                    {
                        return Console.OpenStandardInput();
                    });
                    services.AddSingleton<IEntityManager, EntityManager>();
                    services.AddSingleton(provider =>
                    {
                        var factory = provider.GetRequiredService<ILoggerFactory>();
                        var entityManager = provider.GetRequiredService<IEntityManager>();
                        var collection = new ComponentManagerCollection(factory);
                        collection.AddComponentManager(new TransformComponentManager(factory, entityManager));
                        return collection;
                    });
                    services.AddSingleton<World, World>();
                    services.AddSingleton<ICommandDefinitions, BasicCommandDefinitions>();
                    services.AddScoped<ICommandTranslator, BasicStringTranslator>();
                    services.AddScoped<CommandHandler, CommandHandler>();
                    services.AddSingleton<InputHandler, InputHandler>();
                    services.AddSingleton<App>();
                });
    }
}
