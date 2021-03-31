using Img2Asc.Entities;
using Img2Asc.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Image2Ascii.Test
{
    public class ChunkServiceTests_Chunks
    {
        //private Mock<IGreyscaleConvertor> _greyScaleConvertor;
        private IGreyscaleConvertor _greyScaleConvertor;
        private IChunkService _chunkService;

        [SetUp]
        public void Setup()
        {
            _greyScaleConvertor = new GreyscaleConvertor();
            //_chunkService = new ChunkService(_greyScaleConvertor.Object);
            _chunkService = new ChunkService(_greyScaleConvertor);
        }

        private static IEnumerable<TestCaseData> GetChunks_Counts_TestDate()
        {
            // bitmap width / bitmap height / chunk width / chunk height / expected chunks wide / expected chunks high
            yield return new TestCaseData(100, 100, 10, 10, 10, 10).SetName("{m} (100/100/10/10)");
            yield return new TestCaseData(100, 200, 10, 10, 10, 20).SetName("{m} (100/200/10/10)");
            yield return new TestCaseData(200, 100, 10, 10, 20, 10).SetName("{m} (200/100/10/10)");
            yield return new TestCaseData(1024, 768, 4, 3, 256, 256).SetName("{m} (1024/768/4/3)");
            yield return new TestCaseData(1008, 601, 11, 2, 92, 301).SetName("{m} (1008/601/11/2)");
        }


        [TestCaseSource(nameof(GetChunks_Counts_TestDate))]
        public void GetChunks_Counts(
            int bitmapWidth,
            int bitmapHeight,
            int chunkWidth,
            int chunkHeight,
            int expectedChunkWidth,
            int expectedChunkHeight)
        {
            // arrange
            using var source = new Bitmap(bitmapWidth, bitmapHeight);
            var defaultBackground = Color.Transparent;

            // act
            var chunks = _chunkService.GetChunks(source, chunkWidth, chunkHeight, defaultBackground);

            // assert
            Assert.AreEqual(expectedChunkHeight, chunks.GetLength(0));
            Assert.AreEqual(expectedChunkWidth, chunks.GetLength(1));
        }

        [Test]
        public void GetChunks_CorrectPortions_4x4()
        {
            // arrange
            var chunkSize = 2;
            var defaultBackground = Color.Transparent;

            using var source = new Bitmap(4, 4);
            using var graph = Graphics.FromImage(source);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 0, 2, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 0, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Yellow), 2, 2, chunkSize, chunkSize);
            graph.Save();
            // act
            var chunks = _chunkService.GetChunks(source, chunkSize, chunkSize, defaultBackground);

            // assert
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);
            var greyScaleYellow = Color.FromArgb(255, 55, 183, 0);

            Assert.AreEqual(2, chunks.GetLength(0));
            Assert.AreEqual(2, chunks.GetLength(1));
            Assert.IsTrue(CheckChunkColour(chunks[0, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckChunkColour(chunks[0, 1], greyScaleRed), "red");
            Assert.IsTrue(CheckChunkColour(chunks[1, 0], greyScaleBlack), "black");
            Assert.IsTrue(CheckChunkColour(chunks[1, 1], greyScaleYellow), "yellow");
        }

        private bool CheckChunkColour(Chunk chunk, Color expectedColor)
        {
            return (bool)(chunk.Data[0, 0] == expectedColor
                          && chunk.Data[0, 1] == expectedColor
                          && chunk.Data[1, 0] == expectedColor
                          && chunk.Data[1, 1] == expectedColor);
        }

        [Test]
        public void GetChunks_CorrectPortions_6x4()
        {
            // arrange
            var chunkSize = 2;
            var defaultBackground = Color.Transparent;

            using var source = new Bitmap(6, 4);
            using var graph = Graphics.FromImage(source);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 0, 2, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 0, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Yellow), 2, 2, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.White), 4, 0, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 4, 2, chunkSize, chunkSize);
            graph.Save();

            // act
            var chunks = _chunkService.GetChunks(source, chunkSize, chunkSize, defaultBackground);

            // assert
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);
            var greyScaleYellow = Color.FromArgb(255, 55, 183, 0);

            Assert.AreEqual(2, chunks.GetLength(0));
            Assert.AreEqual(3, chunks.GetLength(1));
            Assert.IsTrue(CheckChunkColour(chunks[0, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckChunkColour(chunks[0, 1], greyScaleRed), "red");
            Assert.IsTrue(CheckChunkColour(chunks[0, 2], greyScaleWhite), "white");
            Assert.IsTrue(CheckChunkColour(chunks[1, 0], greyScaleBlack), "black");
            Assert.IsTrue(CheckChunkColour(chunks[1, 1], greyScaleYellow), "yellow");
            Assert.IsTrue(CheckChunkColour(chunks[1, 2], greyScaleRed), "red");
        }


        [Test]
        public void GetChunks_CorrectPortions_4x6()
        {
            // arrange
            var chunkSize = 2;
            var defaultBackground = Color.Transparent;

            using var source = new Bitmap(4, 6);
            using var graph = Graphics.FromImage(source);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 0, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Black), 0, 2, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Yellow), 2, 2, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 4, chunkSize, chunkSize);
            graph.FillRectangle(new SolidBrush(Color.Red), 2, 4, chunkSize, chunkSize);
            graph.Save();

            // act
            var chunks = _chunkService.GetChunks(source, chunkSize, chunkSize, defaultBackground);

            // assert
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);
            var greyScaleYellow = Color.FromArgb(255, 55, 183, 0);

            Assert.AreEqual(3, chunks.GetLength(0));
            Assert.AreEqual(2, chunks.GetLength(1));
            Assert.IsTrue(CheckChunkColour(chunks[0, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckChunkColour(chunks[0, 1], greyScaleRed), "red");
            Assert.IsTrue(CheckChunkColour(chunks[1, 0], greyScaleBlack), "black");
            Assert.IsTrue(CheckChunkColour(chunks[1, 1], greyScaleYellow), "yellow");
            Assert.IsTrue(CheckChunkColour(chunks[2, 0], greyScaleWhite), "white");
            Assert.IsTrue(CheckChunkColour(chunks[2, 1], greyScaleRed), "red");
        }

    }
}