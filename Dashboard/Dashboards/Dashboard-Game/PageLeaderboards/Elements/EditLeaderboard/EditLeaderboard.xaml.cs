using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PageLeaderboards.Elements
{
    public partial class EditLeaderboard : UserControl
    {

        public EditLeaderboard(IEditorLeaderboard Editor)
        {
            InitializeComponent();

            #region Page Setting

            TextToken.Text = Editor.DetailLeaderboard["Settings"]["Token"].AsObjectId.ToString();
            TextStart.Text = DateTime.Parse(Editor.DetailLeaderboard["Settings"]["Start"].ToString()).ToString();
            TextLeaderboardName.Text = Editor.DetailLeaderboard["Settings"]["Name"].AsString;
            TextName_Setting.Text = Editor.DetailLeaderboard["Settings"]["Name"].AsString;
            TextMinValue.Text = Editor.DetailLeaderboard["Settings"]["Min"].ToString();
            TextMaxValue.Text = Editor.DetailLeaderboard["Settings"]["Max"].ToString();

            ComboboxReset.SelectedIndex = Editor.DetailLeaderboard["Settings"]["Reset"].ToInt32();
            TextAmount.Text = Editor.DetailLeaderboard["Settings"]["Amount"].ToString();

            ComboboxSort.SelectedIndex = Editor.DetailLeaderboard["Settings"]["Sort"].ToInt32();


            PanelAmount.Loaded += (s, e) =>
            {
                if (Editor.DetailLeaderboard["Settings"]["Reset"].ToInt32() == 0)
                {
                    PanelAmount.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelAmount.Visibility = Visibility.Visible;
                }

            };


            //copy token
            TextToken.MouseDown += GlobalEvents.CopyText;

            //Control and Deploy amount
            TextAmount.LostFocus += (s, e) =>
            {
                try
                {
                    Editor.DetailLeaderboard["Settings"]["Amount"] = int.Parse(TextAmount.Text);
                    Editor.Save();
                }
                catch (Exception ex)
                {
                    Editor.DetailLeaderboard["Settings"]["Amount"] = 1;
                    TextAmount.Text = "1";
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                    Editor.Save();
                }
            };

            //change Minvalue
            TextMinValue.LostFocus += (s, e) =>
            {
                try
                {
                    Editor.DetailLeaderboard["Settings"]["Min"] = Int64.Parse(TextMinValue.Text);
                    Editor.Save();
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                    TextMinValue.Text = Editor.DetailLeaderboard["Settings"]["Min"].ToString();
                }
            };

            //change Max value
            TextMaxValue.LostFocus += (s, e) =>
            {
                try
                {
                    Editor.DetailLeaderboard["Settings"]["Max"] = Int64.Parse(TextMaxValue.Text);
                    Editor.Save();
                }
                catch (Exception ex)
                {

                    TextMaxValue.Text = Editor.DetailLeaderboard["Settings"]["Max"].ToString();
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }

            };


            //change reset
            ComboboxReset.SelectionChanged += (s, e) =>
            {
                Editor.DetailLeaderboard["Settings"]["Reset"] = ComboboxReset.SelectedIndex;

                if (Editor.DetailLeaderboard["Settings"]["Reset"].ToInt32() == 0)
                {
                    PanelAmount.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PanelAmount.Visibility = Visibility.Visible;
                }
                Editor.Save();
            };

            //change sort
            ComboboxSort.SelectionChanged += (s, e) =>
            {
                Editor.DetailLeaderboard["Settings"]["Sort"] = ComboboxSort.SelectedIndex;
                Editor.Save();
            };

            #endregion

            #region Page Leaderboard

            var Count = 100;
            //action btn recive leaderboards
            BTNLeaderboard.Click += (s, e) =>
            {
                Count = 100;
                ReciveLeaderboardDetail();
                TextSeeMore.Text = Count.ToString();
            };

            //action btn see more
            BTNSeeMore.MouseDown += (s, e) =>
            {
                Count += 100;
                ReciveLeaderboardDetail();
                TextSeeMore.Text = Count.ToString();
            };

            //action btn Backups
            BTNBackup.MouseDown += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Backup(Editor.DetailLeaderboard["Settings"]["Name"].ToString(), Result =>
                {
                    Debug.WriteLine(Result);
                });

            };

            //action  btn reset leaderboard
            BTNReset.MouseDown += async (s, e) =>
            {
                if (await DashboardGame.DialogYesNo("All information is lost.\n Are you sure ? ") == MessageBoxResult.Yes)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reset(Editor.DetailLeaderboard["Settings"]["Name"].ToString(), result =>
                    {
                        if (result)
                        {
                            DashboardGame.Notifaction("Leaderboard Reset", Notifaction.StatusMessage.Ok);
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild reset", Notifaction.StatusMessage.Warrning);
                        }
                    });

                }
                else
                {
                    DashboardGame.Notifaction("Canceled", Notifaction.StatusMessage.Warrning);
                }
            };

            //action btn Show panel add 
            BTNShowPanelAdd.MouseDown += (s, e) =>
            {
                ShowPanelAddPlayer();
            };

            //show off panel add
            PanelAddPlayer.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPaneladdPlayer();
                }
            };

            //action btn add player
            BTNAddPlayer.MouseDown += (s, e) =>
            {
                try
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.AddPlayer(Editor.DetailLeaderboard["Settings"]["Name"].ToString(), ObjectId.Parse(TextboxTokenPlayer.Text), long.Parse(TextboxValue.Text),
                        result =>
                        {
                            if (result)
                            {
                                DashboardGame.Notifaction("Player Add", Notifaction.StatusMessage.Ok);
                                ReciveLeaderboardDetail();
                                ShowoffPaneladdPlayer();
                                Editor.DetailLeaderboard["Settings"]["Count"] = Editor.DetailLeaderboard["Settings"]["Count"].ToInt32() + 1;
                                Editor.Save();
                            }
                            else
                            {
                                DashboardGame.Notifaction("Faild add", Notifaction.StatusMessage.Error);
                            }
                        });
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }


            };


            void ReciveLeaderboardDetail()
            {
                ContentPlaceLeaderboard.Children.Clear();
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Leaderboard(Count, Editor.DetailLeaderboard["Settings"]["Name"].ToString(), result =>
                {
                    if (result.ElementCount >= 1)
                    {
                        var Conter = 0;
                        foreach (var item in result["List"].AsBsonArray)
                        {
                            item.AsBsonDocument.Add("Rank", Conter);
                            ContentPlaceLeaderboard.Children.Add(new ContentValue(item.AsBsonDocument, Editor));
                            Conter++;
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                        ShowPanelAddPlayer();
                    }
                });
            }


            #endregion

            #region Backups

            var CountBackups = 100;

            //btn backups
            BTNBackupHistory.Click += (s, e) =>
            {

                PlaceContentBackups.Children.Clear();
                SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.BackupRecive(Editor.DetailLeaderboard["Settings"]["Name"].ToString(), CountBackups, result =>
                 {
                     if (result.ElementCount >= 1)
                     {
                         foreach (var item in result["Leaderboards"].AsBsonArray)
                         {
                             PlaceContentBackups.Children.Add(new ModelBackupAbstract(item.AsBsonDocument));
                         }

                     }
                     else
                     {
                         DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                     }

                 });
            };

            #endregion
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
                        BTNBackupHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNLeaderboard.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    }
                    break;
                case "Leaderboard":
                    {
                        ContentLeaderboard.Visibility = Visibility.Visible;
                        ContentSetting.Visibility = Visibility.Collapsed;
                        ContentHistory.Visibility = Visibility.Collapsed;

                        BTNSetting.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        BTNBackupHistory.BorderBrush = new SolidColorBrush(Colors.Transparent);
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
                        BTNBackupHistory.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    }
                    break;

                default:
                    Debug.WriteLine("not set");
                    break;
            }
            ShowoffPaneladdPlayer();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }


        private void ShowPanelAddPlayer()
        {
            PanelAddPlayer.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        private void ShowoffPaneladdPlayer()
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


    }


}
