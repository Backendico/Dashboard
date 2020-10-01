using MongoDB.Bson;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageLogs.Elements
{
    /// <summary>
    /// Interaction logic for ModelLogDashboard.xaml
    /// </summary>
    public partial class ModelLogDashboard : UserControl
    {
        public ModelLogDashboard(BsonDocument DetailReport)
        {
            InitializeComponent();

            TextCreated.Text = DetailReport["Created"].ToUniversalTime().ToString();
            TextID.Text = DetailReport["ID"].AsString;


            switch (DetailReport["Category"].AsInt32)
            {
                case 2:
                    {
                        TextCategory.Text = "Page Leaderboard";

                        switch (DetailReport["Action"].AsInt32)
                        {
                            case 0:
                                TextAction.Text = "New Leaderboard";
                                break;
                            case 1:
                                TextAction.Text = "Edit Setting ";
                                break;
                            case 2:
                                TextAction.Text = "Add Player to Leaderboard ";
                                break;
                            case 3:
                                TextAction.Text = "Remove Player from Leaderboard ";
                                break;
                            case 4:
                                TextAction.Text = "Reset Leaderboard ";
                                break;
                            case 5:
                                TextAction.Text = "Make Backup";
                                break;
                            case 6:
                                TextAction.Text = "Remove Backup";
                                break;
                            case 7:
                                TextAction.Text = "Download Backup";
                                break;
                        }
                    }
                    break;
                case 3:
                    {
                        TextCategory.Text = "Page Player";

                        switch (DetailReport["Action"].AsInt32)
                        {
                            case 0:
                                TextAction.Text = "Add Player";
                                break;
                            case 1:
                                TextAction.Text = "Delete Player";
                                break;
                            case 2:
                                TextAction.Text = "Edit Setting Player";
                                break;
                        }
                    }
                    break;
                default:
                    Debug.WriteLine("Notset");
                    break;
            }

            switch (DetailReport["Method"].AsInt32)
            {
                case 0:
                    TextMethod.Text = "Get";
                    TextMethod.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case 1:
                    TextMethod.Text = "Put";
                    TextMethod.Foreground = new SolidColorBrush(Colors.Blue);
                    break;
                case 2:
                    TextMethod.Text = "Delete";
                    TextMethod.Foreground = new SolidColorBrush(Colors.Tomato);
                    break;
                case 3:
                    TextMethod.Text = "Post";
                    TextMethod.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case 4:
                    TextMethod.Text = "Head";
                    TextMethod.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case 5:
                    TextMethod.Text = "Trace";
                    break;
                case 6:
                    TextMethod.Text = "Patch";
                    break;
                case 7:
                    TextMethod.Text = "Connect";
                    break;
                case 8:
                    TextMethod.Text = "Options";
                    break;
                case 9:
                    TextMethod.Text = "Custom";
                    break;
            }

            TextResult.Text = DetailReport["Result"].ToString();
        }
    }
}
