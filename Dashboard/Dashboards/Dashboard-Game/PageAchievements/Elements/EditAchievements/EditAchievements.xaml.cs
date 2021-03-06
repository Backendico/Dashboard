﻿using Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements.EditAchievements.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements.EditAchievements
{
    public partial class EditAchievements : UserControl
    {
        //global
        Grid CurentPage;
        Button CurentTab;

        //page list
        BsonDocument DetailAchievements;
        int CountList = 100;

        public EditAchievements(BsonDocument DetailAchievement, Action RefreshList)
        {
            InitializeComponent();
            this.DetailAchievements = DetailAchievement;

            CurentPage = ContentSetting;
            CurentTab = BTNSetting;

            TextAchievementsID.Text = DetailAchievement["Name"].ToString();
            TextValue.Text = DetailAchievement["Value"].ToString();
            TextCreated.Text = DetailAchievement["Created"].ToLocalTime().ToString();
            TextToken.Text = DetailAchievement["Token"].ToString();

            try
            {
                TextPercent.Text = ((DetailAchievement["Players"]["Achievements"].ToInt32() * 100) / DetailAchievement["TotalPlayer"].ToInt32()) + "%";
                TextPlayer.Text = DetailAchievement["Players"]["Achievements"].ToString();
            }
            catch (Exception)
            {
                TextPercent.Text = "0 %";
                TextPlayer.Text = "(0) No player received ";
            }


            //actions
            //copy token
            TextToken.MouseDown += GlobalEvents.CopyText;

            //action btnsave
            BTNSaveSetting.MouseDown += (s, e) =>
            {

                try
                {
                    long.Parse(TextValue.Text);

                    DetailAchievement["Value"] = long.Parse(TextValue.Text);

                    SDK.SDK_PageDashboards.DashboardGame.PageAchievements.EditAchievements(DetailAchievement["Token"].AsObjectId, DetailAchievement, result =>
                    {
                        if (result)
                        {
                            RefreshList();
                            DashboardGame.Notifaction("Updated", Notifaction.StatusMessage.Ok);

                            //add log
                            SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("modified achievement", $"Modify achievement  \" {DetailAchievement["Name"] } \" ", DetailAchievement, false, resultLog => { });

                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Update", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }
            };


            BTNShowPanelAdd.MouseDown += (s, e) =>
            {
                ShowPaneladdPlayer();
            };

            //close panel add
            PanelAddPlayer.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPaneladdPlayer();
                }
            };

            //actin add player to achievements
            BTNAddPlayer.MouseDown += (s, e) =>
            {
                try
                {
                    var SerilseDetail = new BsonDocument
                    {
                        {"Token",DetailAchievement["Token"] },
                        {"Name",DetailAchievement["Name"] },
                    };

                    SDK.SDK_PageDashboards.DashboardGame.PageAchievements.AddPlayerAchievements(ObjectId.Parse(TextboxTokenPlayer.Text), SerilseDetail, result =>
                    {
                        if (result)
                        {
                            ShowoffPaneladdPlayer();
                            ReciveList();

                            //add log
                            SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Add player to achievement", $"Player \" {TextboxTokenPlayer.Text} \" was added to the achievement of \" {DetailAchievement["Name"]} \"", new BsonDocument { }, false, resultLog => { });
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Add", Notifaction.StatusMessage.Error);
                        }
                    });
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }
            };

            //actin btn refresh
            BTNRefresh.MouseDown += (s, e) =>
            {
                ReciveList();
            };

            //action  inc countlist
            BTNSeeMore.MouseDown += (s, e) =>
            {
                CountList += 100;
                TextSeeMore.Text = CountList.ToString();
                ReciveList();
            };

            //refresh base list for edit setting
            Unloaded += (s, e) =>
            {
                RefreshList();
            };
        }


        void ChangePage(object sender, RoutedEventArgs e)
        {
            var ButtonTab = sender as Button;

            CurentPage.Visibility = Visibility.Collapsed;
            CurentTab.BorderBrush = new SolidColorBrush(Colors.Transparent);

            switch (ButtonTab.Content)
            {
                case "Setting":
                    {
                        CurentPage = ContentSetting;
                        CurentTab = BTNSetting;
                    }
                    break;
                case "List":
                    {
                        CurentPage = ContentList;
                        CurentTab = BTNListAchievemetns;
                        CountList = 100;
                        TextSeeMore.Text = CountList.ToString();
                        ReciveList();

                    }
                    break;
                default:
                    Debug.WriteLine("Not Set");
                    break;
            }

            CurentPage.Visibility = Visibility.Visible;
            CurentTab.BorderBrush = new SolidColorBrush(Colors.Orange);

        }



        public void Close(object Sender, RoutedEventArgs E)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }


        //page list
        private void ShowPaneladdPlayer()
        {
            PanelAddPlayer.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }
        private void ShowoffPaneladdPlayer()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                TextboxTokenPlayer.Text = "";

                PanelAddPlayer.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void ReciveList()
        {
            ContentPlaceAchievements.Children.Clear();

            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.RecivePlayersAchivementsList(DetailAchievements["Token"].AsObjectId, CountList, result =>
            {
                if (result.ElementCount >= 1)
                {

                    if (result["List"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in result["List"].AsBsonArray)
                        {
                            ContentPlaceAchievements.Children.Add(new ModelPlayersAchievements(item.AsBsonDocument, DetailAchievements, ReciveList));
                        }
                    }
                    else
                    {
                        ShowPaneladdPlayer();
                        DashboardGame.Notifaction("No player found", Notifaction.StatusMessage.Error);
                    }

                }
                else
                {
                    ShowPaneladdPlayer();
                    DashboardGame.Notifaction("No player found", Notifaction.StatusMessage.Error);
                }
            });
        }

    }
}
