﻿using Dashboard.Dashboards.Dashboard_Game.PageAchievements.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PageAchievements
{
    /// <summary>
    /// Interaction logic for PageAchievements.xaml
    /// </summary>
    public partial class PageAchievements : UserControl
    {
        long Value = 0;


        public PageAchievements()
        {
            InitializeComponent();
            ReciveListAchievements();

            //actions
            //action btnaddachivemenents 
            BTNaddAchievements.MouseDown += (s, e) =>
            {
                try
                {
                    if (TextBoxName.Text.Length == 0)
                        throw new FormatException();

                    long.Parse(TextBoxValue.Text);

                    SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveMonetize(Monetize =>
                    {
                        if (PlaceContentAchievements.Children.Count + 1 <= Monetize["Achievements"].ToInt32())
                        {

                            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.AddAchievements(TextBoxName.Text, long.Parse(TextBoxValue.Text), result =>
                            {
                                ShowOffPanelAchievements();
                                ReciveListAchievements();

                                //add log
                                SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Creat achievement", $"\" {TextBoxName.Text} \" achievement was created", new BsonDocument(), false, resultlog => { });

                            });
                        }
                        else
                        {
                            DashboardGame.Notifaction("You can not create new achievements. Buy more achievements", Notifaction.StatusMessage.Error);
                        }

                    }, () => { });


                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }
            };
            //action btnshow panel achievements
            BTNShowAddAchievment.MouseDown += (s, e) =>
            {
                ShowPanelAddChievements();
            };


            //action cheack name
            TextBoxName.TextChanged += (s, e) =>
            {
                var Text = s as TextBox;
                SDK.SDK_PageDashboards.DashboardGame.PageAchievements.CheackAchievements(Text.Text, result =>
                {
                    if (result)
                    {
                        Text.BorderBrush = new SolidColorBrush(Colors.Tomato);
                        DashboardGame.Notifaction($"\" {Text.Text} \" Duplicate name", Notifaction.StatusMessage.Error);
                        Text.Text += new Random().Next();
                        DashboardGame.Notifaction($"Rename to \" {Text.Text} \"", Notifaction.StatusMessage.Ok);
                    }
                    else
                    {
                        Text.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                    }
                });

            };


            //close panel add
            PanelAddAchievement.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelAchievements();
                }
            };
        }

        public void ReciveListAchievements()
        {
            PlaceContentAchievements.Children.Clear();
            Value = 0;
            TextTotallValue.Text = "0";
            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.ReciveAchievements(result =>
            {
                if (result.ElementCount >= 1)
                {
                    if (result["Achievements"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in result["Achievements"].AsBsonArray)
                        {
                            Value += item.AsBsonDocument["Value"].ToInt64();

                            item.AsBsonDocument.Add(new BsonElement("TotalPlayer", result["TotalPlayer"]));

                            PlaceContentAchievements.Children.Add(new ModelAchivments(item.AsBsonDocument, ReciveListAchievements));
                            TextTotallValue.Text = Value.ToString();
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No content", Notifaction.StatusMessage.Warrning);
                        ShowPanelAddChievements();
                    }
                }
                else
                {
                    ShowPanelAddChievements();
                }

            });

        }

        void ShowPanelAddChievements()
        {
            PanelAddAchievement.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddAchievement.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void ShowOffPanelAchievements()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddAchievement.Visibility = Visibility.Collapsed;

                PanelAddAchievement.Visibility = Visibility.Collapsed;

                TextBoxName.Text = "";
                TextBoxValue.Text = "";
            };

            Storyboard.SetTargetName(Anim, PanelAddAchievement.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

            TextBoxName.BorderBrush = new SolidColorBrush(Colors.Gainsboro);
        }
    }
}
