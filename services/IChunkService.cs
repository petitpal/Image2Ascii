using Img2Asc.entities;
using System.Drawing;

namespace Img2Asc.services
{
    public interface IChunkService
    {

        Chunk GetChunk(Bitmap source, int startX, int startY, int width, int height);
    }
}
