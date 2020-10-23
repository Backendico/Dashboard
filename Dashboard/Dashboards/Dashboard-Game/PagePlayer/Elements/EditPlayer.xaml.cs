using Dashboard.Dashboards.Dashboard_Game.Elements.PagePlayer;
using Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLog;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using RestSharp;
using System;
using System.CodeDom;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    /// <summary>
    /// Interaction logic for EditPlayer.xaml
    /// </summary>
    public partial class EditPlayer : UserControl
    {

        BsonDocument PlayerDetail;
        BsonDocument TotalLeaderboard;



        Grid CurentPage;
        Button CurentBTNHeader;


        public EditPlayer(BsonDocument PlayerDetail, Action<object, RoutedEventArgs> RefreshList)
        {
            InitializeComponent();
            //frist init
            this.PlayerDetail = PlayerDetail;
            this.RefreshList = RefreshList;
            CurentPage = PageAccount;
            CurentBTNHeader = BTNAccount;


            //change frist
            TextIDPlayer_Header.Text = this.PlayerDetail["Account"]["Token"].AsObjectId.ToString();
            TextboxNickname.Text = this.PlayerDetail["Account"]["Name"].AsString;
            TextboxAvatar.Text = this.PlayerDetail["Account"]["Avatar"].AsString;
            TextLanguage.Text = this.PlayerDetail["Account"]["Language"].AsString;
            TextCreated.Text = this.PlayerDetail["Account"]["Created"].ToUniversalTime().ToString();
            TextCountry.Text = this.PlayerDetail["Account"]["Country"].AsString;
            TextboxUsername.Text = this.PlayerDetail["Account"]["Username"].AsString;
            TextboxEmail.Text = this.PlayerDetail["Account"]["Email"].AsString;
            TextToken.Text = this.PlayerDetail["Account"]["Token"].AsObjectId.ToString();

            //cheak banplayer
            CheackBoxBan.IsChecked = this.PlayerDetail["Account"]["IsBan"].AsBoolean;
            CheackBoxBan.Checked += (s, e) =>
            {
                this.PlayerDetail["Account"]["IsBan"] = true;
            };
            CheackBoxBan.Unchecked += (s, e) =>
            {
                this.PlayerDetail["Account"]["IsBan"] = false;
            };

            //init pageLeaderboard
            try
            {
                ReciveLeaderboardPlayer(PlayerDetail["Leaderboards"]["List"].AsBsonDocument);
            }
            catch (Exception)
            {
                PlayerDetail.Add("Leaderboards", new BsonDocument { { "List", new BsonDocument { } } });

                ReciveLeaderboardPlayer(PlayerDetail["Leaderboards"]["List"].AsBsonDocument);
            }



            //action btn Email Recovery
            BTNSendEmailRecovery.MouseDown += async (s, e) =>
            {

                if (await DashboardGame.DialogYesNo($"Do you want to send the recovery email to \"{PlayerDetail["Account"]["Token"].AsObjectId}\"?") == MessageBoxResult.Yes)
                {

                    SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SendEmailRecovery(PlayerDetail["Account"]["Token"].AsObjectId, result =>
                    {

                        if (result)
                        {
                            DashboardGame.Notifaction("Email Send", Notifaction.StatusMessage.Ok);

                            //add log
                            SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Send Email Recovery", $"You sent a recovery email for \"{PlayerDetail["Account"]["Token"]}\" ", new BsonDocument(), false, (ResultLog) => { });
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Send", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                else
                {

                    DashboardGame.Notifaction("Rejected", Notifaction.StatusMessage.Error);
                }

            };

            //action btgn see more
            BTNSeeMoreLog.MouseDown += (s, e) =>
            {
                CountLog += 100;
                TextSeeMoreNumber.Text = CountLog.ToString();
            };

            //actin btn Add Log
            BTNAddLog.MouseDown += (s, e) =>
            {
                ShowPanelAddLogs();
            };

            //action btn ClosePaneladdlog
            PanelAddLog.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPaneladdLogs();
                }
            };

            //action btn send log
            BTNSendLog.MouseDown += (s, e) =>
            {
                if (TextboxHeader.Text.Length >= 1 && TextboxDescription.Text.Length >= 1)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PagePlayers.AddLogPlayer(PlayerDetail["Account"]["Token"].AsObjectId, TextboxHeader.Text, TextboxDescription.Text, result =>
                    {
                        if (result)
                        {
                            DashboardGame.Notifaction("Log Added", Notifaction.StatusMessage.Ok);
                            ShowoffPaneladdLogs();
                            RecivePlayerLogs();
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Add", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                else
                {
                    DashboardGame.Notifaction("Header or Description Short", Notifaction.StatusMessage.Error);
                }
            };

            //action btn clear
            BTNClearLogs.MouseDown += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.ClearLog(PlayerDetail["Account"]["Token"].AsObjectId,
                    result =>
                    {
                        if (result)
                        {
                            PlaceContentLogs.Children.Clear();
                            DashboardGame.Notifaction("All Logs Deleted", Notifaction.StatusMessage.Ok);
                        }
                        else
                        {
                            DashboardGame.Notifaction("Clear faild", Notifaction.StatusMessage.Error);
                        }

                    });
            };

        }

        //global
        private void ChangePage(object sender, RoutedEventArgs e)
        {
            CurentPage.Visibility = Visibility.Collapsed;
            CurentBTNHeader.BorderBrush = new SolidColorBrush(Colors.Transparent);

            switch ((sender as Button).Name)
            {
                case "BTNAccount":
                    {
                        CurentPage = PageAccount;
                        PageAccount.Visibility = Visibility.Visible;
                        BTNAccount.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        CurentBTNHeader = BTNAccount;
                    }
                    break;
                case "BTNLeaderboards":
                    {
                        CurentPage = PageLeaderboards;
                        PageLeaderboards.Visibility = Visibility.Visible;
                        CurentBTNHeader = BTNLeaderboards;
                        BTNLeaderboards.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    }
                    break;
                case "BTNLogs":
                    {
                        CurentPage = PageLogs;
                        PageLogs.Visibility = Visibility.Visible;
                        CurentBTNHeader = BTNLogs;
                        BTNLogs.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                        RecivePlayerLogs();
                    }
                    break;
                default:
                    Debug.WriteLine("Not Set");
                    break;
            }
        }


        //page Setting
        private void NicknameChange(object sender, TextChangedEventArgs e)
        {
            var Textbox = sender as TextBox;
            if (IsLoaded)
            {
                Textbox.BorderBrush = new SolidColorBrush(Colors.Orange);
                PlayerDetail["Account"]["Name"] = Textbox.Text;
            }

        }

        private void AvatarChange(object sender, TextChangedEventArgs e)
        {
            var Textbox = sender as TextBox;
            if (IsLoaded)
            {
                try
                {
                    Uri link = new Uri(Textbox.Text);
                    Textbox.BorderBrush = new SolidColorBrush(Colors.Orange);
                    PlayerDetail["Account"]["Avatar"] = Textbox.Text;
                }
                catch (Exception)
                {
                    Textbox.BorderBrush = new SolidColorBrush(Colors.Red);
                }
            }

        }

        private void LanguageChange(object sender, TextChangedEventArgs e)
        {
            var Textbox = sender as TextBox;
            if (IsLoaded)
            {
                Textbox.BorderBrush = new SolidColorBrush(Colors.Orange);
                PlayerDetail["Account"]["Language"] = Textbox.Text;
            }
        }

        private void CountryChange(object sender, TextChangedEventArgs e)
        {
            var Textbox = sender as TextBox;
            if (IsLoaded)
            {
                Textbox.BorderBrush = new SolidColorBrush(Colors.Orange);
                PlayerDetail["Account"]["Country"] = Textbox.Text;

            }

        }

        private void UsernameChange(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (IsLoaded && textbox.Text.Length >= 6)
            {
                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchUsername(textbox.Text, result =>
                {
                    if (result)
                    {
                        textbox.BorderBrush = new SolidColorBrush(Colors.Tomato);
                        textbox.Text += new Random().Next();
                    }
                    else
                    {
                        textbox.BorderBrush = new SolidColorBrush(Colors.Orange);
                        PlayerDetail["Account"]["Username"] = textbox.Text;
                    }


                });
            }
        }

        private void EmailChange(object sender, TextChangedEventArgs e)
        {
            var Textbox = sender as TextBox;
            if (IsLoaded)
            {
                try
                {
                    new System.Net.Mail.MailAddress(Textbox.Text);
                    Textbox.BorderBrush = new SolidColorBrush(Colors.Orange);
                    PlayerDetail["Account"]["Email"] = Textbox.Text;
                }
                catch (Exception)
                {
                    Textbox.BorderBrush = new SolidColorBrush(Colors.Red);
                }

            }
        }


        private void AvatarHelp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DashboardGame.Dialog("Sample acceptable link\n\n https://example.com \n \n or\n \n http://example.com ", "Help Link");
        }

        private void LanguageHelp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DashboardGame.Dialog("ISO is the language standard (ISO 639-1) \n for example:\n \n\n Persian : (fa) \n \n or\n \n English : (en) ", "Help Language");
        }

        private void CountryHelp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DashboardGame.Dialog("ISO is the country  standard (ISO 3166 ALPHA3) \n for example:\n \n\n Iran : (IRN) \n \n or\n \n Belgien : (BEL) ", "Help Country");
        }

        private async void Delete(object sender, MouseButtonEventArgs e)
        {
            if (await DashboardGame.DialogYesNo("All user information is lost \n Are you sure") == MessageBoxResult.Yes)
            {
                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.Delete(PlayerDetail["Account"]["Token"].ToString(), result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Deleted !", Notifaction.StatusMessage.Ok);

                        RefreshList(null, null);
                        DashboardGame.Dashboard.Root.Children.Remove(this);

                        //add log
                        SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Delete Player", $"You have deleted player \" {PlayerDetail["Account"]["Username"]} \"", PlayerDetail, false, resul => { });
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Delete !", Notifaction.StatusMessage.Ok);
                    }
                });

            }
            else
            {
                DashboardGame.Notifaction("Delete Reject", Notifaction.StatusMessage.Ok);
            }
        }

        private void SaveSetting(object sender, MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.Save(PlayerDetail["Account"]["Token"].ToString(), PlayerDetail, result =>
            {
                if (result)
                {
                    RefreshList(null, null);
                    TextboxNickname.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    TextboxAvatar.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    TextLanguage.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    TextCountry.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    TextboxUsername.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    TextboxEmail.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    DashboardGame.Notifaction("Saved", Notifaction.StatusMessage.Ok);

                    // add log
                    SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Edit Player", $"You have Edit player \" {PlayerDetail["Account"]["Username"]} \" ", PlayerDetail, false, resul => { });
                }
                else
                {
                    DashboardGame.Notifaction("Faild Save", Notifaction.StatusMessage.Error);
                }

            });

        }

        private void CopyToken(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Clipboard.SetText((sender as TextBlock).Text);
            DashboardGame.Notifaction("Token Copied", Notifaction.StatusMessage.Ok);
        }


        //page Leaderboards

        /// <summary>
        /// if Player have a leaderboard
        /// </summary>
        /// <param name="Leaderboards"></param>
        public void ReciveLeaderboardPlayer(BsonDocument Leaderboards)
        {

            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reciveleaderboards(
                Resul =>
            {
                TotalLeaderboard = Resul;

                PlaceContentLeaderboard.Children.Clear();

                try
                {
                    foreach (var item in Leaderboards)
                    {
                        PlaceContentLeaderboard.Children.Add(new ModelLeaderboardPlayerEdit(TotalLeaderboard, Leaderboards, item.Name, item.Value.AsInt64));
                    }
                }
                catch (Exception)
                {

                }

            },
            () =>
            {

            });

        }


        private void Add(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                PlaceContentLeaderboard.Children.Add(new ModelLeaderboardPlayer_New(TotalLeaderboard, PlayerDetail["Leaderboards"]["List"].AsBsonDocument));
            }
            catch (Exception)
            {
                PlayerDetail.Add("Leaderboards", new BsonDocument { { "List", new BsonDocument { } } });

                PlaceContentLeaderboard.Children.Add(new ModelLeaderboardPlayer_New(TotalLeaderboard, PlayerDetail["Leaderboards"]["List"].AsBsonDocument));
            }
        }

        private void Save(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.Save_LeaderboardPlayer(PlayerDetail["Account"]["Token"].AsObjectId.ToString(), PlayerDetail["Leaderboards"]["List"].AsBsonDocument,
                () =>
                {
                    DashboardGame.Notifaction("Leaderboards Saved", Notifaction.StatusMessage.Ok);
                    ReciveLeaderboardPlayer(PlayerDetail["Leaderboards"]["List"].AsBsonDocument);

                    SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("modifie Player Leaderboard", $"You have modifie player \" {PlayerDetail["Account"]["Username"]} \" ", PlayerDetail["Leaderboards"]["List"].AsBsonDocument, false, resul => { });
                },
                () =>
                {
                    DashboardGame.Notifaction("Not Change", Notifaction.StatusMessage.Warrning);
                });
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }

        //page Log
        int CountLog = 100;

        public void RecivePlayerLogs()
        {
            PlaceContentLogs.Children.Clear();
            CountLog = 100;
            TextSeeMoreNumber.Text = CountLog.ToString();
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.RecivePlayerlog(PlayerDetail["Account"]["Token"].AsObjectId, CountLog,
                Result =>
                {

                    if (Result["Logs"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in Result["Logs"].AsBsonArray)
                        {
                            PlaceContentLogs.Children.Add(new ModelLogPlayer(item.AsBsonDocument));
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                    }
                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Recive", Notifaction.StatusMessage.Error);
                });
        }

        private void ShowPanelAddLogs()
        {
            PanelAddLog.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddLog.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        private void ShowoffPaneladdLogs()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, ee) =>
            {
                TextboxDescription.Text = "";
                TextboxHeader.Text = "";

                PanelAddLog.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddLog.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        Action<object, RoutedEventArgs> RefreshList;
    }
}
