using Image2Ascii.Entities;
using Image2Ascii.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Image2Ascii.Test
{
    public class TileServiceTests_Chunks
    {
        private IGreyscaleConvertor _greyScaleConvertor;
        private ITileService _tileService;
        private ICharacterService _characterService;

        [SetUp]
        public void Setup()
        {
            _greyScaleConvertor = new GreyscaleConvertor();
            _tileService = new TileService(_greyScaleConvertor);
            _characterService = new CharacterService();
        }

        private static IEnumerable<TestCaseData> GetTile_Counts_TestDate()
        {
            // bitmap width / bitmap height / tile width / tile height / expected tiles wide / expected tiles high
            yield return new TestCaseData(100, 100, 10, 10, 10, 10).SetName("{m} (100/100/10/10)");
            yield return new TestCaseData(100, 200, 10, 10, 10, 20).SetName("{m} (100/200/10/10)");
            yield return new TestCaseData(200, 100, 10, 10, 20, 10).SetName("{m} (200/100/10/10)");
            yield return new TestCaseData(1024, 768, 4, 3, 256, 256).SetName("{m} (1024/768/4/3)");
            yield return new TestCaseData(1008, 601, 11, 2, 92, 301).SetName("{m} (1008/601/11/2)");
        }


        [TestCaseSource(nameof(GetTile_Counts_TestDate))]
        public void GetTile_Counts(
            int bitmapWidth,
            int bitmapHeight,
            int tileWidth,
            int tileHeight,
            int expectedTileWidth,
            int expectedTileHeight)
        {
            // arrange
            using var source = new Bitmap(bitmapWidth, bitmapHeight);
            var defaultBackground = Color.Transparent;

            // act
            var tiles = _tileService.GetTiles(source, tileWidth, tileHeight, defaultBackground);

            // assert
            Assert.AreEqual(expectedTileHeight, tiles.GetLength(0));
            Assert.AreEqual(expectedTileWidth, tiles.GetLength(1));
        }

        [Test]
        public void GetTiles_CorrectPortions_2x4x4()
        {
            // arrange
            var tileSize = 2;
            var defaultBackground = Color.Transparent;

            using var source = new Bitmap(4, 4);
            using var graph = Graphics.FromImage(source);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 0, 2, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Yellow), 2, 2, tileSize, tileSize);
            graph.Save();
            // act
            var tiles = _tileService.GetTiles(source, tileSize, tileSize, defaultBackground);

            // assert
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);
            var greyScaleYellow = Color.FromArgb(255, 55, 183, 0);

            Assert.AreEqual(2, tiles.GetLength(0));
            Assert.AreEqual(2, tiles.GetLength(1));
            Assert.IsTrue(CheckTileColour(tiles[0, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckTileColour(tiles[0, 1], greyScaleRed), "red");
            Assert.IsTrue(CheckTileColour(tiles[1, 0], greyScaleBlack), "black");
            Assert.IsTrue(CheckTileColour(tiles[1, 1], greyScaleYellow), "yellow");
        }

        private bool CheckTileColour(Tile tile, Color expectedColor)
        {
            return (bool)(tile.Data[0, 0] == expectedColor
                          && tile.Data[0, 1] == expectedColor
                          && tile.Data[1, 0] == expectedColor
                          && tile.Data[1, 1] == expectedColor);
        }

        [Test]
        public void GetTiles_CorrectPortions_2x6x4()
        {
            // arrange
            var tileSize = 2;
            var defaultBackground = Color.Transparent;

            using var source = new Bitmap(6, 4);
            using var graph = Graphics.FromImage(source);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 0, 2, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Yellow), 2, 2, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.White), 4, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 4, 2, tileSize, tileSize);
            graph.Save();

            // act
            var tiles = _tileService.GetTiles(source, tileSize, tileSize, defaultBackground);

            // assert
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);
            var greyScaleYellow = Color.FromArgb(255, 55, 183, 0);

            Assert.AreEqual(2, tiles.GetLength(0));
            Assert.AreEqual(3, tiles.GetLength(1));
            Assert.IsTrue(CheckTileColour(tiles[0, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckTileColour(tiles[0, 1], greyScaleRed), "red");
            Assert.IsTrue(CheckTileColour(tiles[0, 2], greyScaleWhite), "white");
            Assert.IsTrue(CheckTileColour(tiles[1, 0], greyScaleBlack), "black");
            Assert.IsTrue(CheckTileColour(tiles[1, 1], greyScaleYellow), "yellow");
            Assert.IsTrue(CheckTileColour(tiles[1, 2], greyScaleRed), "red");
        }


        [Test]
        public void GetTiles_CorrectPortions_2x4x6()
        {
            // arrange
            var tileSize = 2;
            var defaultBackground = Color.Transparent;

            using var source = new Bitmap(4, 6);
            using var graph = Graphics.FromImage(source);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 0, 2, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Yellow), 2, 2, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 4, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 4, tileSize, tileSize);
            graph.Save();

            // act
            var tiles = _tileService.GetTiles(source, tileSize, tileSize, defaultBackground);

            // assert
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);
            var greyScaleYellow = Color.FromArgb(255, 55, 183, 0);

            Assert.AreEqual(3, tiles.GetLength(0));
            Assert.AreEqual(2, tiles.GetLength(1));
            Assert.IsTrue(CheckTileColour(tiles[0, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckTileColour(tiles[0, 1], greyScaleRed), "red");
            Assert.IsTrue(CheckTileColour(tiles[1, 0], greyScaleBlack), "black");
            Assert.IsTrue(CheckTileColour(tiles[1, 1], greyScaleYellow), "yellow");
            Assert.IsTrue(CheckTileColour(tiles[2, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckTileColour(tiles[2, 1], greyScaleRed), "red");
        }


        [Test]
        public void GetTiles_CorrectPortions_3x9x9()
        {
            // arrange
            var tileSize = 3;
            var defaultBackground = Color.Transparent;

            using var source = new Bitmap(9, 9);
            using var graph = Graphics.FromImage(source);

            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 3, 0, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 6, 0, tileSize, tileSize);

            graph.FillRectangle(new SolidBrush(Color.Black), 0, 3, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.White), 3, 3, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 6, 3, tileSize, tileSize);

            graph.FillRectangle(new SolidBrush(Color.Red), 0, 6, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 3, 6, tileSize, tileSize);
            graph.FillRectangle(new SolidBrush(Color.White), 6, 6, tileSize, tileSize);

            graph.Save();

            // act
            var tiles = _tileService.GetTiles(source, tileSize, tileSize, defaultBackground);

            // assert
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);

            Assert.AreEqual(3, tiles.GetLength(0));
            Assert.AreEqual(3, tiles.GetLength(1));

            Assert.IsTrue(CheckTileColour(tiles[0, 0], greyScaleWhite), "white 1");
            Assert.IsTrue(CheckTileColour(tiles[0, 1], greyScaleRed), "red 1");
            Assert.IsTrue(CheckTileColour(tiles[0, 2], greyScaleBlack), "black 1");

            Assert.IsTrue(CheckTileColour(tiles[1, 0], greyScaleBlack), "black 2");
            Assert.IsTrue(CheckTileColour(tiles[1, 1], greyScaleWhite), "white 2");
            Assert.IsTrue(CheckTileColour(tiles[1, 2], greyScaleRed), "red 2");

            Assert.IsTrue(CheckTileColour(tiles[2, 0], greyScaleRed), "red 3");
            Assert.IsTrue(CheckTileColour(tiles[2, 1], greyScaleBlack), "black 3");
            Assert.IsTrue(CheckTileColour(tiles[2, 2], greyScaleWhite), "white 3");

        }


        [Test]
        public void GetTiles_CharactersFixed()
        {
            // arrange
            var source = new char[] { 'a', 'A', 'b', 'B', 'W', '!' };
            var tileHeight = 50;
            var tileWidth = _tileService.CalculateTileWidth(tileHeight);
            var background = Color.Black;
            var foreground = Color.White;

            // act
            var tiles = _tileService.GetTiles(source, tileWidth, tileHeight, foreground, background);

            // assert
            Assert.IsNotNull(tiles);
        }


        [Test]
        public void GetTiles_CharactersFromService()
        {
            // arrange
            var source = _characterService.GetCharacters();
            var tileHeight = 20;
            var tileWidth = _tileService.CalculateTileWidth(tileHeight);
            var background = Color.Transparent;
            var foreground = Color.White;

            // act
            var tiles = _tileService.GetTiles(source, tileWidth, tileHeight, foreground, background);

            // assert
            Assert.IsNotNull(tiles);
        }

    }
}