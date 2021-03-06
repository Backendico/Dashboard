﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.Notifaction
{

    public partial class Notifaction : UserControl
    {
        public Notifaction(string _Message, StatusMessage Status)
        {
            InitializeComponent();
            TextMessage.Text = _Message;

            switch (Status)
            {
                case StatusMessage.Error:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.Tomato);
                    break;
                case StatusMessage.Ok:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                    break;
                case StatusMessage.Warrning:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    break;
                default:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.Black);
                    break;
            }

            DashboardGame.Dashboard.PlaceNotifaction.Children.Add(this);
        }

        public void Close(object sender, EventArgs e)
        {
            try
            {
                DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
                Anim.Completed += (s, ee) =>
                {
                    DashboardGame.Dashboard.PlaceNotifaction.Children.Remove(this);
                };
                Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
                Storyboard.SetTargetName(Anim, BorderColor.Name);
                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(Anim);
                storyboard.Begin();
            }
            catch (Exception)
            {
                DashboardGame.Dashboard.PlaceNotifaction.Children.Remove(this);
            }
        }

    }

    public enum StatusMessage
    {

        Error, Ok, Warrning
    }
}
