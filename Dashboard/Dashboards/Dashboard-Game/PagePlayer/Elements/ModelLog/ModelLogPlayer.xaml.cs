using MongoDB.Bson;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLog
{
    /// <summary>
    /// Interaction logic for ModelLogPlayer.xaml
    /// </summary>
    public partial class ModelLogPlayer : UserControl
    {
        public ModelLogPlayer(BsonDocument LogDetail)
        {
            InitializeComponent();

            TextHeader.Text = LogDetail["Header"].AsString;
            TextDescription.Text = LogDetail["Description"].AsString;
            TextTime.Text = LogDetail["Time"].ToLocalTime().ToString();
        }
    }
}
