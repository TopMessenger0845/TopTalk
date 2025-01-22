using TopTalk.Core.Services.FileServices;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Services.Builders
{
    /// <summary>
    /// Билдер отвечающий за создание объекта FileContainer
    /// </summary>
    public class FileContainerBuilder
    {
        private FileDataCompressorService fcs;
        public FileContainerBuilder()
        {
            fcs = new FileDataCompressorService();
        }
        public FileContainer Build(byte[] originalData, string fileName)
        {
            byte[] compressedData = fcs.Compress(originalData);
            FileContainer container = new FileContainer()
            {
                FileData = compressedData,
                FileName = fileName
            };
            return container;
        }
    }
}
