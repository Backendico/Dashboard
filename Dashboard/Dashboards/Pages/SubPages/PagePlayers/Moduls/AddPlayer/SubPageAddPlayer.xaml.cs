using Dashboard.Dashboards.Pages.Main;
using Dashboard.GlobalElement;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.AddPlayer
{

    public partial class SubPageAddPlayer : UserControl
    {
        public SubPageAddPlayer()
        {
            InitializeComponent();

            ShowPage(1);

            //action btn close
            BTNClose.MouseDown += (s, e) =>
            {
                ShowPage(0, () =>
                {
                    Visibility = System.Windows.Visibility.Collapsed;
                });
            };

            //action send request for creat player
            BTNAddPlayer.Worker += () =>
                        {
                            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.CreatPlayer(InputUsername.Text, InputPassword.Text,
                            result =>
                            {
                                if (result)
                                {
                                    Debug.WriteLine("Created");
                                }
                                else
                                {
                                    Debug.WriteLine("Err Created");
                                }
                            });
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
