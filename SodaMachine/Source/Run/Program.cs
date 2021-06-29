using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SodaMachine.Run
{
    /// <summary>
    /// Bootloader to start the machine.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Instantiates the core domain classes and injects them into the sodamachine before starting it.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            var sodaMachine = ActivatorUtilities.CreateInstance<DomainModel.SodaMachine>(GetDiContainer().Services);
            sodaMachine.Start();
        }

        private static IHost GetDiContainer()
        {
            var builder = new ConfigurationBuilder();

            // Initiated the denpendency injection container 
            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            services.Scan(scan =>
                                scan.FromCallingAssembly()
                                .AddClasses()
                                .AsSelf()
                                .WithSingletonLifetime()
                                .AddClasses()
                                .AsImplementedInterfaces()
                                .WithSingletonLifetime()
                            );
                        })
                        .Build();
            return host;
        }
    }
}
