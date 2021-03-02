using Dashboard.GlobalElement;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.SubPages.PageKeyvalue
{
    /// <summary>
    /// Interaction logic for SubPageKeyvalue.xaml
    /// </summary>
    public partial class SubPageKeyvalue : UserControl, IPageKeyValue
    {
        public SubPageKeyvalue()
        {
            InitializeComponent();

            Init();

            BTNAddAchievement.Work += () =>
            {

            };

            BTNListView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Collapsed;
                PanelListView.Visibility = Visibility.Visible;

            };
            BTNGridView.Worker += () =>
            {
                PanelGridView.Visibility = Visibility.Visible;
                PanelListView.Visibility = Visibility.Collapsed;
            };

        }

        public void Init()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageKeyValue.ReciveKeys(result =>
            {
                try
                {

                    if (result["KeyValue"].AsBsonDocument.ElementCount >= 1)
                    {

                    }
                    else
                    {

                    }
                }
                catch (Exception)
                {

                    Debug.WriteLine("Create one");
                }
            });

        }
    }

    public interface IPageKeyValue
    {

        void Init();
    }
}
