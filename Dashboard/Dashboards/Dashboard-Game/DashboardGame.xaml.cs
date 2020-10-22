using Dashboard.Dashboards.Dashboard_Game.Elements.PageDashboard;
using Dashboard.Dashboards.Dashboard_Game.Elements.PagePlayer;
using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.PageAUT.Login;
using Dashboard.Dashboards.Dashboard_Game.PageLeaderboards;
using Dashboard.Dashboards.Dashboard_Game.SubPages;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageDocuments;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageEduction;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageHelps;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageInternalNotifaction;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport;
using Dashboard.GlobalElement;
using Dashboard.Properties;
using Microsoft.AspNetCore.SignalR.Client;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Dashboard.Dashboards.Dashboard_Game
{

    public partial class DashboardGame : Window
    {
        public static DashboardGame Dashboard;

        UserControl CurentPage;
        TextBlock CurentTab;

        //singnal
        internal string SignalID = "";
        HubConnection HubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44346/Signal").Build();

        InternalNotifaction internalNotifaction;
        BlurEffect Blur = new BlurEffect() { Radius = 0 };

        public DashboardGame()
        {
            InitializeComponent();
            Settings.Default.Reset();
            Dashboard = this;

            CurentTab = BTNDashboard;




            if (Settings.Default._id == "")
            {
                Blure(true);
                Root.Children.Add(new Login());
            }
            else
            {
                Debug.WriteLine(Settings.Default._id);
            }


            //action BTNHelp
            BTNHelp.MouseDown += (s, e) =>
            {

                Root.Children.Add(new SubPageHelp());
            };

            //action btn eduction
            BTNEduction.MouseDown += (s, e) =>
            {
                Root.Children.Add(new SubPageEduction());
            };


            //action BTN Studio
            BTNStudio.MouseDown += (s, e) =>
            {
                Root.Children.Add(new SubPageStudios());

            };

            //actionBTnSetting
            BTNSetting.MouseDown += (s, e) =>
            {
                Root.Children.Add(new PageSetting());

            };

            //action btn Support
            BTNSupport.MouseDown += (s, e) =>
            {

                Root.Children.Add(new SubpageSupport());
            };

            //action btn Notifaction
            BTNNotifaction.MouseDown += (s, e) =>
            {

                if (internalNotifaction != null)
                    Root.Children.Remove(internalNotifaction);

                var notif = new InternalNotifaction();

                Root.Children.Add(notif);
                internalNotifaction = notif;

            };

            //action btn cheack Health
            BTNCheackHealth.MouseDown += (s, e) =>
            {

                SDK.SDK_PageDashboards.DashboardGame.PageDashboard.CheackStatusServer(Result =>
                {
                    if (Result)
                    {
                        ((s as Border).Child as TextBlock).Foreground = new SolidColorBrush(Colors.LightGreen);
                        Notifaction("Server is Up", StatusMessage.Ok);
                    }
                    else
                    {
                        ((s as Border).Child as TextBlock).Foreground = new SolidColorBrush(Colors.Tomato);
                        Notifaction("Server connection error\n 1: Reset the program\n 2: Check your internet\n 3: Report the problem to support ", StatusMessage.Error);
                    }

                });

            };

            //actin Swichapp 
            SwitchAPP.MouseDown += (s, e) =>
            {
                Notifaction("App service will be added soon", StatusMessage.Ok);
            };

            //action Report Bug
            BTNReportBug.MouseDown += (s, e) =>
            {
                Root.Children.Add(new SubPagesReportBug());

            };

            //action Pane
            BTNOpenPane.MouseDown += (s, e) =>
            {
                Storyboard storyboard = new Storyboard();
                if (NameList.Width >= 100)
                {
                    DoubleAnimation Anim = new DoubleAnimation(100, 0, TimeSpan.FromSeconds(0.3));
                    Storyboard.SetTargetName(Anim, NameList.Name);
                    Storyboard.SetTargetProperty(Anim, new PropertyPath("Width"));
                    storyboard.Children.Add(Anim);
                }
                else
                {
                    DoubleAnimation Anim = new DoubleAnimation(0, 100, TimeSpan.FromSeconds(0.3));
                    Storyboard.SetTargetName(Anim, NameList.Name);
                    Storyboard.SetTargetProperty(Anim, new PropertyPath("Width"));
                    storyboard.Children.Add(Anim);
                }

                storyboard.Begin(this);

            };
            BTNOpenPane.MouseLeave += (s, e) =>
            {
                if (NameList.Width >= 1)
                {
                    BTNOpenPane.Foreground = new SolidColorBrush(Colors.Orange);
                    BTNOpenPane.Text = "\xEA49";
                }
                else
                {
                    BTNOpenPane.Text = "\xEA5B";
                }
            };

            //acton BTNDocuments
            BTNDocument.MouseDown += (s, e) =>
            {
                Root.Children.Add(new SubpageDocuments());
            };
        }


        internal async void ReciveNotifactions()
        {

            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();

                await HubConnection.SendAsync("AddClient", HubConnection.ConnectionId, SettingUser.Token);
                SignalID = SignalID = HubConnection.ConnectionId;

                HubConnection.On("Notifaction", () =>
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageDashboard.Notifaction(result =>
                    {
                        if (result["Support"].ToInt32() >= 1)
                        {
                            PlaceNotifactionSupport.Visibility = Visibility.Visible;
                            TextNumberNotifactionSupport.Text = result["Support"].ToString();
                        }
                        else
                        {
                            PlaceNotifactionSupport.Visibility = Visibility.Collapsed;
                        }


                        if (result["Logs"].ToInt32() >= 1)
                        {
                            PlaceNotifactionLogs.Visibility = Visibility.Visible;
                            TextNumbetNotifactionLogs.Text = result["Logs"].ToString();
                        }
                        else
                        {
                            PlaceNotifactionLogs.Visibility = Visibility.Collapsed;
                        }
                    },
                    () =>
                    {

                    });
                });

            }

        }



        internal async void Blure(bool OnOff)
        {
            if (OnOff)
            {
                PageDashboard.Effect = Blur;
                while (Blur.Radius < 10)
                {
                    Blur.Radius += 0.5d;
                    await Task.Delay(4);

                    if (Blur.Radius >= 10)
                        break;
                }
            }
            else
            {
                while (Blur.Radius > 0)
                {
                    Blur.Radius -= 0.5d;
                    await Task.Delay(4);

                    if (Blur.Radius <= 0)
                    {
                        PageDashboard.Effect = null;
                        break;

                    }
                }

            }
        }


        public void ChangeColor_Active(object sender, MouseEventArgs e)
        {
            var TextBlock = sender as TextBlock;
            ColorAnimation Anim = new ColorAnimation(fromValue: Colors.Gray, toValue: Colors.Orange, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

            DoubleAnimation Anim1 = new DoubleAnimation(18, 20, TimeSpan.FromSeconds(0.1));
            Storyboard.SetTargetName(Anim1, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("FontSize"));


            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);

            storyboard.Begin(this);

        }

        public void ChangeColor_DeActive(object sender, MouseEventArgs e)
        {
            var TextBlock = sender as TextBlock;

            ColorAnimation Anim = new ColorAnimation(fromValue: Colors.Orange, toValue: Colors.Gray, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

            DoubleAnimation Anim1 = new DoubleAnimation(20, 18, TimeSpan.FromSeconds(0.1));
            Storyboard.SetTargetName(Anim1, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("FontSize"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);
            storyboard.Begin(this);

        }


        private void ChangePage(object sender, MouseButtonEventArgs e)
        {
            Content.Children.Clear();

            CurentTab.Foreground = new SolidColorBrush(Colors.Gray);
            CurentTab.MouseLeave -= (s, ee) => { };

            switch ((sender as TextBlock).Name)
            {
                case "BTNLeaderboards":
                    CurentPage = new PageLeaderboard();
                    CurentTab = BTNLeaderboards;
                    break;
                case "BTNPlayers":
                    CurentPage = new PagePlayers();
                    CurentTab = BTNPlayers;
                    break;
                case "BTNDashboard":
                    CurentPage = new PageDashboard();
                    CurentTab = BTNDashboard;
                    break;
                default:
                    Debug.WriteLine("Not set");
                    break;
            }

            Content.Children.Add(CurentPage);
            CurentTab.Foreground = new SolidColorBrush(Colors.Orange);

            //Curent change Color
            CurentTab.MouseLeave += (s, ee) =>
            {
                CurentTab.Foreground = new SolidColorBrush(Colors.Orange);
            };

        }


        internal void ChangeStudio(BsonDocument DetailStudio, bool? NotifactionChange = null)
        {
            //fill Studio user
            SettingUser.CurentDetailStudio = DetailStudio;

            //Change Textheader dashboard
            TextStudioName.Text = SettingUser.CurentDetailStudio["Name"].ToString();

            //add subpageDashbaord to dashbaord
            Content.Children.Remove(CurentPage);
            CurentPage = new PageDashboard();
            Content.Children.Add(CurentPage);

            CurentTab.Foreground = new SolidColorBrush(Colors.Gray);
            CurentTab = BTNDashboard;
            CurentTab.Foreground = new SolidColorBrush(Colors.Orange);



            if (NotifactionChange != null)
            {
                Notifaction("Change Studio", StatusMessage.Ok);
            }

            //addLog

            SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Login", $"You login at {DateTime.Now} (Local Time)", new BsonDocument(), false, (Reslult) => { });
        }


        //Statics
        public static void Notifaction(string Message, StatusMessage Status)
        {
            new Notifaction.Notifaction(Message, Status);
        }
        public static void Dialog(string Message, string Header)
        {
            new Dialog(Message, Header);
        }

        public static async Task<MessageBoxResult> DialogYesNo(string Message)
        {
            var Dialog = new DialogYesNo(Message);
            Dashboard.Root.Children.Add(Dialog);

            return await Dialog.Result();
        }


    }
}
