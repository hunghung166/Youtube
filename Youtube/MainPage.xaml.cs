﻿using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Youtube
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private YouTubeService youtubeService =
            new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyDBf8bq5AKUSHfF_CF0eeZ2RCLzyfmOi5s",
                ApplicationName = "youtube"
            });
        List<Video> ListVideo = new List<Video>();
        private string TokenNextPage = null, TokenPrivPage = null;


        public MainPage()
        {
            this.InitializeComponent();
        }

        string PageToken = null;
        private async void ChannelSearchClick(object sender, RoutedEventArgs e)
        {
            var Request = youtubeService.Search.List("snippet");
            Request.ChannelId = chanelId.Text;
            Request.MaxResults = 25;
            Request.Type = "video";
            Request.Order = SearchResource.ListRequest.OrderEnum.Date;
            Request.PageToken = PageToken;
            var Result = await Request.ExecuteAsync();
            if (Result.NextPageToken != null)
                TokenNextPage = Result.NextPageToken;
            if (Result.PrevPageToken != null)
                TokenPrivPage = Result.PrevPageToken;

            foreach (var item in Result.Items)
            {
                ListVideo.Add(new Video
                {
                    Title = item.Snippet.Title,
                    Id = item.Id.VideoId,
                    Img = item.Snippet.Thumbnails.Default__.Url
                });
            }
            lv.ItemsSource = null;
            lv.ItemsSource = ListVideo;
        }

        private void lv_ItemClick(object sender, ItemClickEventArgs e)
        {
            Video video = e.ClickedItem as Video;
            Frame.Navigate(typeof(PageVideo), video);

        }

    }
}
