﻿using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.SubPages.PageLeaderboards.Moduls.AddLeaderboard;
using Dashboard.Dashboards.Pages.SubPages.PageLeaderboards.Moduls.Leaderboard_GridView;
using Dashboard.Dashboards.Pages.SubPages.PageLeaderboards.Moduls.Leaderboard_ListView;
using Dashboard.GlobalElement;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.SubPages.PageLeaderboards
{
    /// <summary>
    /// Interaction logic for SubpageLeaderboards.xaml
    /// </summary>
    public partial class SubpageLeaderboards : UserControl, IPageLeaderboard
    {
        public SubpageLeaderboards()
        {
            InitializeComponent();

            Init();

            BTNaddLeaderboard.Work += () =>
            {
                PageAUT.Placeholder.Children.Add(new SubpageAddLeaderboards());
            };

            BTNListView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Collapsed;
                PanelListView.Visibility = Visibility.Visible;

            };
            BTNGridView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Visible;
                PanelListView.Visibility = Visibility.Collapsed;
            };
        }

        public void Init()
        {
            ContentPlaceHolderGridviewe.Children.Clear();
            ContentPlaceHolderListView.Children.Clear();

            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reciveleaderboards(Result =>
            {
                if (Result.ElementCount >= 1)
                {
                    if (Result["List"].AsBsonArray.Count >= 1)
                    {
                        int Zindex = Result["List"].AsBsonArray.Count;



                        foreach (var item in Result["List"].AsBsonArray)
                        {
                            item["Settings"].AsBsonDocument.Add("Zindex", Zindex);

                            ContentPlaceHolderGridviewe.Children.Add(new LeaderboardGridView(item["Settings"].AsBsonDocument));
                            ContentPlaceHolderListView.Children.Add(new LeaderboardListView(item["Settings"].AsBsonDocument));
                            Zindex--;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No leaderboard");
                        PageAUT.Placeholder.Children.Add(new SubpageAddLeaderboards());
                    }
                }
                else
                {
                    Debug.WriteLine("No Leaderboard");
                    PageAUT.Placeholder.Children.Add(new SubpageAddLeaderboards());
                }

            });

        }
    }
    public interface IPageLeaderboard
    {
        void Init();
    }

    public enum SortLeaderboard
    {
        Last,
        Minimum,
        Maxmimum,
        Sum,
    }


    public enum ResetLeaderboard
    {
        Manually,
        Hourly,
        Daily,
        Weekly,
        Monthly
    }
}
