using Dashboard.Dashboards.Dashboard_Game.Elements.PageDashboard;
using Dashboard.Dashboards.Dashboard_Game.Elements.PageLeaderboards;
using Dashboard.Dashboards.Dashboard_Game.Elements.PagePlayer;
using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.PageAUT.Login;
using Dashboard.Dashboards.Dashboard_Game.PageStudios;
using Dashboard.Dashboards.Dashboard_Game.SubPages;
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

        public DashboardGame()
        {
            InitializeComponent();
            Settings.Default.Reset();
            Dashboard = this;

            if (Settings.Default._id == "")
            {
                PageDashboard.Effect = new BlurEffect();
                Root.Children.Add(new Login(Init));
            }
            else
            {
                Debug.WriteLine(Settings.Default._id);
            }

        }


        /// <summary>
        /// 1: recive Studios
        /// 2 if no studio page creat studio show
        /// 3: if studio found frist srudio init 
        /// </summary>
        public void Init()
        {

            SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveStudios(
                result =>
                {
                    if (result.ElementCount <= 0)
                    {

                        Root.Children.Add(new CreatStudio());
                    }
                    else
                    {
                        //fill Studio user
                        SettingUser.CurentDetailStudio = result[0].AsBsonDocument;

                        //Change Textheader dashboard
                        TextStudioName.Text = SettingUser.CurentDetailStudio["Name"].ToString();

                        //add subpageDashbaord to dashbaord
                        CurentPage = new PageDashboard();
                        Content.Children.Add(CurentPage);
                        CurentTab = BTNDashboard;

                        //add log
                        SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Login", $"You logged in at {DateTime.Now} ", new BsonDocument { }, false, resultlog => { });
                    }
                },
                () =>
                {
                });
        }

        internal void Blure(bool OnOff)
        {
            if (OnOff)
            {
                PageDashboard.Effect = new BlurEffect();
            }
            else
            {
                PageDashboard.Effect = null;
            }
        }

        private void HoverColor(object sender, MouseEventArgs e)
        {
            var Hove = sender as TextBlock;
            if (Hove.Name != CurentTab.Name)
            {
                Hove.Foreground = new SolidColorBrush(Colors.Orange);
            }
        }

        private void ExtitColor(object sender, MouseEventArgs e)
        {
            var Hove = sender as TextBlock;
            if (Hove.Name != CurentTab.Name)
            {

                Hove.Foreground = new SolidColorBrush(Colors.Gray);
            }

        }

        private void OpenStudios(object sender, MouseButtonEventArgs e)
        {
            Root.Children.Add(new SubPageStudios());
            Blure(true);
        }

        private void BackgroundChangeEnter(object sender, MouseEventArgs e)
        {
            var Borders = sender as Border;
            Borders.Background = new SolidColorBrush(new Color { R = 255, G = 255, B = 255, A = 33 });
        }

        private void BackgroundChangeLeave(object sender, MouseEventArgs e)
        {
            var Borders = sender as Border;
            Borders.Background = new SolidColorBrush(Colors.Transparent);
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

        private void SwitchApp(object sender, MouseButtonEventArgs e)
        {
            Notifaction("App service will be added soon", StatusMessage.Ok);
        }


        //Statics
        public static void ChangeStudio(DashboardGame Dashboard)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveStudios(
              result =>
              {
                  if (result.ElementCount <= 0)
                  {

                      Dashboard.Root.Children.Add(new CreatStudio());
                  }
                  else
                  {
                      //fill Studio user
                      SettingUser.CurentDetailStudio = result[0].AsBsonDocument;

                      //Change Textheader dashboard
                      Dashboard.TextStudioName.Text = SettingUser.CurentDetailStudio["Name"].ToString();

                      //add subpageDashbaord to dashbaord
                      Dashboard.CurentPage = new PageDashboard();

                      Dashboard.Content.Children.Add(Dashboard.CurentPage);
                      Dashboard.CurentTab = Dashboard.BTNDashboard;

                      //add log
                      SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Login", $"You logged in at {DateTime.Now} ", new BsonDocument { }, false, resultlog => { });
                  }
              },
              () =>
              {
              });
        }

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
