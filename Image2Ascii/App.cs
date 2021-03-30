using Img2Asc.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Img2Asc
{
    public class App : IApp
    {
        private readonly IChunkService _chunkService;

        public App(IChunkService chunkService)
        {
            _chunkService = chunkService;
        }

        public void Run(string sourceFile, int chunkWidth, int chunkHeight)
        {
            var defaultBackground = Color.Transparent;
            using var source = new Bitmap(sourceFile);

            // get chunks from image
            var chunks = _chunkService.GetChunks(source, chunkWidth, chunkHeight, defaultBackground);


            // convert each chunk - compare to ascii image 'chunk' (cache these)

        }
    }

    public interface IApp
    {
        void Run(string sourceFile, int chunkWidth, int chunkHeight);
    }
}
