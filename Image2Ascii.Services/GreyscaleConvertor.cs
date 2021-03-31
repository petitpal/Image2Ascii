using System;
using System.Drawing;

namespace Image2Ascii.Services
{
    public class GreyscaleConvertor : IGreyscaleConvertor
    {
        public Color ToGreyscale(Color source)
        {
            var c = Color.FromArgb(
                source.A,
                (int)Math.Ceiling(source.R * 0.2125),
                (int)Math.Ceiling(source.G * 0.7154),
                (int)Math.Ceiling(source.B * 0.0721));
            return c;
        }
    }
}
