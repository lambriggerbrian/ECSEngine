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
                    services.AddSingleton<World, World>();
                    services.AddSingleton<InputHandler, InputHandler>();
                    services.AddSingleton<IEntityManager, EntityManager>();
                    services.AddSingleton<App>();
                });
    }
}
