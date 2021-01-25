using Dashboard.GlobalElement;
using MongoDB.Bson;
using System.Diagnostics;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLeaderboard
{
    public partial class ModelLeaderboards : UserControl
    {
        public ModelLeaderboards(BsonDocument Detail, IEditLeaderboard Editor)
        {
            InitializeComponent();
            TextLeaderboard.Text = Detail["Settings"]["Name"].ToString();


            //action Textbox Score
            TextScore.TextChanged += (sk, e) =>
            {
                if (!GlobalEvents.ControllNumberFilde(TextScore))
                {
                    TextScore.Text = 0.ToString();
                }
            };

            //Action btn add
            BTNAdd.MouseDown += (s, e) =>
            {
                Editor.AddLeaderbord(Detail["Settings"]["Name"].ToString(), long.Parse(TextScore.Text));
                Editor.InitLeaderboards();
                Debug.WriteLine(Parent);
            };

        }
    }
}
