using YouTubeGUI.Core;
using YouTubeGUI.ViewModels;
using YouTubeGUI.Windows;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI
{
    public class Bootstrapper
    {
        public MainViewModel MainViewModel;
        // Gets called when windows can be created.
        public readonly NotifyBootstrapInitialized NotifyInitialized;
        public Bootstrapper(ref DebugManager? dm, string[] mainArgs)
        {
            dm = new DebugManager(ref NotifyInitialized);
            Logger.Log("Bootstrapping...", LogType.Debug);
            NotifyInitialized += OnNotifyInitialized;
            SettingsManager.LoadSettings();
            Program.InitialUser = new YoutubeUser();
            Program.LibVlcManager = new LibVlcManager();
            //BUG: Somehow CEF fires up 2 more debug windows (Only seen this on Linux, not tested it on other platforms) that are transparent.
            //BUG: Idk what causing this but it is some sort of a bug, need to look into that. For now we are not calling the CEF initializer.
            //CefManager.InitializeCef(mainArgs);
        }

        private void OnNotifyInitialized()
        {
            MainViewModel = new MainViewModel();
            Program.MainWindow = new MainWindow
            {
                DataContext = MainViewModel
            };
        }
    }
    public delegate void NotifyBootstrapInitialized();
}