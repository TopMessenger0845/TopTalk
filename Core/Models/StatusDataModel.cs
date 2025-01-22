
namespace TopTalk.Core.Models
{
    public class StatusDataModel
    {
        public string ContactName { get; set; }
        public Uri ContactPhoto { get; set; } = new("/Assets/2.jpg", UriKind.RelativeOrAbsolute);
        // public string StatusMessage { get; set; }
        public Uri StatusImage { get; set; } = new("/Assets/1.jpg", UriKind.RelativeOrAbsolute);
        public bool IsMeAddStatus { get; set; } = false;
    }
}
