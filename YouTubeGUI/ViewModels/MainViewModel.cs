using System.Linq;
using YouTubeGUI.Commands;
using YouTubeGUI.Models;
using YouTubeGUI.Stores;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _session = WorkplaceManager.DefaultWorkspace.Sessions.First();
            _session.Initialized += SessionInitialized;
            ContentNavigator.ViewModelChanged += NavigatorOnViewModelChanged;
            /* Commands */
            TopbarButtonCommand = new TopbarButtonCommand();
            NavigationPaneCommand = new NavigationPaneCommand();
            NavigationPaneCommand.TogglePane += () => { IsGuidePaneOpen = !IsGuidePaneOpen; OnPropertyChanged(nameof(IsGuidePaneOpen)); };
        }

        // Properties
        
        /// <summary>
        /// The session this view model is using!
        /// </summary>
        public Session Session => _session;
        /// <summary>
        /// The title the linked view has.
        /// </summary>
        public string WindowTitle { get; set; } = "YouTube Desktop";
        private readonly Session _session;

        public Topbar Topbar => _session.TopbarSnippet != null ? _session.TopbarSnippet.Topbar : new Topbar();

        // Current content viewmodel
        public ViewModelBase CurrentContentViewModel => ContentNavigator.CurrentContentViewModel;
        /* Commands */
        public TopbarButtonCommand TopbarButtonCommand { get; set; }
        public NavigationPaneCommand NavigationPaneCommand { get; set; }
        public bool IsGuidePaneOpen { get; set; } = true;
        
        /* Privates */
        private void NavigatorOnViewModelChanged() => OnPropertyChanged(nameof(CurrentContentViewModel));

        public override void Dispose()
        {
            _session.Initialized -= SessionInitialized;
            ContentNavigator.ViewModelChanged -= NavigatorOnViewModelChanged;
        }
        private void SessionInitialized()
        {
            OnPropertyChanged(nameof(Topbar));
        }
    }
}