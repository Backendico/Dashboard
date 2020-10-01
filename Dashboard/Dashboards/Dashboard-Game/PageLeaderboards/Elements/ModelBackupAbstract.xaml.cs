using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for ModelBackupAbstract.xaml
    /// </summary>
    public partial class ModelBackupAbstract : UserControl
    {
        BsonDocument Detail;

        public ModelBackupAbstract(BsonDocument Detail, Action RefreshList)
        {
            Debug.WriteLine(Detail.ToString());
            InitializeComponent();
            this.Detail = Detail;
            this.RefreshList = RefreshList;

            TextVersion.Text = Detail["Name"].AsString;
            TextStart.Text = Detail["Start"].ToUniversalTime().ToString();
            TextEnd.Text = Detail["End"].ToUniversalTime().ToString();

        }

        private void PointerEnter(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gainsboro);
            BTNDownload.Visibility = Visibility.Visible;
        }

        private void PointerExit(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.WhiteSmoke);
            BTNDownload.Visibility = Visibility.Collapsed;
        }


        private void RemoveBackup(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deleted data is not recoverable", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.RemoveBackup(Detail["NameLeaderboard"].AsString, Detail["Name"].AsString,
                    () =>
                    {
                        MessageBox.Show("Deleted !");
                        RefreshList();
                    },
                    () =>
                    {
                        MessageBox.Show("Not delete :(");
                    });
            }
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            new ModelDownloadBackup(Detail["NameLeaderboard"].ToString(), Detail["Name"].ToString()).Show();
        }


        Action RefreshList;


    }


}
