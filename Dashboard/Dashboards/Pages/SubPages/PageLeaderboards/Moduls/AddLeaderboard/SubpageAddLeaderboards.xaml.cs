using Dashboard.GlobalElement;
using System;
using System.Collections.Generic;
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

namespace Dashboard.Dashboards.Pages.SubPages.PageLeaderboards.Moduls.AddLeaderboard
{
    /// <summary>
    /// Interaction logic for SubpageAddLeaderboards.xaml
    /// </summary>
    public partial class SubpageAddLeaderboards : UserControl
    {
        public SubpageAddLeaderboards()
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
            BTNAddLeaderboard.Worker += () =>
            {
                long Reset = 0;
                long Amount = 0;

                switch (ComboReset.TextSelected.Text)
                {
                    default:
                        break;
                }


                //SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Creat(TextName.Text,Reset,);

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
