using Dashboard.GlobalElement;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for ModelLeaderboardAbstract.xaml
    /// </summary>
    public partial class ModelLeaderboardAbstract : UserControl
    {

        public ModelLeaderboardAbstract(MongoDB.Bson.BsonDocument Detail, Action<object, RoutedEventArgs> Refreshlist)
        {
            InitializeComponent();
            this.Refreshlist = Refreshlist;

            TextName.Text = Detail["Name"].AsString;
            TextToken.Text = Detail["Token"].AsObjectId.ToString();
            TextPlayers.Text = Detail["Count"].ToString();

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

            BTNEdit.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new EditLeaderboard(Detail, Refreshlist));
            };

            TextToken.MouseDown += GlobalEvents.CopyText;
        }


      



        Action<object, RoutedEventArgs> Refreshlist;
    }
}
