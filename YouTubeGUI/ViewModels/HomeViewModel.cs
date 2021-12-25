using YouTubeGUI.Stores;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data.Snippets;

namespace YouTubeGUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(YoutubeUser youtubeUser)
        {
            YoutubeUserStore.NotifyInitialRequestFinished += OnNotifyInitialRequestFinished;
        }
        
        private HomeSnippet _homeSnippet;
        public HomeSnippet HomeSnippetContent
        {
            get => _homeSnippet;
            set
            {
                _homeSnippet = value;
                OnPropertyChanged();
            }
        }

        private GuideSnippet _guideSnippet;
        public GuideSnippet GuideSnippetContent
        {
            get => _guideSnippet;
            set
            {
                _guideSnippet = value;
                OnPropertyChanged();
            }
        }

        private void OnNotifyInitialRequestFinished(HomeSnippet arg1, GuideSnippet arg2)
        {
            HomeSnippetContent = arg1;
            GuideSnippetContent = arg2;
        }

        public override void Dispose()
        {
            YoutubeUserStore.NotifyInitialRequestFinished -= OnNotifyInitialRequestFinished;
        }
    }
}