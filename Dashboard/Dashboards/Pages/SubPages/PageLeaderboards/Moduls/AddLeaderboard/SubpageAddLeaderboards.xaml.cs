using Dashboard.GlobalElement;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
                long Sort = 0;
                long Amount = 0;
                long Min = 0;
                long Max = 0;

                //Reset
                switch (ComboReset.TextSelected.Text)
                {
                    case "Manually":
                        {
                            Reset = 0;
                        }
                        break;
                    case "Hourly":
                        {
                            Reset = 1;
                        }
                        break;
                    case "Daily":
                        {
                            Reset = 2;
                        }
                        break;
                    case "Weekly":
                        {
                            Reset = 3;
                        }
                        break;
                    case "Monthly":
                        {
                            Reset = 4;
                        }
                        break;
                    default:
                        Reset = 0;
                        break;
                }


                //sort
                switch (ComboboxSort.TextSelected.Text)
                {
                    case "Last":
                        Sort = 0;
                        break;
                    case "Minimum":
                        Sort = 1;
                        break;
                    case "Maximum":
                        Sort = 2;
                        break;
                    case "Sum":
                        Sort = 3;
                        break;
                    default:
                        Sort = 0;
                        break;
                }


                //amount
                try
                {
                    Amount = long.Parse(TextAmount.Text);
                    Min = long.Parse(TextMinimum.Text);
                    Max = long.Parse(TextMaximum.Text);


                    if (TextName.Text != TextName.PlaceHolder)
                    {
                        SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Creat(TextName.Text, Reset, Amount, Sort, Min, Max, Result =>
                        {
                            if (Result)
                            {
                                ShowPage(0);
                            }
                            else
                            {
                                Debug.WriteLine("Faild add");
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

                    Debug.WriteLine("Error to created");
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
