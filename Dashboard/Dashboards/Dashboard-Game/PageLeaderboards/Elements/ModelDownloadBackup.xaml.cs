using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{

    public partial class ModelDownloadBackup : Window
    {
        FolderBrowserDialog Path = new FolderBrowserDialog { Description = "Select Folder for Save" };
        string NameLeaderboard;
        string Version;

        public ModelDownloadBackup(string NameLeaderboard, string Version)
        {
            InitializeComponent();
            this.NameLeaderboard = NameLeaderboard;
            this.Version = Version;
        }

        private void SelectFolder(object sender, RoutedEventArgs e)
        {
            Path.ShowDialog();

            TextboxPath.Text = Path.SelectedPath;


        }

        private void Download(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.MessageBox.Show("Download Started  . . .");

            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.DownloadBackup(NameLeaderboard, Version,
                result =>
                {
                    File.WriteAllText(Path.SelectedPath + $"/{NameLeaderboard + "-" + Version}.json", result.ToString());
                    System.Windows.Forms.MessageBox.Show("Donload Complited!\n Path: \n" + Path.SelectedPath);

                    Process.Start(Path.SelectedPath);

                },
                () => { });
        }


        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
