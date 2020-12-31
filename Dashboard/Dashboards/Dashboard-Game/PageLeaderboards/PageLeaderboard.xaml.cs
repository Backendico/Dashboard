using Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards
{
   
    public partial class PageLeaderboard : UserControl
    {
        public PageLeaderboard()
        {
            InitializeComponent();

            ReciveLeaderboards();




            #region SubPage Add Leaderboard

            BTNAddLeaderboard.MouseDown += (s, e) =>
            {
                if (TextNameLeaderboard.Text.Length >= 6)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveMonetize(result =>
                    {
                        if (PlaceLeaderboard.Children.Count + 1 <= result["Leaderboards"].ToInt32())
                        {
                            try
                            {
                                //amount not 0
                                if (int.Parse(TextAmount.Text) == 0)
                                    TextAmount.Text = 1.ToString();


                                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Creat(TextNameLeaderboard.Text, ComboboxReset.SelectedIndex, Int64.Parse(TextAmount.Text), ComboboxSort.SelectedIndex, int.Parse(TextMinValue.Text), Int64.Parse(TextMaxValue.Text), Result =>
                                   {
                                       if (Result)
                                       {
                                           DashboardGame.Notifaction("Leaderboard Added", Notifaction.StatusMessage.Ok);
                                           ShowOffSubpageAddLeaderboard();
                                           ReciveLeaderboards();
                                       }
                                       else
                                       {
                                           DashboardGame.Notifaction("Faild Add", Notifaction.StatusMessage.Error);
                                       }
                                   });
                            }
                            catch (Exception ex)
                            {
                                TextMinValue.Text = 0.ToString();
                                TextMaxValue.Text = 10000.ToString();
                                TextAmount.Text = 1.ToString();
                                DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                            }
                        }
                        else
                        {
                            DashboardGame.Notifaction("You can not add more Leaderboard.See Payments to buy a leaderboard.", Notifaction.StatusMessage.Warrning);
                        }

                    }, () => { });
                }
                else
                {
                    DashboardGame.Notifaction("Leaderboard name short ", Notifaction.StatusMessage.Warrning);
                }
            };
           
            BTNShowPanelAddLeaderboards.MouseDown += (s, e) =>
            {
                ShowSubpageAddLeaderboard();
            };
           
            PanelAddLeaderboard.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffSubpageAddLeaderboard();
                }
            };
          
            ComboboxReset.SelectionChanged += (s, e) =>
            {
                ChangeAmount();
            };

            #endregion
        }


        private void ReciveLeaderboards()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reciveleaderboards(
                result =>
                {

                    if (result.ElementCount >= 1)
                    {
                        PlaceLeaderboard.Children.Clear();
                        foreach (var item in result[1].AsBsonArray)
                        {
                            PlaceLeaderboard.Children.Add(new ModelLeaderboardAbstract(item.AsBsonDocument, ReciveLeaderboards));
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No content", Notifaction.StatusMessage.Warrning);
                        ShowSubpageAddLeaderboard();
                    }
                },
                () =>
                {
                });
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


        //subpages

        /// <summary>
        /// show subpage Add Leaderboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowSubpageAddLeaderboard()
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
        void ShowOffSubpageAddLeaderboard()
        {

            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                PanelAddLeaderboard.Visibility = Visibility.Collapsed;
                TextNameLeaderboard.Text = "";
                ComboboxReset.SelectedIndex = 0;

            };
            Storyboard.SetTargetName(Anim, PanelAddLeaderboard.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        void ChangeAmount()
        {
            if (ComboboxReset.SelectedIndex == 0)
            {
                PanelAmount.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelAmount.Visibility = Visibility.Visible;
            }
        }
    }
}
