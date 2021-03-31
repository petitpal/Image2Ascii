using Image2Ascii.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Image2Ascii.Test
{
    public class TileServiceTests_GreyScale
    {
        private IGreyscaleConvertor _greyScaleConvertor;

        [SetUp]
        public void Setup()
        {
            _greyScaleConvertor = new GreyscaleConvertor();
        }

        private static IEnumerable<TestCaseData> ColorData()
        {
            // bitmap width / bitmap height / chunk width / chunk height / expected chunks wide / expected chunks high
            yield return new TestCaseData(Color.White, Color.FromArgb(255, 55, 183, 19)).SetName("{m} (White)");
            yield return new TestCaseData(Color.Red, Color.FromArgb(255, 55, 0, 0)).SetName("{m} (Red)");
            yield return new TestCaseData(Color.Yellow, Color.FromArgb(255, 55, 183, 0)).SetName("{m} (Yellow)");
            yield return new TestCaseData(Color.Black, Color.FromArgb(255, 0, 0, 0)).SetName("{m} (Black)");
        }

        [TestCaseSource(nameof(ColorData))]
        public void ConvertColors(Color original, Color expectedGrey)
        {
            // arrange

            // act
            var grey = _greyScaleConvertor.ToGreyscale(original);

            // assert
            Assert.AreEqual(expectedGrey, grey);
        }
    }
}