
namespace TopTalk.Core.Services.FileServices
{
    /// <summary>
    /// класс для сжатия/разжатие файлов реализующий базовый класс Compressor
    /// </summary>
    public class FileDataCompressorService : Compressor
    {
        public override byte[] Compress(byte[] originaData)
        {
            byte[] compressedData = originaData;
            return compressedData;
        }
        public override byte[] Decompress(byte[] compressedData)
        {
            byte[] decompressedData = compressedData;
            return decompressedData;
        }
    }
}
