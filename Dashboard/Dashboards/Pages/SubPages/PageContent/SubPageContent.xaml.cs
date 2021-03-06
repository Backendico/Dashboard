using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.SubPages.PageContent.Moduls.AddContent;
using Dashboard.GlobalElement;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.SubPages.PageContent
{
    /// <summary>
    /// Interaction logic for SubPageContent.xaml
    /// </summary>
    public partial class SubPageContent : UserControl, IPageContent
    {
        public SubPageContent()
        {
            InitializeComponent();

            Init();

            BTNAddAchievement.Work += () =>
            {
                PageAUT.Placeholder.Children.Add(new AddContent(this));
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
            SDK.SDK_PageDashboards.DashboardGame.PageContent.RecieveContents(100, result =>
            {

                Debug.WriteLine(result);
                try
                {
                    if (result["Content"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in result["Content"].AsBsonArray)
                        {
                            Debug.WriteLine(item);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Show add panel");

                    }

                }
                catch (Exception)
                {

                    PageAUT.Placeholder.Children.Add(new AddContent(this));
                }



            });
        }
    }

    public interface IPageContent
    {
        void Init();
    }
}
