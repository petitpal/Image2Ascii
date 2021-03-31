using Image2Ascii.Services;
using System.Drawing;

namespace Image2Ascii.Applicaton
{
    public class App : IApp
    {
        private readonly ITileService _tileService;
        private readonly ICharacterService _characterService;

        public App(ITileService tileService, ICharacterService characterService)
        {
            _tileService = tileService;
            _characterService = characterService;
        }

        public void Run(string sourceFile, int tileWidth, int tileHeight)
        {
            var defaultBackground = Color.White;
            var defaultForeground = Color.Black;
            using var source = new Bitmap(sourceFile);

            // printable characters
            var chars = _characterService.GetCharacters();

            // get chunks from image
            var imageTiles = _tileService.GetTiles(source, tileWidth, tileHeight, defaultBackground);

            // get ascii tiles
            var asciiTiles = _tileService.GetTiles(chars, tileWidth, tileHeight, defaultForeground, defaultBackground);

            // convert each tile - compare to ascii image 'tiles' (cache these)

        }
    }
}
