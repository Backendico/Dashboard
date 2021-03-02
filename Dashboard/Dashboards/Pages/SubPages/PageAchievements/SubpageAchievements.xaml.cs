using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.SubPages.PageAchievements.Components.Achievements_GridView;
using Dashboard.Dashboards.Pages.SubPages.PageAchievements.Components.Achievements_ListView;
using Dashboard.Dashboards.Pages.SubPages.PageAchievements.Moduls.AddAchievements;
using Dashboard.GlobalElement;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.SubPages.PageAchievements
{
    public partial class SubpageAchievements : UserControl, IPageAchievements
    {

        public SubpageAchievements()
        {
            InitializeComponent();

            Init();


            BTNaddAchievements.Work += () =>
            {
                PageAUT.Placeholder.Children.Add(new SubpageAddAchievements(this));
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
            ContentPlaceHolderGridviewe.Children.Clear();
            ContentPlaceholderListView.Children.Clear();

            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.ReciveAchievements(Result =>
            {
                if (Result.ElementCount >= 1)
                {
                    int Zindex = 0;
                    if (Result["Achievements"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in Result["Achievements"].AsBsonArray)
                        {
                            ContentPlaceHolderGridviewe.Children.Add(new AchievementsGridView(item.AsBsonDocument));
                            item.AsBsonDocument.Add("Zindex", Zindex);
                            ContentPlaceholderListView.Children.Add(new AchievementListView(item.AsBsonDocument));
                        }
                    }
                    else
                    {
                        PageAUT.Placeholder.Children.Add(new SubpageAddAchievements(this));
                    }

                }
                else
                {
                    Debug.WriteLine("No Achivements");
                }
            });
        }



    }
    public interface IPageAchievements
    {
        void Init();
    }
}
