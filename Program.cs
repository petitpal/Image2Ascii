using Img2Asc.services;
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


            IGreyscaleConvertor colorConvertor = new GreyscaleConvertor();
            IChunkService chunkService = new ChunkService(colorConvertor);

            using var source = new Bitmap(file);


            // get chunks from image
            var chunks = chunkService.GetChunks(source, chunkWidth, chunkHeight);

            
            // convert each chunk - compare to ascii image 'chunk' (cache these)


        }

    }
}
