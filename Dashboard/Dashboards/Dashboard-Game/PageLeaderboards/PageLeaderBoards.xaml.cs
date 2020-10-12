using Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements;
using Dashboard.GlobalElement;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.PageLeaderboards
{
    /// <summary>
    /// Interaction logic for PageLeaderBoards.xaml
    /// </summary>
    public partial class PageLeaderBoards : UserControl
    {
        public PageLeaderBoards()
        {
            InitializeComponent();

            Start(null, null); ;
            PanelAddLeaderboard.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffSubpageAddLeaderboard(null, null);
                }
            };
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reciveleaderboards(
                resul =>
                {
                    if (resul.ElementCount >= 1)
                    {
                        PlaceLeaderboard.Children.Clear();
                        foreach (var item in resul)
                        {
                            PlaceLeaderboard.Children.Add(new ModelLeaderboardAbstract(item.Value.AsBsonDocument, Start));
                        }
                    }
                    else
                    {
                        ShowSubpageAddLeaderboard(null, null);
                    }
                },
                () =>
                {
                });
        }


        //subpages
        private void AddLeaderBoard(object sender, MouseButtonEventArgs e)
        {
            if (TextNameLeaderboard.Text.Length >= 6)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Creat(TextNameLeaderboard.Text, ComboboxReset.SelectedIndex, ComboboxSort.SelectedIndex, Result =>
                {
                    if (Result)
                    {
                        DashboardGame.Notifaction("Leaderboard Added", Notifaction.StatusMessage.Ok);
                        ShowOffSubpageAddLeaderboard(null, null);
                        Start(null, null);
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Add", Notifaction.StatusMessage.Error);
                    }
                });
            }
            else
            {
                DashboardGame.Notifaction("Leaderboard name short ", Notifaction.StatusMessage.Warrning);
            }
        }


        /// <summary>
        /// show subpage Add Leaderboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowSubpageAddLeaderboard(object sender, MouseButtonEventArgs e)
        {
            PanelAddLeaderboard.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddLeaderboard.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

        }


        /// <summary>
        /// showof Panel AddLeaderboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowOffSubpageAddLeaderboard(object sender, MouseButtonEventArgs e)
        {

            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                PanelAddLeaderboard.Visibility = Visibility.Collapsed;
                TextNameLeaderboard.Text = "";

            };
            Storyboard.SetTargetName(Anim, PanelAddLeaderboard.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
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

                    DashboardGame.Notifaction("Leaderboard name is duplicate", Notifaction.StatusMessage.Warrning);
                }
                else
                {
                    TextboxUsername.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                }

            });

        }


    }
}
