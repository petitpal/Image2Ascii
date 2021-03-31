using Image2Ascii.Services;
using NUnit.Framework;

namespace Image2Ascii.Test
{
    public class CharacterServiceTests
    {
        private ICharacterService _characterService;

        [SetUp]
        public void Setup()
        {
            _characterService = new CharacterService();
        }

        [Test]
        public void ReturnsSomething()
        {
            // arrange

            // act
            var chars = _characterService.GetCharacters();

            // assert
            Assert.IsNotNull(chars);
            Assert.Greater(chars.Length, 0);
        }
    }
}