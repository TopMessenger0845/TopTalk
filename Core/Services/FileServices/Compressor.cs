
namespace TopTalk.Core.Services.FileServices
{
    /// <summary>
    /// Базовый класс отвечающий за сжатие/разжатие массива байтов
    /// </summary>
    public abstract class Compressor
    {
        public abstract byte[] Compress(byte[] originalData);
        public abstract byte[] Decompress(byte[] compressedData);
    }
}
