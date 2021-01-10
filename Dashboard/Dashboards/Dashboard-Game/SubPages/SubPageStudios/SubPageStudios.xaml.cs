using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios.Element;
using Dashboard.GlobalElement;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios
{

    public partial class SubPageStudios : UserControl
    {
        public SubPageStudios()
        {
            InitializeComponent();
            //control blure
            DashboardGame.Dashboard.Blure(true);
            Unloaded += (s, e) =>
            {
                DashboardGame.Dashboard.Blure(false);
            };


            ReciveStudios();


            BTNAddStudio.MouseDown += (o, s) =>
            {
                ShowPaneladdStudio();
            };

            BTNadd.MouseDown += (s, e) =>
            {
                if (TextNameStudio.Text.Length >= 1)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageStudios.CreatStudio(TextNameStudio.Text, result =>
                    {
                        if (result)
                        {
                            ReciveStudios();
                            ShowOffPanelStudio();
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Add", Notifaction.StatusMessage.Error);
                        }
                    });
                }
                else
                {
                    DashboardGame.Notifaction("Name Studio Short", Notifaction.StatusMessage.Error);
                }
            };

            PanelAddStudio.MouseDown += (s, e) =>
            {
                ShowOffPanelStudio();
            };

        }

        internal void ReciveStudios()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveStudios(
                result =>
                {
                    SettingUser.ServerTime = new DateTime(result["ServerTime"].AsInt64);

                    PlaceContentStudios.Children.Clear();
                    if (result["Settings"].AsBsonArray.Count <= 0)
                    {
                        ShowPaneladdStudio();
                    }
                    else
                    {
                        foreach (var item in result["Settings"].AsBsonArray)
                        {
                            PlaceContentStudios.Children.Add(new ModelStudio(item["Setting"].AsBsonDocument, this));
                        }
                    }
                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Recive", Notifaction.StatusMessage.Error);
                }
                );

        }

        internal void ShowPaneladdStudio()
        {
            PanelAddStudio.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));


            Storyboard.SetTargetName(Anim, PanelAddStudio.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

        }

        internal void ShowOffPanelStudio()
        {
            TextNameStudio.Text = "";

            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddStudio.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetName(Anim, PanelAddStudio.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }
    }
}
