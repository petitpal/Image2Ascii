using Img2Asc.Entities;
using System.Drawing;

namespace Img2Asc.Services
{
    public interface IChunkService
    {
        int CalculateWidthInChunks(int chunkWidth, int imageWidth);
        int CalculateHeightInChunks(int chunkHeight, int imageHeight);
        Chunk[,] GetChunks(Bitmap source, int chunkWidth, int chunkHeight);
        Chunk GetChunk(Bitmap source, int startX, int startY, int width, int height);
    }
}
