namespace Img2Asc.Applicaton
{
    public interface IApp
    {
        void Run(string sourceFile, int chunkWidth, int chunkHeight);
    }
}
