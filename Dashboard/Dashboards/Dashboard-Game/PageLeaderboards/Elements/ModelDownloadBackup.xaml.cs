using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.IO;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{

    public partial class ModelDownloadBackup : System.Windows.Controls.UserControl
    {
        FolderBrowserDialog Path = new FolderBrowserDialog { Description = "Select Folder for Save" };
        string NameLeaderboard;
        string Version;

        public ModelDownloadBackup(string NameLeaderboard, string Version)
        {
            InitializeComponent();
            this.NameLeaderboard = NameLeaderboard;
            this.Version = Version;

            Root.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    Close(null, null);
                }

            };
        }

        private void SelectFolder(object sender, MouseButtonEventArgs e)
        {
            Path.ShowDialog();

            TextboxPath.Text = Path.SelectedPath;


        }

        private void Download(object sender, MouseButtonEventArgs e)
        {

            DashboardGame.Notifaction("Download Started  . . .", Notifaction.StatusMessage.Ok);

            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.DownloadBackup(NameLeaderboard, Version,
                result =>
                {
                    if (result.ElementCount >= 1)
                    {
                        try
                        {
                            File.WriteAllText(Path.SelectedPath + $"/{NameLeaderboard + "-" + Version}.json", result.ToString());
                            DashboardGame.Notifaction("Donload Complited!\n Path: \n" + Path.SelectedPath, Notifaction.StatusMessage.Ok);
                            Process.Start(Path.SelectedPath);
                        }
                        catch (Exception)
                        {
                            DashboardGame.Notifaction("Select Folder", Notifaction.StatusMessage.Warrning);
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                    }

                },
                () =>
                {
                });
        }



        private void Close(object sender, MouseButtonEventArgs e)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation Anim1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim1, Root.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("Opacity"));
            storyboard.Children.Add(Anim1);


            ThicknessAnimation Anim2 = new ThicknessAnimation(fromValue: new Thickness(0, 20, 0, 0), toValue: new Thickness(0, -200, 0, 0), TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim2, Content.Name);
            Storyboard.SetTargetProperty(Anim2, new PropertyPath("Margin"));
            storyboard.Children.Add(Anim2);


            storyboard.Completed += (s, ee) =>
            {
                DashboardGame.Dashboard.Root.Children.Remove(this);
            };


            storyboard.Begin(this);

        }

    }
}
