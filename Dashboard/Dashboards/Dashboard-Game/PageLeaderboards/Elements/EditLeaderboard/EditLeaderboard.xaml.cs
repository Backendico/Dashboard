using Dashboard.GlobalElement;
using RestSharp;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MongoDB.Bson;
using System;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Security.Cryptography.X509Certificates;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    /// <summary>
    /// Interaction logic for EditLeaderboard.xaml
    /// </summary>
    public partial class EditLeaderboard : UserControl
    {
        BsonDocument Detail;
        int Count;

        public EditLeaderboard(BsonDocument Detail, Action<object, RoutedEventArgs> RefreshList)
        {
            InitializeComponent();

            this.Detail = Detail;
            this.RefreshList = RefreshList;

            TextLeaderboardName.Text = Detail["Name"].AsString;
            TextToken.Text = Detail["Token"].AsObjectId.ToString();
            TextName_Setting.Text = Detail["Name"].AsString;
            TextStart.Text = Detail["Start"].ToUniversalTime().ToString();

            ComboboxReset.SelectedIndex = Detail["Reset"].AsInt32;
            ComboboxSort.SelectedIndex = Detail["Sort"].AsInt32;

            PanelAddPlayer.MouseDown += (s, e) =>
            {

                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPaneladdPlayer(null, null);
                }
            };

        }

        //global method
        private void ChangePage(object sender, RoutedEventArgs e)
        {
            var BTN = sender as Button;

            switch (BTN.Content)
            {
                case "Setting":
                    {
                        ContentLeaderboard.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Collapsed;
                        ContentSetting.Visibility = Visibility.Visible;


                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    }
                    break;
                case "Leaderboard":
                    {
                        ContentLeaderboard.Visibility = Visibility.Visible;
                        ContentSetting.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Collapsed;

                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.DarkOrange);

                    }
                    break;
                case "Backups":
                    {
                        ContentLeaderboard.Visibility = Visibility.Collapsed;
                        ContentSetting.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Visible;


                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNHistory.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    }
                    break;

                default:
                    Debug.WriteLine("not set");
                    break;
            }

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }


        //page1 
        private void Save(object sender, MouseButtonEventArgs e)
        {
            Detail["Reset"] = ComboboxReset.SelectedIndex;
            Detail["Sort"] = ComboboxSort.SelectedIndex;
            Detail.Remove("Count");

            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.EditLeaderboard(Detail, result =>
             {
                 if (result)
                 {
                     DashboardGame.Notifaction("Saved !", Notifaction.StatusMessage.Ok);
                     RefreshList(null, null);


                     //log
                     SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Edit Leaderboard", $"You have changed the \" {Detail["Name"]} \" leaderboard settings", Detail, false, resultlog => { });
                 }
                 else
                 {

                     DashboardGame.Notifaction("Faild Save !", Notifaction.StatusMessage.Error);
                 }

             });

        }
        private void CopyToken(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText((sender as TextBlock).Text);
            DashboardGame.Notifaction("Token Copied !", Notifaction.StatusMessage.Ok);
        }



        //page2
        private void SeeMore(object sender, MouseButtonEventArgs e)
        {
            Count += 100;
            TextSeeMore.Text = Count.ToString();
            ReciveLeaderboards();
        }


        private async void Reset(object sender, MouseButtonEventArgs e)
        {
            var Result = await DashboardGame.DialogYesNo("All records are lost\n Do you want to continue?");

            if (Result == MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reset(Detail["Name"].AsString,
                    () =>
                    {
                        DashboardGame.Notifaction("Reseted !", Notifaction.StatusMessage.Ok);
                        ReciveLeaderboards();
                        TextStart.Text = DateTime.Now.ToString();

                        //log
                        SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Reset the leaderboard", $"You reset the \" {Detail["Name"]} \" leaderboard", new BsonDocument { }, false, result => { });
                    },
                    () =>
                    {
                        DashboardGame.Notifaction("Reset Faild !", Notifaction.StatusMessage.Error);
                    });
            }
            else
            {
                DashboardGame.Notifaction("Reset Reject !", Notifaction.StatusMessage.Error);
            }

        }

        private void Backup(object sender, MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Backup(Detail["Name"].AsString,
                () =>
                {
                    DashboardGame.Notifaction("Saved !", Notifaction.StatusMessage.Ok);


                    //log
                    SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Leaderboard Backup", $"You have backed up your \"{Detail["Name"]}\" leadboard", new BsonDocument { }, false, result => { });

                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Save !", Notifaction.StatusMessage.Error);
                });

        }

        private void VisibilityChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch ((sender as Grid).Name)
            {
                case "ContentHistory" when (sender as Grid).Visibility == Visibility.Visible:
                    {
                        ReciveBackup();
                    }
                    break;
                case "ContentLeaderboard" when (sender as Grid).Visibility == Visibility.Visible:
                    {
                        Count = 100;
                        ReciveLeaderboards();
                    }
                    break;

            }

        }

        public void ReciveLeaderboards()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Leaderboard(Count, Detail["Name"].ToString(),
                Result =>
                {
                    var Place = (ContentPlaceLeaderboard.Content = new StackPanel()) as StackPanel;

                    foreach (var item in Result.AsBsonDocument)
                    {
                        Place.Children.Add(new ContentValue(item.Value.AsBsonDocument.Add("NameLeaderboard", Detail["Name"]), ReciveLeaderboards));
                    }
                },
                () =>
                {
                });

        }

        public void ReciveBackup()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.BackupRecive(Detail["Name"].AsString,
                Result =>
                {
                    PlaceContentBackups.Children.Clear();

                    foreach (var item in Result)
                    {
                        item.Value.AsBsonDocument.Add("NameLeaderboard", Detail["Name"]);
                        PlaceContentBackups.Children.Add(new ModelBackupAbstract(item.Value.AsBsonDocument, ReciveBackup));
                    }
                },
                () =>
                {

                });
        }

        private void ShowPanelAddPlayer(object sender, MouseButtonEventArgs e)
        {
            PanelAddPlayer.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }
        private void ShowoffPaneladdPlayer(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                TextboxTokenPlayer.Text = "";
                TextboxValue.Text = "";

                PanelAddPlayer.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        private void AddPlayer(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _ = int.Parse(TextboxValue.Text);
                _ = ObjectId.Parse(TextboxTokenPlayer.Text);

                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.AddPlayer(Detail["Name"].ToString(), TextboxTokenPlayer.Text, int.Parse(TextboxValue.Text),
                  () =>
                  {
                      DashboardGame.Notifaction("Added ! ", Notifaction.StatusMessage.Ok);
                      ShowoffPaneladdPlayer(null, null);
                      ReciveLeaderboards();

                      //Addlog
                      var detaillog = new BsonDocument { { "NameLeaderboard", Detail["Name"] }, { "TokenPlayer", TextboxTokenPlayer.Text }, { "Value", TextboxValue.Text } };
                      SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Add player to leaderboard", $"You have added player \" {TextboxTokenPlayer.Text} \" to the \" {Detail["Name"]} \" leaderboard", detaillog, false, resultlog => { });
                  },
                  () =>
                  {
                      DashboardGame.Notifaction("Faild Add ! ", Notifaction.StatusMessage.Error);
                  });

            }
            catch (Exception ex)
            {
                TextboxValue.Text = "";
                TextboxTokenPlayer.Text = "";
                DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
            }
        }
       
        Action<object, RoutedEventArgs> RefreshList;

    }
}
