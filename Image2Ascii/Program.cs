using Img2Asc.Applicaton;
using Img2Asc.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;
using System.IO;

namespace Img2Asc
{
    class Program
    {
        /// <param name="sourceFile">Source file to be converted</param>
        /// <param name="chunkWidth">Width of chunks</param>
        /// <param name="chunkHeight">Height of chunks</param>
        static void Main(FileInfo sourceFile, int chunkWidth, int chunkHeight)
        {
            if (sourceFile == null || chunkWidth < 0 | chunkHeight < 0) return;

            using var host = CreateHostBuilder().Build();
            using var serviceScope = host.Services.CreateScope();

            var app = serviceScope.ServiceProvider.GetService<IApp>();
            app.Run(sourceFile.FullName, chunkWidth, chunkHeight);

        }


        static IHostBuilder CreateHostBuilder()
        {
            var host = Host.CreateDefaultBuilder();

            // default
            host.ConfigureServices((_, services) =>
                    services.AddSingleton<IApp, App>()
                            .AddTransient<IChunkService, ChunkService>()
                            .AddTransient<IGreyscaleConvertor, GreyscaleConvertor>()
            );

            return host;
        }

    }
}
