using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Pages.SubPages.PageLeaderboards.Moduls.Leaderboard_ListView
{
    /// <summary>
    /// Interaction logic for LeaderboardListView.xaml
    /// </summary>
    public partial class LeaderboardListView : UserControl, IListViewLeaderboard
    {
        BsonDocument Detail;
        bool IsSeemoreOpen = false;
        public LeaderboardListView(BsonDocument Detail)
        {
            InitializeComponent();
            this.Detail = Detail;
            Panel.SetZIndex(this, this.Detail["Zindex"].ToInt32());

            Init();


            BTNSeemore.MouseDown += (s, e) =>
            {
                if (IsSeemoreOpen)
                {
                    CloseMore();
                }
                else
                {
                    OpenMore();
                }

            };


        }
        void OpenMore()
        {
            IsSeemoreOpen = true;
            PanelMore.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3f));
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void CloseMore()
        {
            IsSeemoreOpen = false;
            DoubleAnimation Anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3f));
            Anim.Completed += (s, e) =>
            {
                PanelMore.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetName(Anim, PanelMore.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        public void Init()
        {
            //name
            TextName.Text = Detail["Name"].ToString();

            //sort
            switch ((SortLeaderboard)Detail["Sort"].ToInt32())
            {
                case SortLeaderboard.Last:
                    TextSort.Text = SortLeaderboard.Last.ToString();
                    break;
                case SortLeaderboard.Minimum:
                    TextSort.Text = SortLeaderboard.Minimum.ToString();
                    break;
                case SortLeaderboard.Maxmimum:
                    TextSort.Text = SortLeaderboard.Maxmimum.ToString();
                    break;
                case SortLeaderboard.Sum:
                    TextSort.Text = SortLeaderboard.Sum.ToString();
                    break;
                default:
                    TextSort.Text = "N/A";
                    break;
            }

            //Reset
            switch ((ResetLeaderboard)Detail["Reset"].ToInt32())
            {
                case ResetLeaderboard.Manually:
                    TextReset.Text = ResetLeaderboard.Manually.ToString();
                    break;
                case ResetLeaderboard.Hourly:
                    TextReset.Text = ResetLeaderboard.Hourly.ToString();
                    break;
                case ResetLeaderboard.Daily:
                    TextReset.Text = ResetLeaderboard.Daily.ToString();
                    break;
                case ResetLeaderboard.Weekly:
                    TextReset.Text = ResetLeaderboard.Weekly.ToString();
                    break;
                case ResetLeaderboard.Monthly:
                    TextReset.Text = ResetLeaderboard.Monthly.ToString();
                    break;
                default:
                    TextReset.Text = "N/A";
                    break;
            }


            //text minimum 
            TextMinimum.Text = Detail["Min"].ToString();

            //Max 
            TextMaximum.Text = Detail["Max"].ToString();

            //Amount 
            TextAmount.Text = Detail["Amount"].ToString();

            //Count
            TextPlayerCount.Text = Detail["Count"].ToString();

            //Created
            TextStart.Text = Detail["Start"].ToLocalTime().ToString();

            //Token
            TextToken.Text = Detail["Token"].ToString();
        }
    }

    internal interface IListViewLeaderboard
    {
        void Init();

    }
}
