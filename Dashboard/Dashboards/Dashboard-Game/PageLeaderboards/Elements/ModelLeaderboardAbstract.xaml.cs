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

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for ModelLeaderboardAbstract.xaml
    /// </summary>
    public partial class ModelLeaderboardAbstract : UserControl
    {
        MongoDB.Bson.BsonDocument Detail;

        public ModelLeaderboardAbstract(MongoDB.Bson.BsonDocument Detail, Action<object, RoutedEventArgs> Refreshlist)
        {
            InitializeComponent();
            this.Detail = Detail;
            this.Refreshlist = Refreshlist;

            TextName.Text = Detail["Name"].AsString;
            TextToken.Text = Detail["Token"].AsObjectId.ToString();
            TextPlayers.Text = Detail["Count"].ToString();
            //TextHistory.Text = Detail["Setting"]["History"].ToString();

            switch (Detail["Reset"].AsInt32)
            {
                case 0:
                    {
                        TextReset.Text = "Manually";
                    }
                    break;
                case 1:
                    {
                        TextReset.Text = "Hourly";
                    }
                    break;
                case 2:
                    {
                        TextReset.Text = "Daily";
                    }
                    break;
                case 3:
                    {
                        TextReset.Text = "Weekly";
                    }
                    break;
                case 4:
                    {
                        TextReset.Text = "monthly";
                    }
                    break;
                default:
                    TextReset.Text = "NotCategory";
                    break;
            }

            switch (Detail["Sort"].AsInt32)
            {
                case 0:
                    {
                        TextSort.Text = "Last";
                    }
                    break;
                case 1:
                    {
                        TextSort.Text = "Minimum";
                    }
                    break;
                case 2:
                    {
                        TextSort.Text = "Maximum";
                    }
                    break;
                case 3:
                    {
                        TextSort.Text = "Sum";
                    }
                    break;

                default:
                    TextReset.Text = "NotCategory";
                    break;
            }

        }

        private void PointerEnter(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gainsboro);
            BTNEdit.Visibility = Visibility.Visible;

        }
        private void PointerExit(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.WhiteSmoke);
            BTNEdit.Visibility = Visibility.Collapsed;
        }

        private void CopyToken(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText((sender as TextBlock).Text);

            MessageBox.Show("Token Leadeboard Copyed !");
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            DashboardGame.MainRoot.Children.Add(new EditLeaderboard(Detail, Refreshlist));
        }

        Action<object, RoutedEventArgs> Refreshlist;
    }
}
