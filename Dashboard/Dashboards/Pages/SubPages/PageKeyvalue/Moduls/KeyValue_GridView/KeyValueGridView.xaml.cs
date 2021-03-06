using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.SubPages.PageKeyvalue.Moduls.KeyValue_GridView
{
    /// <summary>
    /// Interaction logic for KeyValueGridView.xaml
    /// </summary>
    public partial class KeyValueGridView : UserControl
    {
        public KeyValueGridView()
        {
            InitializeComponent();
            BTNMore.MouseDown += (s, e) =>
            {
                if (PanelMore.Visibility == Visibility.Visible)
                {

                    CloseMore();
                }
                else
                {

                    OpenMore();
                }
            };

            MouseLeave += (s, e) =>
            {
                CloseMore();
            };
        }

        void OpenMore()
        {
            PanelMore.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3f));
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void CloseMore()
        {
            DoubleAnimation Anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3f));
            Anim.Completed += (s, e) =>
            {
                PanelMore.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

    }
}
