using Dashboard.Dashboards.Dashboard_Game.PageContent.Elements.ModelContent;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageContent
{
    /// <summary>
    /// Interaction logic for PageContent.xaml
    /// </summary>
    public partial class PageContent : UserControl
    {
        int Count = 100;

        public PageContent()
        {
            InitializeComponent();

            InitPageContent();

            BTNAddContent.MouseDown += (s, e) =>
            {
                if (TextBoxName.Text.Length >= 4)
                {

                    SDK.SDK_PageDashboards.DashboardGame.PageContent.AddContent(TextBoxName.Text, result =>
                    {

                        if (result)
                        {

                            InitPageContent();
                            ShowOffPanelContent();
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Add", Notifaction.StatusMessage.Error);
                        }
                    });
                }
                else
                {
                    DashboardGame.Notifaction("Name Short", Notifaction.StatusMessage.Warrning);
                }

            };

            //close sub page
            PanelAddContent.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelContent();
                }
            };


            BTNShowAddAchievment.MouseDown += (s, e) =>
            {
                ShowPanelAddContent();
            };


            BTNSeeMorePlayer.MouseDown += (s, e) => {

                Count += 100;
                TextSeeMoreNumber.Text = Count.ToString();
            };
        }

        void InitPageContent()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageContent.RecieveContents(Count, result =>
            {
                PlaceContent.Children.Clear();
                if (result.ElementCount >= 1)
                {

                    if (result["Content"].AsBsonArray.Count >= 1)
                    {

                        foreach (var item in result["Content"].AsBsonArray)
                        {
                            PlaceContent.Children.Add(new ModelContent(item.AsBsonDocument));
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                        ShowPanelAddContent();
                    }
                }
                else
                {

                    DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                    ShowPanelAddContent();
                }

            });

        }


        void ShowPanelAddContent()
        {
            PanelAddContent.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddContent.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void ShowOffPanelContent()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddContent.Visibility = Visibility.Collapsed;

                PanelAddContent.Visibility = Visibility.Collapsed;

                TextBoxName.Text = "";
            };

            Storyboard.SetTargetName(Anim, PanelAddContent.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

            TextBoxName.BorderBrush = new SolidColorBrush(Colors.Gainsboro);
        }
    }
}
