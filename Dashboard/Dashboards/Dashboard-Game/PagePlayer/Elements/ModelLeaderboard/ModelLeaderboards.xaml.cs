using Dashboard.GlobalElement;
using MongoDB.Bson;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLeaderboard
{
    /// <summary>
    /// Interaction logic for ModelLeaderboards.xaml
    /// </summary>
    public partial class ModelLeaderboards : UserControl
    {
        public ModelLeaderboards(BsonDocument Detail ,IEditLeaderboard Editor)
        {
            InitializeComponent();
            TextLeaderboard.Text = Detail["Settings"]["Name"].ToString();

            TextScore.TextChanged += (sk, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextScore))
                {


                }
            };

            BTNAdd.MouseDown += (s, e) =>
            {
                Editor.AddLeaderbord(new BsonDocument());
                (Parent as StackPanel).Children.Remove(this);

            };

        }
    }
}
