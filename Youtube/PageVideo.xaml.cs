using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Youtube
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageVideo : Page
    {
        Video video;
        public PageVideo()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    video = e.Parameter as Video;
                    var Url = await YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality720P);
                    Player.Source = Url.Uri;
                }
                else
                {
                    MessageDialog message = new MessageDialog("you are not connect internet!...");
                    await message.ShowAsync();
                    this.Frame.GoBack();
                }
            }
            catch (Exception err)
            {

            }

        }
        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), new object());
        }
        private async void btn240p_Click(object sender, RoutedEventArgs e)
        {
            await YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality240P);
        }
        private async void btn480p_Click(object sender, RoutedEventArgs e)
        {
            await YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality480P);
        }
        private async void btn720p_Click(object sender, RoutedEventArgs e)
        {
            await YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality720P);
        }
        private async void btn1080p_Click(object sender, RoutedEventArgs e)
        {
            await YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality1080P);
        }

    }
}
