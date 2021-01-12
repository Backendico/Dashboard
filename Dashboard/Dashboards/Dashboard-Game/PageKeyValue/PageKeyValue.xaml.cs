using Dashboard.Dashboards.Dashboard_Game.PageKeyValue.Elements;
using Dashboard.GlobalElement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PageKeyValue
{
    /// <summary>
    /// Interaction logic for PageKeyValue.xaml
    /// </summary>
    public partial class PageKeyValue : UserControl
    {
        public PageKeyValue()
        {
            InitializeComponent();
                ReciveListAchievements();

            //Action show panel add Key
            BTNShowAddKey.MouseDown += (s, e) =>
            {
                ShowPanelAddChievements();
            };

            //action btn add
            BTNaddKeyValue.MouseDown += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageKeyValue.AddKey(TextBoxKey.Text, TextBoxValue.Text, result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Key Added", Notifaction.StatusMessage.Ok);
                    }
                    else
                    {
                        DashboardGame.Notifaction("Key Dublicated", Notifaction.StatusMessage.Ok);
                    }
                    ShowOffPanelAchievements();
                });

            };

            //show off panel Add;
            PanelAddKey.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelAchievements();
                }
            };
        }



        public void ReciveListAchievements()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageKeyValue.ReciveKeys(result =>
            {
                if (result.ElementCount >= 1)
                {
                    foreach (var item in result["KeyValue"].AsBsonDocument)
                    {
                        PlaceContentValue.Children.Add(new ModelKeyValue());
                    }
                }
                else
                {
                    DashboardGame.Notifaction("No content", Notifaction.StatusMessage.Error);
                }
            });

        }

        void ShowPanelAddChievements()
        {
            PanelAddKey.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddKey.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void ShowOffPanelAchievements()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddKey.Visibility = Visibility.Collapsed;

                PanelAddKey.Visibility = Visibility.Collapsed;

                TextBoxKey.Text = "";
                TextBoxValue.Text = "";
            };

            Storyboard.SetTargetName(Anim, PanelAddKey.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

            TextBoxKey.BorderBrush = new SolidColorBrush(Colors.Gainsboro);
        }

    }
}
