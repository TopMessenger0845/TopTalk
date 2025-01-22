using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TopTalk.Core.Storage.Models
{
    public class FileContainer
    {
        public Guid Id {  get; set; }
        public string FileName { get; set; }
        [MaxLength(1000), Column(TypeName ="Binary")]
        public byte[] FileData { get; set; }
    }
}
