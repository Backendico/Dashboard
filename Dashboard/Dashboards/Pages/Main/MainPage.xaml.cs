using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.Main
{

    public partial class MainPage : UserControl
    {

       
        bool IsPanOpen = true;

        public MainPage()
        {
            InitializeComponent();

            

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
            Storyboard.SetTargetProperty(Anim, new System.Windows.PropertyPath("Width"));

            //anim change size font dashboard
            DoubleAnimation Anim1 = new DoubleAnimation(11, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim1, TextDashboard.Name);
            Storyboard.SetTargetProperty(Anim1, new System.Windows.PropertyPath("FontSize"));

            TextDashboard.Foreground = (Brush)new BrushConverter().ConvertFromString("#d0e2ff");



            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);


            storyboard.Begin(this);
        }
    }
}
