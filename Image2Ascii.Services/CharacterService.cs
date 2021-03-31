using System.Text;

namespace Image2Ascii.Services
{
    public class CharacterService : ICharacterService
    {
        private char[] printableChars;

        public char[] GetCharacters()
        {
            if (printableChars == null)
            {

                const byte firstPrintableChar = 32;
                const byte lastPrintableChar = 126;

                byte[] charBytes = new byte[lastPrintableChar - firstPrintableChar];

                for (var chr = firstPrintableChar; chr < lastPrintableChar; chr++)
                {
                    charBytes[chr - firstPrintableChar] = chr;
                }
                
                printableChars = ASCIIEncoding.ASCII.GetChars(charBytes);
            }

            return printableChars;
        }
    }
}
