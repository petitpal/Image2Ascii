using Img2Asc.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;

namespace Img2Asc
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args[0];
            var chunkWidth = Convert.ToInt32(args[1]);
            var chunkHeight = Convert.ToInt32(args[2]);


            using var host = CreateHostBuilder(args).Build();
            using var serviceScope = host.Services.CreateScope();

            var chunkService = serviceScope.ServiceProvider.GetService<IChunkService>();

            var defaultBackground = Color.Transparent;
            using var source = new Bitmap(file);

            // get chunks from image
            var chunks = chunkService.GetChunks(source, chunkWidth, chunkHeight, defaultBackground);

            
            // convert each chunk - compare to ascii image 'chunk' (cache these)


        }


        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);

            // default
            host.ConfigureServices((_, services) =>
                    services.AddTransient<IChunkService, ChunkService>()
                            .AddTransient<IGreyscaleConvertor, GreyscaleConvertor>()
            );
            
            return host;
        }

    }
}
