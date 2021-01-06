using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{

    public partial class ModelDownloadBackup : System.Windows.Controls.UserControl
    {
        FolderBrowserDialog Path = new FolderBrowserDialog { Description = "Select Folder for Save" };

        public ModelDownloadBackup(BsonDocument Detail)
        {
            InitializeComponent();


            //close
            Root.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    Close();
                }
            };

            //btn select folder
            BTNSelectFolder.MouseDown += (s, e) =>
            {

                Path.ShowDialog();

                TextboxPath.Text = Path.SelectedPath;
            };

            BTNSave.MouseDown += (s, e) =>
            {
                DashboardGame.Notifaction("Download Started  . . .", Notifaction.StatusMessage.Ok);

                if (Detail.ElementCount >= 1)
                {
                    try
                    {
                        File.WriteAllText(Path.SelectedPath + $"/{Detail["Settings"]["Token"].ToString()}.json", Detail.ToString());

                        DashboardGame.Notifaction("Download Complited!\n Path: \n" + Path.SelectedPath, Notifaction.StatusMessage.Ok);
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
            };
        }

        private void Close()
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


