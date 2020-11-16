using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageUpdate
{
    public partial class SubPageUpdate : UserControl
    {
        public SubPageUpdate()
        {
            InitializeComponent();

            ControlUpdate();
            
        }


        void ControlUpdate()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageDashboard.CheackUpdate(
                result =>
                {
                    if (result.ElementCount >= 1)
                    {
                        TextWaiting.Visibility = Visibility.Collapsed;

                        Version version = new Version(result["Version"].AsString);

                        //change Texts
                        TextVersionUpdated.Text = "v" + version.ToString();
                        TextCurentVersion.Text = Application.ResourceAssembly.GetName().Version.ToString();
                        TextLastVersion.Text = result["Version"].AsString;
                        TextDetail.Text = result["Detail"].AsString;
                        TextSize.Text = result["Size"].AsString;
                        TextLink.Text = result["URL"].AsString;
                        
                        BTNDownload.MouseDown += (s, e) =>
                        {
                            Process.Start(TextLink.Text);
                        };

                        if (version == Application.ResourceAssembly.GetName().Version)
                        {
                            PanelUpdated.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            PanelNewUpdate.Visibility = Visibility.Visible;
                            BTNCLose.Visibility = Visibility.Collapsed;
                        }

                    }
                },

                () =>
                {
                    TextWaiting.Text = "Faild :(";
                    DashboardGame.Notifaction("Faild Recive Update", Notifaction.StatusMessage.Error);
                });
        }


        public void Close(object Sender, RoutedEventArgs E)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }


        #region Statics

        public static void CheackUpdate()
        {
            var a = new UIElement();
            SDK.SDK_PageDashboards.DashboardGame.PageDashboard.CheackUpdate(
                result =>
                {
                    if (result.ElementCount >= 1)
                    {
                        Version version = new Version(result["Version"].AsString);

                        if (version != Application.ResourceAssembly.GetName().Version)
                        {
                            DashboardGame.Dashboard.Root.Children.Add(new SubPageUpdate());
                        }
                    }
                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Recive Update", Notifaction.StatusMessage.Error);
                });


        }

        #endregion
    }
}
