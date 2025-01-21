
namespace TopTalk.Services
{
    public class FileCompressorService : BaseFileCompressor
    {
        public override byte[] CompressFile(byte[] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("Данные для сжатия не могут быть пустыми", nameof(data));
            return data;
        }
        public override byte[] DecompressFile(byte[] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("Данные для сжатия не могут быть пустыми.", nameof(data));
            return data;
        }
    }
}
