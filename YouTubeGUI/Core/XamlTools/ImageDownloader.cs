using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using YouTubeScrap.Core;
using YouTubeScrap.Data.Extend;
using Image = Avalonia.Controls.Image;

namespace YouTubeGUI.Core.XamlTools
{
    public class ImageDownloader
    {
        static ImageDownloader()
        {
            ThumbnailsProperty.Changed
                .Where(args => args.IsEffectiveValueChange)
                .Subscribe(args => OnSourceChangedThumbnails((Image)args.Sender, args.NewValue.Value));
        }

        private static async void OnSourceChangedThumbnails(Image sender, List<Thumbnail> thumbnails)
        {
            var bytes = await GetFromWeb(thumbnails);
            if (bytes == null) return;

            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                var bitmap = new Bitmap(memStream);
                sender.Source = bitmap;
            }
        }

        private static async Task<byte[]> GetFromWeb(List<Thumbnail> thumbnails)
        {
            if (thumbnails == null) return null;
            var thumbnail = thumbnails.First().Url;
            if (thumbnail.IsNullEmpty())
            {
                Trace.WriteLine("No image urls found!");
                return null;
            }
            return await Program.Instance?.MainViewModel?.CurrentUser.NetworkHandler.GetDataAsync(thumbnail);
        }
        
        public static readonly AttachedProperty<List<Thumbnail>> ThumbnailsProperty = AvaloniaProperty.RegisterAttached<Image, List<Thumbnail>>("Thumbnails", typeof(ImageDownloader));
        
        public static List<Thumbnail> GetThumbnails(Image element)
        {
            return element.GetValue(ThumbnailsProperty);
        }

        public static void SetThumbnails(Image element, List<Thumbnail> value)
        {
            element.SetValue(ThumbnailsProperty, value);
        }
    }
}