using System.Drawing;

namespace Image2Ascii.Services
{
    public interface IGreyscaleConvertor
    {
        Color ToGreyscale(Color source);
    }
}
