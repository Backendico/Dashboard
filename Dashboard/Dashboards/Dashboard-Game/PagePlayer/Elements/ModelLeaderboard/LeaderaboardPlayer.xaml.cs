using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLeaderboard
{
    /// <summary>
    /// Interaction logic for LeaderaboardPlayer.xaml
    /// </summary>
    public partial class LeaderaboardPlayer : UserControl
    {
        public LeaderaboardPlayer(string NameLeaderboard, long Score)
        {
            InitializeComponent();
            TextLeaderboard.Text = NameLeaderboard;
            TextScore.Text = Score.ToString();
        }

    }
}
