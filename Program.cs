using System;
using System.Drawing;

namespace Img2Asc
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = @"C:\git\petitpal\Img2Asc\samples\coleman.jpg";
            using var source = new Bitmap(file);
            
            var chunkWidth = 4;
            var chunkHeight = 3;

            var totalChunksWidth = (int)source.Width / chunkWidth;
            var totalChunksHeight = (int)source.Height / chunkHeight;

            var chunks = new Chunk[totalChunksHeight, totalChunksWidth];

            for (var rowIndex = 0; rowIndex < totalChunksHeight; rowIndex++)
            {
                for (var colIndex = 0; colIndex < totalChunksWidth; colIndex++)
                {
                    chunks[rowIndex, colIndex] = GetChunk(source, rowIndex, colIndex, chunkWidth, chunkHeight);
                }
            }


            Console.WriteLine("Hello World!");
        }

        static Chunk GetChunk(Bitmap source, int startX, int startY, int width, int height)
        {
            var chunk = new Color[height, width];

            /*
             *  p p p
             *  p p p
             *  p p p
             *  p p p
             */

            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    chunk[rowIndex, colIndex] = source.GetPixel(rowIndex, colIndex);
                }
            }

            return new Chunk() { Data = chunk };
        }

        public class Chunk
        {
            public Color[,] Data { get; set; }
        }
    }
}
