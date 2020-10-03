﻿using Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport
{
    /// <summary>
    /// Interaction logic for SubpageSupport.xaml
    /// </summary>
    public partial class SubpageSupport : UserControl
    {

        public BsonDocument CurentSupport;


        public SubpageSupport()
        {
            InitializeComponent();

            //init btnsener //send support
            BTNAddSupport.MouseDown += (o, s) =>
            {
                var Detailsupport = new BsonDocument
                {
                    { "Header" ,TextboxTitle.Text},
                    {"Priority",ComboBoxPriority.SelectedIndex },
                    {"Part",ComboBoxPart.SelectedIndex },
                };
                SDK.SDK_PageDashboards.DashboardGame.PageSupport.AddSupport(Detailsupport, result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Support Add \n You will receive the answer soon", Notifaction.StatusMessage.Ok);
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild add", Notifaction.StatusMessage.Error);
                    }

                });

            };
            SDK.SDK_PageDashboards.DashboardGame.PageSupport.ReciveSupports(
                result =>
                {
                    foreach (var item in result[1].AsBsonArray)
                    {
                        PlaceContentSupport.Children.Add(new ModelSupport(item.AsBsonDocument, PageEachQuestion, this));
                    }
                },
                () =>
                {
                });
        }

        private void Close(object sender, EventArgs e)
        {
            DashboardGame.MainRoot.Children.Remove(this);
        }

        //PageEachMessage
        private void SendMessage(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(CurentSupport.ToString());

            if (TextMessage.Text.Length >= 1)
            {
                BsonDocument DetailMessage = new BsonDocument
                  {
                   {"Message",TextMessage.Text },
                   {"TokenUser",SettingUser.Token }
                  };

                SDK.SDK_PageDashboards.DashboardGame.PageSupport.AddMessage(CurentSupport["Token"].ToString(), DetailMessage, result =>
                {
                    Debug.WriteLine(result);
                });
            }
            else
            {
                DashboardGame.Notifaction("Message To Short", Notifaction.StatusMessage.Error);
            }
        }
        public void OpenEachPlayer()
        {
            DoubleAnimation Anim1 = new DoubleAnimation(0, 300, TimeSpan.FromSeconds(1));

            Storyboard.SetTargetName(Anim1, PageEachQuestion.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("Width"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim1);

            storyboard.Begin(this);

        }

        public void CloseEachQuestion(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation Anim = new DoubleAnimation(fromValue: 300, toValue: 0, TimeSpan.FromSeconds(1));

            Storyboard.SetTargetName(Anim, PageEachQuestion.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Width"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        public void OpenAddSupport(object sender, MouseButtonEventArgs e)
        {
            var Story = new Storyboard();
            if (PageEachQuestion.Width >= 0)
            {
                DoubleAnimation Anim1 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(1));

                Storyboard.SetTargetName(Anim1, PageEachQuestion.Name);
                Storyboard.SetTargetProperty(Anim1, new PropertyPath("Width"));
                Story.Children.Add(Anim1);
            }

            if (PageQuestions.Width >= 0)
            {
                DoubleAnimation Anim2 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(1));
                Storyboard.SetTargetName(Anim2, PageQuestions.Name);
                Storyboard.SetTargetProperty(Anim2, new PropertyPath("Width"));
                Story.Children.Add(Anim2);
            }


            DoubleAnimation Anim3 = new DoubleAnimation(0, 300, TimeSpan.FromSeconds(1));
            Storyboard.SetTargetName(Anim3, PageAddQuestions.Name);
            Storyboard.SetTargetProperty(Anim3, new PropertyPath("Width"));


            Story.Children.Add(Anim3);

            Story.Begin(this);
        }


        public void CLoseSupport(object sender,MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageSupport.CloseSupport(CurentSupport["Token"].ToString(), result =>
            {
                if (result)
                {
                    DisableEachPage();
                    DashboardGame.Notifaction($"Support \" {CurentSupport["Header"]} \" Closed", Notifaction.StatusMessage.Ok);
                
                }
                else
                {
                    DashboardGame.Notifaction($" Close Faild", Notifaction.StatusMessage.Error);
                }
            });
        }

        void DisableEachPage()
        {
            TextMessage.Visibility = Visibility.Collapsed;
            BTNSendMessage.Visibility = Visibility.Collapsed;
        }
        void EnableEachPage()
        {
            TextMessage.Visibility = Visibility.Visible;
            BTNSendMessage.Visibility = Visibility.Visible;
        }
    }
}
