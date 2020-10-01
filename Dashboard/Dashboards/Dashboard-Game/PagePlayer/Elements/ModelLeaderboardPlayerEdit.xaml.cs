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

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    /// <summary>
    /// Interaction logic for ModelLeaderboardPlayerEdit.xaml
    /// </summary>
    public partial class ModelLeaderboardPlayerEdit : UserControl
    {
        public ModelLeaderboardPlayerEdit(BsonDocument TotalLeaderboard, BsonDocument LeaderboardPlayer, string NameLeaderboard, Int64 Value)
        {
            InitializeComponent();

            //combobox init
            foreach (var item in TotalLeaderboard)
            {
                ComboBoxName.Items.Add(item.Name);
            }

            ComboBoxName.SelectedValue = NameLeaderboard;
            ComboBoxName.IsEnabled = false;
            TextValue.Text = Value.ToString();
            TextValue.TextChanged += (objc, e) =>
            {
                if (long.TryParse((objc as TextBox).Text, out _))
                {
                    LeaderboardPlayer[ComboBoxName.Text] = new BsonInt64(long.Parse(TextValue.Text));
                    Background = new SolidColorBrush(Colors.Orange);
                }
                else
                {
                    (objc as TextBox).Text = LeaderboardPlayer[ComboBoxName.Text].ToString();
                }
            };
        }
    }
}
