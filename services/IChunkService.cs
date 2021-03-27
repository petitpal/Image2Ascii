using Img2Asc.entities;
using System.Drawing;

namespace Img2Asc.services
{
    public interface IChunkService
    {
        int CalculateWidthInChunks(int chunkWidth, int imageWidth);
        int CalculateHeightInChunks(int chunkHeight, int imageHeight);
        Chunk GetChunk(Bitmap source, int startX, int startY, int width, int height);
    }
}
