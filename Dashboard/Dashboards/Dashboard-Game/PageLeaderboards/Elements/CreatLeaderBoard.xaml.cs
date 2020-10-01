using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using RestSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    public partial class CreatLeaderBoard : UserControl
    {
        public CreatLeaderBoard(Action<object, RoutedEventArgs> Refreshlist)
        {
            InitializeComponent();
            this.Refreshlist = Refreshlist;
        }


        private void CheackNameLeaderboard(object sender, TextChangedEventArgs e)
        {
            var TextboxUsername = sender as TextBox;

            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Cheackname(TextNameLeaderboard.Text, result =>
            {
                if (result)
                {
                    TextboxUsername.BorderBrush = new SolidColorBrush(Colors.Tomato);
                    TextboxUsername.Text += new Random().Next();

                    DashboardGame.Notifaction("Leaderboard name is duplicate", StatusMessage.Warrning);
                }
                else
                {
                    TextboxUsername.BorderBrush = new SolidColorBrush(Colors.LightGreen);

                }

            });

        }

        private void Add(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (TextNameLeaderboard.Text.Length >= 6)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Creat(TextNameLeaderboard.Text, ComboboxReset.SelectedIndex, ComboboxSort.SelectedIndex, Result =>
                {
                    if (Result)
                    {
                        DashboardGame.Notifaction("Leaderboard Added", StatusMessage.Ok);
                        Refreshlist(null, null);
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Add", StatusMessage.Error);
                    }
                });
            }
            else
            {
                DashboardGame.Notifaction("Leaderboard name short ", StatusMessage.Warrning);
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }

        Action<object, RoutedEventArgs> Refreshlist;

    }
}
