using Image2Ascii.Applicaton;
using Image2Ascii.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Image2Ascii
{
    class Program
    {
        /// <param name="sourceFile">Source file to be converted</param>
        /// <param name="tileWidth">Width of chunks</param>
        /// <param name="tileHeight">Height of chunks</param>
        static void Main(FileInfo sourceFile, int tileWidth, int tileHeight)
        {
            if (sourceFile == null || tileWidth < 0 | tileHeight < 0) return;

            using var host = CreateHostBuilder().Build();
            using var serviceScope = host.Services.CreateScope();

            var app = serviceScope.ServiceProvider.GetService<IApp>();
            app.Run(sourceFile.FullName, tileWidth, tileHeight);
        }


        static IHostBuilder CreateHostBuilder()
        {
            var host = Host.CreateDefaultBuilder();

            // default
            host.ConfigureServices((_, services) =>
                    services.AddSingleton<IApp, App>()
                            .AddTransient<ICharacterService, CharacterService>()
                            .AddTransient<ITileService, TileService>()
                            .AddTransient<IGreyscaleConvertor, GreyscaleConvertor>()
            );

            return host;
        }

    }
}
