using Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element;
using Dashboard.GlobalElement;
using Microsoft.Win32;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport
{
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
                        ReciveSupportList();
                        OpenPageQuestions(null, null);
                        DashboardGame.Notifaction("Support Add \n You will receive the answer soon", Notifaction.StatusMessage.Ok);

                        //log
                        SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Support Created", $"You created the \"{Detailsupport["Header"]}\" ticket", Detailsupport, false, (R) => { });
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild add", Notifaction.StatusMessage.Error);
                    }

                });

            };

            //init btnReload
            BTNReload.MouseDown += (s, e) =>
            {
                ReciveSupportList();
                OpenPageQuestions(null, null);
            };

            ReciveSupportList();
        }


        private void Close(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() == typeof(Grid))
            {
                DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));

                Storyboard.SetTargetName(Anim, Root.Name);
                Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
                Storyboard storyboard = new Storyboard();

                storyboard.Children.Add(Anim);
                storyboard.Completed += (s, ee) =>
                {
                    DashboardGame.Dashboard.Root.Children.Remove(this);
                };
                storyboard.Begin(this);
            }
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

        public void OpenPageQuestions(object sender, MouseButtonEventArgs e)
        {
            var Story = new Storyboard();
            if (PageEachQuestion.Width > 0)
            {
                DoubleAnimation Anim1 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(0.3));

                Storyboard.SetTargetName(Anim1, PageEachQuestion.Name);
                Storyboard.SetTargetProperty(Anim1, new PropertyPath("Width"));
                Story.Children.Add(Anim1);
            }

            if (PageAddQuestions.Width > 0)
            {
                DoubleAnimation Anim3 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim3, PageAddQuestions.Name);
                Storyboard.SetTargetProperty(Anim3, new PropertyPath("Width"));
                Story.Children.Add(Anim3);
            }

            DoubleAnimation Anim2 = new DoubleAnimation(0, 300, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim2, PageQuestions.Name);
            Storyboard.SetTargetProperty(Anim2, new PropertyPath("Width"));
            Story.Children.Add(Anim2);

            Story.Begin(this);

        }

        public void OpenEachSupport()
        {
            var Story = new Storyboard();

            if (PageQuestions.Width > 0)
            {
                DoubleAnimation Anim2 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim2, PageQuestions.Name);
                Storyboard.SetTargetProperty(Anim2, new PropertyPath("Width"));
                Story.Children.Add(Anim2);
            }

            if (PageAddQuestions.Width > 0)
            {
                DoubleAnimation Anim3 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim3, PageAddQuestions.Name);
                Storyboard.SetTargetProperty(Anim3, new PropertyPath("Width"));
                Story.Children.Add(Anim3);
            }

            DoubleAnimation Anim1 = new DoubleAnimation(0, 300, TimeSpan.FromSeconds(0.3));

            Storyboard.SetTargetName(Anim1, PageEachQuestion.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("Width"));
            Story.Children.Add(Anim1);



            Story.Begin(this);


            if (CurentSupport["IsOpen"].AsBoolean)
            {
                EnableEachPage();
            }
            else
            {
                DisableEachPage();
            }


        }

        public void OpenAddSupport(object sender, MouseButtonEventArgs e)
        {
            var Story = new Storyboard();

            if (PageEachQuestion.Width > 0)
            {
                DoubleAnimation Anim1 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(0.3));

                Storyboard.SetTargetName(Anim1, PageEachQuestion.Name);
                Storyboard.SetTargetProperty(Anim1, new PropertyPath("Width"));
                Story.Children.Add(Anim1);
            }

            if (PageQuestions.Width > 0)
            {
                DoubleAnimation Anim2 = new DoubleAnimation(300, 0, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim2, PageQuestions.Name);
                Storyboard.SetTargetProperty(Anim2, new PropertyPath("Width"));
                Story.Children.Add(Anim2);
            }


            DoubleAnimation Anim3 = new DoubleAnimation(0, 300, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim3, PageAddQuestions.Name);
            Storyboard.SetTargetProperty(Anim3, new PropertyPath("Width"));


            Story.Children.Add(Anim3);

            Story.Begin(this);

        }

        public void CloseSupport(object sender, MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageSupport.CloseSupport(CurentSupport["Token"].ToString(), result =>
            {
                if (result)
                {
                    DisableEachPage();
                    ReciveSupportList();
                    DashboardGame.Notifaction($"Support \" {CurentSupport["Header"]} \" Closed", Notifaction.StatusMessage.Ok);
                }
                else
                {
                    DashboardGame.Notifaction($" Close Faild", Notifaction.StatusMessage.Error);
                }
            });
        }


        void ReciveSupportList()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageSupport.ReciveSupports(
                result =>
                {
                    Debug.WriteLine(result[1].AsBsonArray.Count);
                    if (result[1].AsBsonArray.Count >= 1)
                    {
                        PlaceContentSupport.Children.Clear();
                        foreach (var item in result[1].AsBsonArray)
                        {
                            PlaceContentSupport.Children.Add(new ModelSupport(item.AsBsonDocument, this));
                        }
                    }
                    else
                    {
                        OpenAddSupport(null, null);

                    }

                },
                () =>
                {
                    OpenAddSupport(null,null);
                });
        }

        void DisableEachPage()
        {
            TextMessage.Visibility = Visibility.Collapsed;
            BTNSendMessage.Visibility = Visibility.Collapsed;

            BTNClose_EachSupport.Text = "\xE72E";

            BTNClose_EachSupport.ToolTip = "Support Closed";

            BTNClose_EachSupport.MouseDown -= CloseSupport;

            BTNClose_EachSupport.Cursor = Cursors.No;
        }
        void EnableEachPage()
        {
            TextMessage.Visibility = Visibility.Visible;
            BTNSendMessage.Visibility = Visibility.Visible;

            BTNClose_EachSupport.Text = "\xE785";

            BTNClose_EachSupport.ToolTip = "Close Support";

            BTNClose_EachSupport.MouseDown -= CloseSupport;
            BTNClose_EachSupport.MouseDown += CloseSupport;

            BTNClose_EachSupport.Cursor = Cursors.Hand;
        }

    }
}
