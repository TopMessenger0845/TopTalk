
using System.Collections.ObjectModel;
using TopTalk.Core.Models;

namespace TopTalk.Core.ViewModels
{
    public class ViewModel
    {
        public ObservableCollection<StatusDataModel> StatusThumbsCollection { get; set; }
        public ViewModel() 
        {
            StatusThumbsCollection =
            [
                new() {ContactName="Art"},
                new() {ContactName="TopTalk"},
                new() {ContactName="Arsyha"},
            ];
        }
    }
}
