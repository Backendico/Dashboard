using Dashboard.Dashboards.Dashboard_Game.Elements.PageDashboard;
using Dashboard.Dashboards.Dashboard_Game.Elements.PageLeaderboards;
using Dashboard.Dashboards.Dashboard_Game.Elements.PagePlayer;
using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.PageAUT.Login;
using Dashboard.Dashboards.Dashboard_Game.SubPages;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageHelps;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageInternalNotifaction;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios;
using Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport;
using Dashboard.GlobalElement;
using Dashboard.Properties;
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
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class DashboardGame : Window
    {
        public static DashboardGame Dashboard;

        UserControl CurentPage;
        TextBlock CurentTab;

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


        public void ControlPane(object sender, MouseButtonEventArgs e)
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
        }

        private void OpenStudios(object sender, MouseButtonEventArgs e)
        {
            Root.Children.Add(new SubPageStudios());
        }
        private void OpenSetting(object sender, MouseButtonEventArgs e)
        {
            Root.Children.Add(new PageSetting());
        }

        private void OpenSuppport(object sender, MouseButtonEventArgs e)
        {
            Root.Children.Add(new SubpageSupport());
        }

        private void ChangePage(object sender, MouseButtonEventArgs e)
        {
            Content.Children.Clear();

            CurentTab.Foreground = new SolidColorBrush(Colors.Gray);


            switch ((sender as TextBlock).Name)
            {
                case "BTNLeaderboards":
                    CurentPage = new PageLeaderBoards();
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
        }

        private void OpenPageReportBug(object sender, MouseButtonEventArgs e)
        {
            Root.Children.Add(new SubPagesReportBug());
        }

        private void OpenInternalNotifaction(object sender, MouseButtonEventArgs e)
        {
            if (internalNotifaction != null)
                Root.Children.Remove(internalNotifaction);

            var notif = new InternalNotifaction();

            Root.Children.Add(notif);
            internalNotifaction = notif;
        }

        private void CheackStatusServer(object sender, MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageDashboard.CheackStatusServer(Result =>
            {
                if (Result)
                {
                    ((sender as Border).Child as TextBlock).Foreground = new SolidColorBrush(Colors.LightGreen);
                    Notifaction("Server is Up", StatusMessage.Ok);
                }
                else
                {
                    ((sender as Border).Child as TextBlock).Foreground = new SolidColorBrush(Colors.Tomato);
                    Notifaction("Server connection error\n 1: Reset the program\n 2: Check your internet\n 3: Report the problem to support ", StatusMessage.Error);
                }

            });

        }

        private void SwitchToApp(object sender, MouseButtonEventArgs e)
        {
            Notifaction("App service will be added soon", StatusMessage.Ok);
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



            if (NotifactionChange != null)
            {
                Notifaction("Change Studio", StatusMessage.Ok);
            }

            //addLog

            SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Login", $"You login at {DateTime.Now} (Local Time)", new BsonDocument(), false, (Reslult) => { });
        }



        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            if (NameList.Width >= 100)
            {
                BTNOpenPane.Foreground = new SolidColorBrush(Colors.Orange);
                BTNOpenPane.Text = "\xEA49";
            }
            else
            {
                BTNOpenPane.Text = "\xEA5B";
            }

            CurentTab.Foreground = new SolidColorBrush(Colors.Orange);

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
