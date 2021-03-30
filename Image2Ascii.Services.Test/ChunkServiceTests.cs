using Img2Asc.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Image2Ascii.Test
{
    public class ChunkServiceTests
    {
        private Mock<IGreyscaleConvertor> _greyScaleConvertor;
        private IChunkService _chunkService;

        [SetUp]
        public void Setup()
        {
            _greyScaleConvertor = new Mock<IGreyscaleConvertor>();
            _chunkService = new ChunkService(_greyScaleConvertor.Object);
        }


        private static IEnumerable<TestCaseData> ChunkCalculation_TestData()
        {
            // chunk dimension / image width / expected chunks
            yield return new TestCaseData(20, 200, 10).SetName("{m} (10/200)");
            yield return new TestCaseData(20, 400, 20).SetName("{m} (20/400)");
            yield return new TestCaseData(20, 399, 20).SetName("{m} (20/399)");
            yield return new TestCaseData(1, 100, 100).SetName("{m} (1/100)");
            yield return new TestCaseData(49, 100, 3).SetName("{m} (49/100)");
            yield return new TestCaseData(32, 1024, 32).SetName("{m} (32/1024)");
        }


        [TestCaseSource(nameof(ChunkCalculation_TestData))]
        public void ChunkCalculation_Width(int chunkWidth, int sourceWidth, int expectedChunks)
        {
            // arrange

            // act
            var chunkCount = _chunkService.CalculateWidthInChunks(chunkWidth, sourceWidth);

            // assert
            Assert.AreEqual(expectedChunks, chunkCount);
        }


        [TestCaseSource(nameof(ChunkCalculation_TestData))]
        public void ChunkCalculation_Height(int chunkHeight, int sourceHeight, int expectedChunks)
        {
            // arrange

            // act
            var chunkCount = _chunkService.CalculateWidthInChunks(chunkHeight, sourceHeight);

            // assert
            Assert.AreEqual(expectedChunks, chunkCount);
        }



        private static IEnumerable<TestCaseData> GetChunks_Basic_TestDate()
        {
            // bitmap width / bitmap height / chunk width / chunk height / expected chunks wide / expected chunks high
            yield return new TestCaseData(100, 100, 10, 10, 10, 10).SetName("{m} (100/100/10/10/10/10)");
            yield return new TestCaseData(100, 200, 10, 10, 10, 20).SetName("{m} (100/200/10/10/10/20)");
        }


        [TestCaseSource(nameof(GetChunks_Basic_TestDate))]
        public void GetChunks_Basic(
            int bitmapWidth,
            int bitmapHeight,
            int chunkWidth,
            int chunkHeight,
            int expectedChunkWidth,
            int expectedChunkHeight)
        {
            // arrange
            var source = new Bitmap(bitmapWidth, bitmapHeight);

            // act
            var chunks = _chunkService.GetChunks(source, chunkWidth, chunkHeight);

            // assert
            Assert.AreEqual(expectedChunkWidth, chunks.GetLength(1));
            Assert.AreEqual(expectedChunkHeight, chunks.GetLength(0));
        }

    }
}