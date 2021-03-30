using System.Drawing;

namespace Img2Asc.Services
{
    public interface IGreyscaleConvertor
    {
        Color ToGreyscale(Color source);
    }
}
