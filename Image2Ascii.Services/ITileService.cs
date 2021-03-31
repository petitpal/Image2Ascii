using Image2Ascii.Entities;
using System.Drawing;

namespace Image2Ascii.Services
{
    public interface ITileService
    {
        int CalculateWidthInTiles(int tileWidth, int imageWidth);
        int CalculateHeightInTiles(int tileHeight, int imageHeight);
        Tile[,] GetTiles(Bitmap source, int tileWidth, int tileHeight, Color defaultBackgroundColour);
        Tile[] GetTiles(char[] source, int tileWidth, int tileHeight, Color foregroundColour, Color backgroundColour);
        Tile GetTile(Bitmap source, int tileRowIndex, int tileColIndex, int tileWidth, int vHeight, Color defaultBackgroundColour);

    }
}
