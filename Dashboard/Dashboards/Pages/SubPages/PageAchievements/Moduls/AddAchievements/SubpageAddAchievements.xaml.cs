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

namespace Dashboard.Dashboards.Pages.SubPages.PageAchievements.Moduls.AddAchievements
{
    public partial class SubpageAddAchievements : UserControl
    {
        public SubpageAddAchievements(IPageAchievements Editor)
        {
            InitializeComponent();
            ShowPage(1);

            //action btn close
            BTNClose.MouseDown += (s, e) =>
            {
                ShowPage(0, () =>
                {
                    Visibility = Visibility.Collapsed;
                });
            };

            //action btn add leaderboard
            BTNAddAchievement.Worker += () =>
            {
                try
                {
                    if (TextName.Text.Length >= 1 && long.Parse(TextValue.Text) >= 0)
                    {

                        SDK.SDK_PageDashboards.DashboardGame.PageAchievements.AddAchievements(TextName.Text, long.Parse(TextValue.Text), result =>
                        {
                            if (result)
                            {
                                Debug.WriteLine("Created");
                                ShowPage(0, () =>
                                {
                                    Editor.Init();
                                    Visibility = Visibility.Collapsed;
                                });
                            }

                        });
                    }
                    else
                    {

                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("Faild Add achievements");
                }

            };

        }

        void ShowPage(int TO, Action Act = null)
        {
            var Current = 0;

            if (TO != 0)
            {
                Current = 0;
            }
            else
            {
                Current = 1;
            }

            DoubleAnimation Anim = new DoubleAnimation(Current, TO, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                if (Act != null)
                {
                    Act();
                }
            };
            Storyboard.SetTargetName(Anim, Root.Name);
            Storyboard.SetTargetProperty(Anim, new System.Windows.PropertyPath("Opacity"));
            Storyboard story = new Storyboard();
            story.Children.Add(Anim);
            story.Begin(this);
        }
    }
}
