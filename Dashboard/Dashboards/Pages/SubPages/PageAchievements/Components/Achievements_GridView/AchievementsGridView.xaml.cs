﻿using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.SubPages.PageAchievements.Components.Achievements_GridView
{
    public partial class AchievementsGridView : UserControl, IAchievementGridview
    {
        BsonDocument Detail;
        public AchievementsGridView(BsonDocument Detail)
        {
            InitializeComponent();
            this.Detail = Detail;

            Init();

            BTNMore.MouseDown += (s, e) =>
            {
                if (PanelMore.Visibility == Visibility.Visible)
                {

                    CloseMore();
                }
                else
                {

                    OpenMore();
                }
            };

            MouseLeave += (s, e) =>
            {
                CloseMore();
            };

        }

        void OpenMore()
        {
            PanelMore.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3f));
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void CloseMore()
        {
            DoubleAnimation Anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3f));
            Anim.Completed += (s, e) =>
            {
                PanelMore.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        public void Init()
        {
            TextValue.Text = Detail["Value"].ToString();
            TextName.Text = Detail["Name"].ToString();
            TextCreated.Text = Detail["Created"].ToUniversalTime().ToString();
            TextPlayers.Text = Detail["Players"].ToString();
            TextToken.Text = Detail["Token"].ToString();
        }
    }


    internal interface IAchievementGridview
    {
        void Init();
    }
}
