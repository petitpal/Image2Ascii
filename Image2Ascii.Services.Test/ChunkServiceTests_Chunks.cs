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
        public void GetChunks_CorrectPortions()
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
            Assert.AreEqual(2, chunks.GetLength(0));
            Assert.AreEqual(2, chunks.GetLength(1));


            var chunk1 = chunks[0, 0];
            var greyScaleWhite = Color.FromArgb(255, 55, 183, 19);
            Assert.AreEqual(greyScaleWhite, chunk1.Data[0, 0]);
            Assert.AreEqual(greyScaleWhite, chunk1.Data[0, 1]);
            Assert.AreEqual(greyScaleWhite, chunk1.Data[1, 0]);
            Assert.AreEqual(greyScaleWhite, chunk1.Data[1, 1]);


            Chunk chunk2 = chunks[0, 1];
            var greyScaleBlack = Color.FromArgb(255, 0, 0, 0);
            Assert.AreEqual(greyScaleBlack, chunk2.Data[0, 0]);
            Assert.AreEqual(greyScaleBlack, chunk2.Data[0, 1]);
            Assert.AreEqual(greyScaleBlack, chunk2.Data[1, 0]);
            Assert.AreEqual(greyScaleBlack, chunk2.Data[1, 1]);


            Chunk chunk3 = chunks[1, 0];
            var greyScaleRed = Color.FromArgb(255, 55, 0, 0);
            Assert.AreEqual(greyScaleRed, chunk3.Data[0, 0]);
            Assert.AreEqual(greyScaleRed, chunk3.Data[0, 1]);
            Assert.AreEqual(greyScaleRed, chunk3.Data[1, 0]);
            Assert.AreEqual(greyScaleRed, chunk3.Data[1, 1]);


            Chunk chunk4 = chunks[1, 1];
            var greyScaleYellow = Color.FromArgb(255, 55, 183, 0);
            Assert.AreEqual(greyScaleYellow, chunk4.Data[0, 0]);
            Assert.AreEqual(greyScaleYellow, chunk4.Data[0, 1]);
            Assert.AreEqual(greyScaleYellow, chunk4.Data[1, 0]);
            Assert.AreEqual(greyScaleYellow, chunk4.Data[1, 1]);
        }

    }
}