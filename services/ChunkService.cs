using Img2Asc.entities;
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
