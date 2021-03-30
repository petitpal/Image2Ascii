using System.Drawing;

namespace Img2Asc.services
{
    public interface IGreyscaleConvertor
    {
        Color ToGreyscale(Color source);
    }
}
