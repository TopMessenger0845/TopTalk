namespace TopTalk.Core.Services
{
    public abstract class BaseFileCompressor
    {
        public abstract byte[] CompressFile(byte[] fileData);
        public abstract byte[] DecompressFile(byte[] fileData);
    }
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
