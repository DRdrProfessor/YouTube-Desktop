using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;
using YouTubeGUI.Core;
using YouTubeGUI.Screens;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;
using YouTubeScrap.Data.Video;
using Button = Avalonia.Controls.Button;

namespace YouTubeGUI.Windows
{
    public class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = this;
#if DEBUG
            this.AttachDevTools();
#endif
            this.AddHandler(PointerReleasedEvent, Handler, handledEventsToo: true);
            SetContent(new LoadingScreen());
        }

        // Properties
        public YoutubeUser CurrentUser
        {
            get => _currentUser;
            init
            {
                _currentUser = value;
                Task.Run(async () =>
                {
                    Metadata = await CurrentUser.DataRequestTask;
                }).ContinueWith((t) => SetContent(new HomeScreen()), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public bool IsContentControlVisible { get; set; } = true;
        public bool IsVideoViewVisible { get; set; } = false;
        
        private readonly YoutubeUser _currentUser;
        public ResponseMetadata? Metadata
        {
            get => _metadata;
            set
            {
                if (value != null)
                    _metadata = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HomePageContentList));
                OnPropertyChanged(nameof(GuideEntries));
            }
        }
        private ResponseMetadata? _metadata;
        
        public List<ContentRender> HomePageContentList
        {
            get
            { //TODO: need to set the list once.
                if (Metadata?.Contents == null) return _homeContentList;
                foreach (var tab in Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
                    _homeContentList.AddRange(tab.Content.Contents);
                return _homeContentList;
            }
        }
        private readonly List<ContentRender> _homeContentList = new();
        
        public object? ContentView
        {
            get => _contentView;
            set
            {
                if (value != null) // Do not set value but call the property changed to update.
                    _contentView = value;
                OnPropertyChanged();
            }
        }
        private object? _contentView;
        
        public object? SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
        private object? _selectedItem;

        public VideoDataSnippet VideoInfo
        {
            get => _videoSnippet;
            set
            {
                if (value != null)
                    _videoSnippet = value;
                OnPropertyChanged();
            }
        }
        private VideoDataSnippet _videoSnippet;

        public List<GuideItemRenderer> GuideEntries
        {
            get
            {
                if (Metadata != null)
                    return Metadata.Items;
                return new List<GuideItemRenderer>();
            }
        }
        
        
        public new event PropertyChangedEventHandler? PropertyChanged;
        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void SetContent(Control element) => ContentView = element;
        
        // For now handle every click event on the window. Bindings would get too complicated.
        private void Handler(object? sender, PointerReleasedEventArgs e)
        {
            if (e.Source?.InteractiveParent == null) return;
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    switch (e.Source)
                    {
                        case Control control:
                            SelectedItem = control.DataContext;
                            HandleVideo(SelectedItem);
                            break;
                    }
                    break;
                case MouseButton.Middle:
                case MouseButton.Right:
                case MouseButton.XButton1:
                case MouseButton.XButton2:
                    break;
            }
        }
        public void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            
        }
        private VideoPlayWindow _vpw = new VideoPlayWindow();
        private void HandleVideo(object? controlSender)
        {
            if (controlSender is not ContentRender cRenderer) return;
            if (cRenderer.RichItem is RichItemRenderer richItemRenderer)
            {
                if (richItemRenderer.RichItemContent.VideoRenderer != null)
                {
                    Task.Run(async () =>
                    {
                        var videoResponse = CurrentUser.GetVideo(richItemRenderer.RichItemContent.VideoRenderer.VideoId);
                        VideoInfo = await videoResponse;
                    }).ContinueWith((t) => SetContent(new VideoPlayScreen(){ DataContext = VideoInfo }), TaskScheduler.FromCurrentSynchronizationContext());;
                }
            }
        }
    }
}