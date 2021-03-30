using Img2Asc.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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
            // chunk width / image width / expected chunks
            yield return new TestCaseData(20, 200, 10).SetName("{m}(10/200)");
            yield return new TestCaseData(20, 400, 20).SetName("{m}(20/400)");
            yield return new TestCaseData(20, 399, 20).SetName("{m}(20/399)");
            yield return new TestCaseData(1, 100, 100).SetName("{m}(1/100)");
            yield return new TestCaseData(49, 100, 3).SetName("{m}(49/100)");
            yield return new TestCaseData(32, 1024, 32).SetName("{m}(32/1024)");
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

    }
}