using Img2Asc.entities;
using System;
using System.Drawing;

namespace Img2Asc.services
{
    public class ChunkService : IChunkService
    {
        private readonly IGreyscaleConvertor _colorConvertor;

        public ChunkService(IGreyscaleConvertor colorConvertor)
        {
            _colorConvertor = colorConvertor;
        }

        public int CalculateHeightInChunks(int chunkHeight, int imageHeight) =>
            (int)Math.Ceiling((decimal)imageHeight / chunkHeight);

        public int CalculateWidthInChunks(int chunkWidth, int imageWidth) =>
            (int)Math.Ceiling((decimal)imageWidth / chunkWidth);


        public Chunk[,] GetChunks(Bitmap source, int chunkWidth, int chunkHeight)
        {
            var totalChunksWidth = CalculateWidthInChunks(chunkWidth, source.Width);
            var totalChunksHeight = CalculateHeightInChunks(chunkHeight, source.Height);
            var chunks = new Chunk[totalChunksHeight, totalChunksWidth];

            for (var rowIndex = 0; rowIndex < totalChunksHeight; rowIndex++)
            {
                for (var colIndex = 0; colIndex < totalChunksWidth; colIndex++)
                {
                    chunks[rowIndex, colIndex] = GetChunk(source, rowIndex, colIndex, chunkWidth, chunkHeight);
                }
            }

            return chunks;
        }

        public Chunk GetChunk(Bitmap source, int startX, int startY, int width, int height)
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
                    var sourceColor = source.GetPixel(rowIndex, colIndex);
                    var greyscale = _colorConvertor.ToGreyscale(sourceColor);
                    chunk[rowIndex, colIndex] = greyscale;
                }
            }

            return new Chunk() { Data = chunk };
        }
    }
}
