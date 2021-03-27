using Img2Asc.entities;
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

            var totalChunksWidth = chunkService.CalculateWidthInChunks(chunkWidth, source.Width);
            var totalChunksHeight = chunkService.CalculateWidthInChunks(chunkHeight, source.Height);

            var chunks = new Chunk[totalChunksHeight, totalChunksWidth];

            // get chunks from image
            for (var rowIndex = 0; rowIndex < totalChunksHeight; rowIndex++)
            {
                for (var colIndex = 0; colIndex < totalChunksWidth; colIndex++)
                {
                    chunks[rowIndex, colIndex] = chunkService.GetChunk(source, rowIndex, colIndex, chunkWidth, chunkHeight);
                }
            }

            // convert each chunk - compare to ascii image 'chunk' (cache these)


        }

    }
}
