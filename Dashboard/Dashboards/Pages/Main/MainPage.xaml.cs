using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.AddPlayer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.Main
{

    public partial class MainPage : UserControl
    {
        bool IsPanOpen = true;
        bool IsSettingUserOpen = false;


        public MainPage()
        {
            InitializeComponent();

            //ControlPan
            BTNNavigationClose.MouseDown += (s, e) =>
            {

                if (IsPanOpen)
                {
                    ClosePane();
                }
                else
                {
                    OpenPane();
                }
            };

            //openPanelSettings
            BTNOpenUserSetting.MouseDown += (s, e) =>
            {
                if (IsSettingUserOpen)
                {
                    ClosePanelUserSetting();
                }
                else
                {
                    OpenPanelUserSetting();

                }
            };

        }


        void ClosePane()
        {
            IsPanOpen = false;

            //anim change size pane
            DoubleAnimation Anim = new DoubleAnimation(50, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PaneNavigation.Name);
            Storyboard.SetTargetProperty(Anim, new System.Windows.PropertyPath("Width"));

            //anim change size font dashboard
            DoubleAnimation Anim1 = new DoubleAnimation(0.0, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim1, TextDashboard.Name);
            Storyboard.SetTargetProperty(Anim1, new System.Windows.PropertyPath("FontSize"));

            Anim1.Completed += (s, e) =>
            {
                TextDashboard.Foreground = new SolidColorBrush(Colors.Transparent);
            };



            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);


            storyboard.Begin(this);

        }

        void OpenPane()
        {
            IsPanOpen = true;

            //anim change size pane
            DoubleAnimation Anim = new DoubleAnimation(225, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PaneNavigation.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Width"));

            //anim change size font dashboard
            DoubleAnimation Anim1 = new DoubleAnimation(11, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim1, TextDashboard.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("FontSize"));

            TextDashboard.Foreground = (Brush)new BrushConverter().ConvertFromString("#d0e2ff");



            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);


            storyboard.Begin(this);
        }

        void OpenPanelUserSetting()
        {
            PanelSettingUser.Visibility = Visibility.Visible;
            IsSettingUserOpen = true;
            ArrowUsername.Text = "\xE96D";



            DoubleAnimation Anim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelSettingUser.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);

            storyboard.Begin(this);

        }
        void ClosePanelUserSetting()
        {
            IsSettingUserOpen = false;
            ArrowUsername.Text = "\xE96E";

            DoubleAnimation Anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelSettingUser.Visibility = Visibility.Visible;
            };
            Storyboard.SetTargetName(Anim, PanelSettingUser.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);

            storyboard.Begin(this);

        }
       
    }

}
