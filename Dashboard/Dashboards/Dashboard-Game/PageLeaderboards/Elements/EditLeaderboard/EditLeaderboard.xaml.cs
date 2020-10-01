﻿using Dashboard.GlobalElement;
using RestSharp;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MongoDB.Bson;
using System;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for EditLeaderboard.xaml
    /// </summary>
    public partial class EditLeaderboard : UserControl
    {
        BsonDocument Detail;
        int Count;

        public EditLeaderboard(BsonDocument Detail, Action<object, RoutedEventArgs> RefreshList)
        {
            InitializeComponent();

            this.Detail = Detail;
            this.RefreshList = RefreshList;

            TextLeaderboardName.Text = Detail["Name"].AsString;
            TextToken.Text = Detail["Token"].AsObjectId.ToString();
            TextName_Setting.Text = Detail["Name"].AsString;
            TextStart.Text = Detail["Start"].ToUniversalTime().ToString();

            ComboboxReset.SelectedIndex = Detail["Reset"].AsInt32;
            ComboboxSort.SelectedIndex = Detail["Sort"].AsInt32;

        }

        //global method
        private void ChangePage(object sender, RoutedEventArgs e)
        {
            var BTN = sender as Button;

            switch (BTN.Content)
            {
                case "Setting":
                    {
                        ContentLeaderboard.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Collapsed;
                        ContentSetting.Visibility = Visibility.Visible;


                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    }
                    break;
                case "Leaderboard":
                    {
                        ContentLeaderboard.Visibility = Visibility.Visible;
                        ContentSetting.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Collapsed;

                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.DarkOrange);

                    }
                    break;
                case "Backups":
                    {
                        ContentLeaderboard.Visibility = Visibility.Collapsed;
                        ContentSetting.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Visible;


                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    }
                    break;

                default:
                    Debug.WriteLine("not set");
                    break;
            }

        }

        private void Close(object sender,RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }


        //page1 
        private void CopyToken(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Clipboard.SetText((sender as TextBlock).Text);
            MessageBox.Show("Token Copied !");
        }



        //page2
        private void Save(object sender, RoutedEventArgs e)
        {
            Detail["Reset"] = ComboboxReset.SelectedIndex;
            Detail["Sort"] = ComboboxSort.SelectedIndex;
            Detail.Remove("Count");

            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.EditLeaderboard(Detail, result =>
             {
                 if (result)
                 {
                     MessageBox.Show("Saved !");
                     RefreshList(null, null);
                 }
                 else
                 {

                     MessageBox.Show("Not change! :(");
                 }

             });

        }
        private void AddPlayer(object sender, RoutedEventArgs e)
        {
            new AddPlayer(Detail["Name"].ToString(), ReciveLeaderboards).Show();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("All records are lost\n Do you want to continue?", "Warning", MessageBoxButton.YesNo);

            if (Result == MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reset(Detail["Name"].AsString,
                    () =>
                    {
                        MessageBox.Show("Reseted !");
                        ReciveLeaderboards();
                        TextStart.Text = DateTime.Now.ToString();
                    },
                    () =>
                    {
                        MessageBox.Show("Reset Faild !");
                    });
            }
        }

        private void Backup(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Backup(Detail["Name"].AsString,
                () =>
                {
                    MessageBox.Show("Saved");
                },
                () =>
                {
                    MessageBox.Show("Faild Save :(");
                });

        }


        private void VisibilityChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch ((sender as Grid).Name)
            {
                case "ContentHistory" when (sender as Grid).Visibility == Visibility.Visible:
                    {
                        ReciveBackup();
                    }
                    break;
                case "ContentLeaderboard" when (sender as Grid).Visibility == Visibility.Visible:
                    {
                        Count = 100;
                        ReciveLeaderboards();
                    }
                    break;

            }

        }



        public void ReciveLeaderboards()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Leaderboard(Count, Detail["Name"].ToString(),
                Result =>
                {
                    var Place = (ContentPlaceLeaderboard.Content = new StackPanel()) as StackPanel;

                    foreach (var item in Result.AsBsonDocument)
                    {
                        Place.Children.Add(new ContentValue(item.Value.AsBsonDocument.Add("NameLeaderboard", Detail["Name"]), ReciveLeaderboards));
                    }
                },
                () =>
                {
                });

        }


        public void ReciveBackup()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.BackupRecive(Detail["Name"].AsString,
                Result =>
                {
                    PlaceContentBackups.Children.Clear();
                    
                    foreach (var item in Result)
                    {
                        item.Value.AsBsonDocument.Add("NameLeaderboard", Detail["Name"]);
                        PlaceContentBackups.Children.Add(new ModelBackupAbstract(item.Value.AsBsonDocument,ReciveBackup));
                    }
                },
                () =>
                {
                    
                });
        }


        private void SeeMore(object sender, RoutedEventArgs e)
        {
            var btnseemore = sender as Button;
            Count += 100;

            btnseemore.Content = " SeeMore (" + Count + ")";
            ReciveLeaderboards();
        }


        Action<object, RoutedEventArgs> RefreshList;

    }
}
