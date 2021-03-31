using Img2Asc.Entities;
using System;
using System.Drawing;

namespace Img2Asc.Services
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


        public Chunk[,] GetChunks(
            Bitmap source,
            int chunkWidth,
            int chunkHeight,
            Color defaultBackgroundColour)
        {
            var totalChunksWidth = CalculateWidthInChunks(chunkWidth, source.Width);
            var totalChunksHeight = CalculateHeightInChunks(chunkHeight, source.Height);
            var chunks = new Chunk[totalChunksHeight, totalChunksWidth];

            // vertical
            for (var chunkRowIndex = 0; chunkRowIndex < totalChunksHeight; chunkRowIndex++)
            {
                // hoiztonal
                for (var chunkColIndex = 0; chunkColIndex < totalChunksWidth; chunkColIndex++)
                {
                    chunks[chunkRowIndex, chunkColIndex] = GetChunk(source,
                                                                    chunkRowIndex,
                                                                    chunkColIndex,
                                                                    chunkWidth,
                                                                    chunkHeight,
                                                                    defaultBackgroundColour);
                }
            }

            return chunks;
        }

        public Chunk GetChunk(
            Bitmap source,
            int chunkRowIndex,
            int chunkColIndex,
            int chunkWidth,
            int chunkHeight,
            Color defaultBackgroundColour)
        {
            var chunk = new Color[chunkHeight, chunkWidth];

            var offsetStartY = chunkRowIndex * chunkWidth;
            var offsetStartX = chunkColIndex * chunkHeight;

            // vertical
            for (var rowIndex = 0; rowIndex < chunkHeight; rowIndex++)
            {
                // horizontal
                for (var colIndex = 0; colIndex < chunkWidth; colIndex++)
                {
                    var pixelX = offsetStartX + colIndex;
                    var pixelY = offsetStartY + rowIndex;
                    if (pixelX < source.Width && pixelY < source.Height)
                    {
                        var sourceColor = source.GetPixel(pixelX, pixelY);
                        var greyscale = _colorConvertor.ToGreyscale(sourceColor);
                        chunk[rowIndex, colIndex] = greyscale;
                    }
                    else
                    {
                        chunk[rowIndex, colIndex] = defaultBackgroundColour;
                    }
                }
            }

            return new Chunk() { Data = chunk };
        }
    }
}
