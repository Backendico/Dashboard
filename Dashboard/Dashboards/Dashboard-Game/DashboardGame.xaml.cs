using Dashboard.Dashboards.Dashboard_Game.Elements.PageDashboard;
using Dashboard.Dashboards.Dashboard_Game.Elements.PageLeaderboards;
using Dashboard.Dashboards.Dashboard_Game.Elements.PagePlayer;
using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.PageAUT.Login;
using Dashboard.Dashboards.Dashboard_Game.PageStudios;
using Dashboard.Dashboards.Dashboard_Game.SubPages;
using Dashboard.GlobalElement;
using Dashboard.Properties;
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
        public static Grid MainRoot;
        public static Grid _Dashboard;

        UserControl CurentPage;
        TextBlock CurentTab;

        public DashboardGame()
        {
            InitializeComponent();
            Settings.Default.Reset();
            MainRoot = Root;
            _Dashboard = PageDashboard;

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
                        SettingUser.CurentDetailStudio = result[0].AsBsonDocument;

                        TextStudioName.Text = SettingUser.CurentDetailStudio["Name"].ToString();

                        CurentPage = new PageDashboard();
                        Content.Children.Add(CurentPage);
                        CurentTab = BTNDashboard;

                    }
                },
                () =>
                {
                });

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
            Root.Children.Add(new SubPageSupport());
        }

        private void ChangePage(object sender, MouseButtonEventArgs e)
        {
            Content.Children.Clear();

            CurentTab.Foreground = new SolidColorBrush(Colors.Gray);


            switch ((sender as TextBlock).Name)
            {
                case "BTNLogs":
                    CurentPage = new PageLogs.PageLogs();
                    CurentTab = BTNLogs;
                    break;
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

        private void OpenPageBug(object sender, MouseButtonEventArgs e)
        {
            Root.Children.Add(new SubPagesReportBug());
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
            MainRoot.Children.Add(Dialog);

            return await Dialog.Result();
        }

        private void SwitchApp(object sender, MouseButtonEventArgs e)
        {
            Notifaction("App service will be added soon", StatusMessage.Ok);
        }

        public void hand()
        {
            Debug.WriteLine("hi");

        }
    }
}
