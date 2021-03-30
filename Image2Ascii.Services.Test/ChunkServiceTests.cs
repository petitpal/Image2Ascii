using Img2Asc.services;
using NUnit.Framework;

namespace Image2Ascii.Test
{
    public class ChunkServiceTests
    {
        private IGreyscaleConvertor _greyScaleConvertor;

        [SetUp]
        public void Setup()
        {
            _greyScaleConvertor = new GreyscaleConvertor();            
        }

        [Test]
        public void ChunkHeightCalculation_Simple()
        {
            // arrange
            var chunkService = new ChunkService(_greyScaleConvertor);

            // act
            var verticalChunks = chunkService.CalculateHeightInChunks(20, 200);

            // assert
            Assert.AreEqual(10, verticalChunks);
        }

        [Test]
        public void ChunkWidthCalculation_Simple()
        {
            // arrange
            var chunkService = new ChunkService(_greyScaleConvertor);

            // act
            var verticalChunks = chunkService.CalculateWidthInChunks(20, 200);

            // assert
            Assert.AreEqual(10, verticalChunks);
        }
    }
}