using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.Main
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            BTNNavigationClose.MouseDown += (s, e) =>
            {
                ClosePane();
            };

            BTNNavigationOpen.MouseDown += (s, e) =>
            {

                OpenPane();

            };
        }

        void ClosePane()
        {
            BTNNavigationOpen.Visibility = System.Windows.Visibility.Visible;

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



            //animation change size BTN navigation
            DoubleAnimation Anim2 = new DoubleAnimation(20, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim2, BTNNavigationOpen.Name);
            Storyboard.SetTargetProperty(Anim2, new System.Windows.PropertyPath("FontSize"));


            //animation change size BTN navigation
            DoubleAnimation Anim3 = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            

            Storyboard.SetTargetName(Anim3, BTNNavigationClose.Name);
            Storyboard.SetTargetProperty(Anim3, new System.Windows.PropertyPath("FontSize"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);
            storyboard.Children.Add(Anim2);
            storyboard.Children.Add(Anim3);

            storyboard.Begin(this);
            
        }

        void OpenPane()
        {
            //anim change size pane
            DoubleAnimation Anim = new DoubleAnimation(225, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PaneNavigation.Name);
            Storyboard.SetTargetProperty(Anim, new System.Windows.PropertyPath("Width"));

            //anim change size font dashboard
            DoubleAnimation Anim1 = new DoubleAnimation(11, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim1, TextDashboard.Name);
            Storyboard.SetTargetProperty(Anim1, new System.Windows.PropertyPath("FontSize"));

                TextDashboard.Foreground = (Brush)new BrushConverter().ConvertFromString("#d0e2ff");
          



            //animation change size BTN navigationopen
            DoubleAnimation Anim2 = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim2, BTNNavigationOpen.Name);
            Storyboard.SetTargetProperty(Anim2, new System.Windows.PropertyPath("FontSize"));


            //animation change size BTN navigation
            DoubleAnimation Anim3 = new DoubleAnimation(20, TimeSpan.FromSeconds(0.3));
            Anim3.Completed += (s, e) => {
                BTNNavigationOpen.Visibility = System.Windows.Visibility.Collapsed;
            };
            Storyboard.SetTargetName(Anim3, BTNNavigationClose.Name);
            Storyboard.SetTargetProperty(Anim3, new System.Windows.PropertyPath("FontSize"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);
            storyboard.Children.Add(Anim2);
            storyboard.Children.Add(Anim3);

            storyboard.Begin(this);
        }
    }
}
