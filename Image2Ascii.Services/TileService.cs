using Image2Ascii.Entities;
using System;
using System.Drawing;

namespace Image2Ascii.Services
{
    public class TileService : ITileService
    {
        private readonly IGreyscaleConvertor _colorConvertor;

        public TileService(IGreyscaleConvertor colorConvertor)
        {
            _colorConvertor = colorConvertor;
        }

        public int CalculateHeightInTiles(int tileHeight, int imageHeight) =>
            (int)Math.Ceiling((decimal)imageHeight / tileHeight);

        public int CalculateWidthInTiles(int tileWidth, int imageWidth) =>
            (int)Math.Ceiling((decimal)imageWidth / tileWidth);


        public Tile[,] GetTiles(Bitmap source, int tileWidth, int tileHeight, Color defaultBackgroundColour)
        {
            var totalTilesWidth = CalculateWidthInTiles(tileWidth, source.Width);
            var totalTilesHeight = CalculateHeightInTiles(tileHeight, source.Height);
            var tiles = new Tile[totalTilesHeight, totalTilesWidth];

            // vertical
            for (var tileRowIndex = 0; tileRowIndex < totalTilesHeight; tileRowIndex++)
            {
                // hoiztonal
                for (var tileColIndex = 0; tileColIndex < totalTilesWidth; tileColIndex++)
                {
                    tiles[tileRowIndex, tileColIndex] = GetTile(source,
                                                                tileRowIndex,
                                                                tileColIndex,
                                                                tileWidth,
                                                                tileHeight,
                                                                defaultBackgroundColour);
                }
            }

            return tiles;
        }

        public Tile[] GetTiles(char[] source, int tileWidth, int tileHeight, Color foregroundColour, Color backgroundColour)
        {
            var tiles = new Tile[source.Length];
            var backgroundBrush = new SolidBrush(backgroundColour);

            var fontAdjustment = 0.4;
            var fontOffset = -0.2;
            var fontSize = (float)(tileHeight + tileHeight * fontAdjustment);
            var fontPaintOffset = (float)(tileHeight * fontOffset);

            var foregroundBrush = new SolidBrush(foregroundColour);
            var tileFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            var tileIndex = 0;

            foreach (var character in source)
            {
                using (var tileBitmap = new Bitmap(tileWidth, tileHeight))
                {
                    using (var graph = Graphics.FromImage(tileBitmap))
                    {
                        graph.FillRectangle(backgroundBrush, 0, 0, tileWidth, tileHeight);
                        graph.DrawString(
                            character.ToString(),
                            tileFont,
                            foregroundBrush,
                            fontPaintOffset,
                            fontPaintOffset,
                            StringFormat.GenericDefault);
                        graph.Save();
                    }
                    tileBitmap.Save($"c:\\temp\\tile{tileIndex}.bmp");

                    tiles[tileIndex] = GetTile(tileBitmap, 0, 0, tileWidth, tileHeight, backgroundColour);
                }
                tileIndex++;
            }
            return tiles;
        }


        public Tile GetTile(
            Bitmap source,
            int tileRowIndex,
            int tileColIndex,
            int tileWidth,
            int tileHeight,
            Color defaultBackgroundColour)
        {
            var tile = new Color[tileHeight, tileWidth];

            var offsetStartY = tileRowIndex * tileWidth;
            var offsetStartX = tileColIndex * tileHeight;

            // vertical
            for (var rowIndex = 0; rowIndex < tileHeight; rowIndex++)
            {
                // horizontal
                for (var colIndex = 0; colIndex < tileWidth; colIndex++)
                {
                    var pixelX = offsetStartX + colIndex;
                    var pixelY = offsetStartY + rowIndex;
                    if (pixelX < source.Width && pixelY < source.Height)
                    {
                        var sourceColor = source.GetPixel(pixelX, pixelY);
                        var greyscale = _colorConvertor.ToGreyscale(sourceColor);
                        tile[rowIndex, colIndex] = greyscale;
                    }
                    else
                    {
                        tile[rowIndex, colIndex] = defaultBackgroundColour;
                    }
                }
            }

            return new Tile() { Data = tile };
        }
    }
}
