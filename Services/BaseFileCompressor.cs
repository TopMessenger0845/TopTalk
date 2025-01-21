
namespace TopTalk.Services
{
    public abstract class BaseFileCompressor
    {
        public abstract byte[] CompressFile(byte[] fileData);
        public abstract byte[] DecompressFile(byte[] fileData);
    }
}
