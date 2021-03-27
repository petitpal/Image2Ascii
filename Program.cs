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
            var file = @"C:\Users\Paul\Source\Repos\petitpal\Image2Ascii\samples\coleman.jpg";
            using var source = new Bitmap(file);


            IGreyscaleConvertor colorConvertor = new GreyscaleConvertor();
            IChunkService chunkService = new ChunkService(colorConvertor);


            var chunkWidth = 4;
            var chunkHeight = 3;

            var totalChunksWidth = (int)source.Width / chunkWidth;
            var totalChunksHeight = (int)source.Height / chunkHeight;

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


            Console.WriteLine("Hello World!");
        }

    }
}
