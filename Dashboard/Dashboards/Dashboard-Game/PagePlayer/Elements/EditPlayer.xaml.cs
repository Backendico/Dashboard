using Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLog;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Resources;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    public partial class EditPlayer : UserControl
    {

        BsonDocument PlayerDetail;
        BsonDocument TotalLeaderboard;


        Grid CurentPage;
        Button CurentBTNHeader;


        public EditPlayer(IEditPlayer EditPlayer)
        {
            InitializeComponent();

            //frist init
            this.PlayerDetail = PlayerDetail;
            this.RefreshList = RefreshList;

            CurentPage = PageAccount;
            CurentBTNHeader = BTNAccount;

            #region PageSetting

            //change frist
            TextIDPlayer_Header.Text = EditPlayer.DetailPlayer["Account"]["Token"].AsObjectId.ToString();
            TextboxNickname.Text = EditPlayer.DetailPlayer["Account"]["Name"].AsString;
            TextboxAvatar.Text = EditPlayer.DetailPlayer["Account"]["Avatar"].AsString;
            TextLanguage.Text = EditPlayer.DetailPlayer["Account"]["Language"].AsString;
            TextCreated.Text = EditPlayer.DetailPlayer["Account"]["Created"].ToUniversalTime().ToString();
            TextCountry.Text = EditPlayer.DetailPlayer["Account"]["Country"].AsString;
            TextboxUsername.Text = EditPlayer.DetailPlayer["Account"]["Username"].AsString;
            TextboxEmail.Text = EditPlayer.DetailPlayer["Account"]["Email"].AsString;
            TextToken.Text = EditPlayer.DetailPlayer["Account"]["Token"].AsObjectId.ToString();
            Textboxphone.Text = EditPlayer.DetailPlayer["Account"]["Phone"].ToString();

            try
            {

            }
            catch (Exception)
            {


            }




            //cheak banplayer
            CheackBoxBan.IsChecked = EditPlayer.DetailPlayer["Account"]["IsBan"].AsBoolean;
            CheackBoxBan.Checked += (s, e) =>
            {
                EditPlayer.DetailPlayer["Account"]["IsBan"] = true;
                EditPlayer.Save();
            };
            CheackBoxBan.Unchecked += (s, e) =>
            {
                EditPlayer.DetailPlayer["Account"]["IsBan"] = false;
                EditPlayer.Save();
            };


            //action  Change NickName
            TextboxNickname.LostFocus += (s, e) =>
            {
                EditPlayer.DetailPlayer["Account"]["Name"] = TextboxNickname.Text;
                EditPlayer.Save();
            };

            //action change nickname
            TextboxAvatar.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllLinkImages(TextboxAvatar))
                {
                    EditPlayer.DetailPlayer["Account"]["Avatar"] = TextboxAvatar.Text;
                    EditPlayer.Save();
                }
                else
                {
                    TextboxAvatar.Text = EditPlayer.DetailPlayer["Account"]["Avatar"].ToString();
                }

            };

            //action change Language
            TextLanguage.LostFocus += (s, e) =>
            {
                EditPlayer.DetailPlayer["Account"]["Language"] = TextLanguage.Text;
                EditPlayer.Save();
            };

            //action change Cuntry
            TextCountry.LostFocus += (s, e) =>
            {
                EditPlayer.DetailPlayer["Account"]["Country"] = TextCountry.Text;
                EditPlayer.Save();
            };

            //action change Username
            TextboxUsername.LostFocus += (s, e) =>
            {
                if (TextboxUsername.Text.Length >= 6)
                {
                    if (TextboxUsername.Text != EditPlayer.DetailPlayer["Account"]["Username"].ToString())
                    {

                        SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchUsername(TextboxUsername.Text, result =>
                        {
                            if (result)
                            {
                                DashboardGame.Notifaction("Username is duplicate ", Notifaction.StatusMessage.Error);
                                TextboxUsername.Text = EditPlayer.DetailPlayer["Account"]["Username"].ToString();
                            }
                            else
                            {
                                EditPlayer.DetailPlayer["Account"]["Username"] = TextboxUsername.Text;
                                EditPlayer.Save();
                            }
                        });
                    }
                }
                else
                {
                    DashboardGame.Notifaction("Username Short", Notifaction.StatusMessage.Error);
                    TextboxUsername.Text = EditPlayer.DetailPlayer["Account"]["Username"].ToString();
                }

            };

            //action change email
            TextboxEmail.LostFocus += (s, e) =>
            {
                try
                {
                    new System.Net.Mail.MailAddress(TextboxEmail.Text);
                    EditPlayer.DetailPlayer["Account"]["Email"] = TextboxEmail.Text;
                    EditPlayer.Save();
                }
                catch (Exception)
                {
                    TextboxEmail.Text = EditPlayer.DetailPlayer["Account"]["Email"].ToString();
                }

            };

            //action BTN control and change phone
            Textboxphone.LostFocus += (s, e) =>
            {
                if (long.TryParse(Textboxphone.Text, out long Handle))
                {
                    EditPlayer.DetailPlayer["Account"]["Phone"] = Handle;
                    EditPlayer.Save();
                }
                else
                {
                    DashboardGame.Notifaction("Can't Conver to Phone number", Notifaction.StatusMessage.Error);
                    Textboxphone.Text = EditPlayer.DetailPlayer["Account"]["Phone"].ToString();
                }
            };


            //action btn Email Recovery
            BTNSendEmailRecovery.MouseDown += async (s, e) =>
            {
                try
                {
                    new MailAddress(EditPlayer.DetailPlayer["Account"]["Email"].ToString());


                    if (await DashboardGame.DialogYesNo($"Do you want to send the recovery email to \"{EditPlayer.DetailPlayer["Account"]["Token"].AsObjectId}\"?") == MessageBoxResult.Yes)
                    {
                        try
                        {

                            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.Recovery1(EditPlayer.DetailPlayer["Account"]["Token"].AsObjectId, new System.Net.Mail.MailAddress(EditPlayer.DetailPlayer["Account"]["Email"].AsString),
                                Code =>
                                {
                                    if (Code != 0)
                                    {
                                        DashboardGame.Dialog(Code.ToString(), "Recovery Code");
                                        PanelChangePassword.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        DashboardGame.Notifaction("Faild Send", Notifaction.StatusMessage.Error);
                                    }

                                });
                        }
                        catch (Exception ex)
                        {

                            DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                        }
                    }
                    else
                    {

                        DashboardGame.Notifaction("Rejected", Notifaction.StatusMessage.Error);
                    }
                }
                catch (Exception)
                {
                    DashboardGame.Notifaction("The player does not have an email", Notifaction.StatusMessage.Error);
                }

            };

            //action btn Change Password
            BTNChangePassword.MouseDown += (s, e) =>
            {
                if (TextNewPassword.Text.Length >= 6)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PagePlayers.ChangePassword(EditPlayer.DetailPlayer["Account"]["Token"].AsObjectId, TextNewPassword.Text, Result =>
                    {
                        if (Result)
                        {
                            DashboardGame.Notifaction("Password Changed", Notifaction.StatusMessage.Ok);

                            TextNewPassword.Text = "";
                            PanelChangePassword.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Change Password", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                else
                {
                    DashboardGame.Notifaction("Password Short", Notifaction.StatusMessage.Warrning);
                }
            };

            //copyToken
            TextToken.MouseDown += GlobalEvents.CopyText;

            //action btn Delete
            BTNDelete.MouseDown += (s, e) =>
            {
                EditPlayer.Delete(this);

            };

            #endregion

            #region PageLeaderboard


            //init pageLeaderboard
            try
            {
                ReciveLeaderboardPlayer(EditPlayer.DetailPlayer["Leaderboards"]["List"].AsBsonDocument);
            }
            catch (Exception)
            {
                PlayerDetail.Add("Leaderboards", new BsonDocument { { "List", new BsonDocument { } } });

                ReciveLeaderboardPlayer(EditPlayer.DetailPlayer["Leaderboards"]["List"].AsBsonDocument);
            }

            #endregion

            #region PageAchievements
            //action show panel Add achievements
            BTNShowPanelAddAchievement.MouseDown += (s, e) =>
            {
                ShowPanelAddAchievements();
            };

            //action Panel Achievements
            PanelAddAchievements.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPaneladdAchievements();
                }
            };

            #endregion

            #region PageLogs


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

            //action btn ClosePaneladdlog
            PanelAddLog.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowoffPaneladdLogs();
                }
            };

            //actin btn Add Log
            BTNAddLog.MouseDown += (s, e) =>
            {
                ShowPanelAddLogs();
            };
            //action btn see more
            BTNSeeMoreLog.MouseDown += (s, e) =>
            {
                CountLog += 100;
                TextSeeMoreNumber.Text = CountLog.ToString();
                RecivePlayerLogs();
            };

            #endregion

        }

        //global
        private void ChangePage(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content != BTNLogs.Content)
            {
                ShowoffPaneladdLogs();

            }


            if ((sender as Button).Content != BTNAchievements.Content)
            {
                ShowoffPaneladdAchievements();

            }

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
                case "BTNAchievements":
                    {
                        CurentPage = PageAchievements;
                        PageAchievements.Visibility = Visibility.Visible;
                        CurentBTNHeader = BTNAchievements;
                        BTNAchievements.BorderBrush = new SolidColorBrush(Colors.DarkOrange);

                        RecivePlayerAchievements();
                    }
                    break;
                default:
                    Debug.WriteLine("Not Set");
                    break;
            }
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


        private void Add(object sender, MouseButtonEventArgs e)
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

        private void Save(object sender, MouseButtonEventArgs e)
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
                    try
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
                    }
                    catch (Exception)
                    {
                        DashboardGame.Notifaction("No logs Content", Notifaction.StatusMessage.Warrning);
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


        //page Achievements
        public void RecivePlayerAchievements()
        {
            PlaceContentAchievements.Children.Clear();

            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.PlayerAchievements(PlayerDetail["Account"]["Token"].AsObjectId,
                result =>
                {
                    if (result.ElementCount >= 1)
                    {
                        if (result["Achievements"].AsBsonArray.Count >= 1)
                        {

                            ShowoffPaneladdAchievements();

                            foreach (var item in result["Achievements"].AsBsonArray)
                            {
                                PlaceContentAchievements.Children.Add(new ModelAchievements.ModelAchievements(item.AsBsonDocument, PlayerDetail["Account"]["Token"].AsObjectId, RecivePlayerAchievements));
                            }
                        }
                        else
                        {
                            DashboardGame.Notifaction("This player has no achievements", Notifaction.StatusMessage.Error);
                            ShowPanelAddAchievements();
                        }

                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Recive", Notifaction.StatusMessage.Error);
                    }
                });

        }

        private void ShowPanelAddAchievements()
        {
            PanelAddAchievements.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddAchievements.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);


            //recive achievement for fill list achievements
            ListStudioAchievement.Children.Clear();
            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.ReciveAchievements(StudioAchievements =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageAchievements.PlayerAchievements(PlayerDetail["Account"]["Token"].AsObjectId, AchievementPlayer =>
                {
                    if (StudioAchievements.ElementCount >= 1)
                    {
                        if (StudioAchievements["Achievements"].AsBsonArray.Count >= 1)
                        {

                            if (AchievementPlayer["Achievements"].AsBsonArray.Count >= 1)
                            {

                                var Mustdelte = new BsonArray();

                                foreach (var STDAchievements in StudioAchievements["Achievements"].AsBsonArray)
                                {
                                    foreach (var PLYRAchievements in AchievementPlayer["Achievements"].AsBsonArray)
                                    {
                                        if (STDAchievements["Token"].AsObjectId == PLYRAchievements["Token"].AsObjectId)
                                        {
                                            AchievementPlayer["Achievements"].AsBsonArray.Remove(STDAchievements);
                                            Mustdelte.Add(STDAchievements);
                                        }
                                    }
                                }

                                for (int i = 0; i < StudioAchievements["Achievements"].AsBsonArray.Count; i++)
                                {
                                    for (int a = 0; a < Mustdelte.Count; a++)
                                    {
                                        if (StudioAchievements["Achievements"].AsBsonArray[i]["Token"] == Mustdelte[a]["Token"])
                                        {
                                            StudioAchievements["Achievements"].AsBsonArray.RemoveAt(i);
                                        }
                                    }
                                }


                                //cheack player recive all achievements
                                if (StudioAchievements["Achievements"].AsBsonArray.Count >= 1)
                                {

                                    foreach (var item in StudioAchievements["Achievements"].AsBsonArray)
                                    {
                                        ListStudioAchievement.Children.Add(new ModelFind(PlayerDetail["Account"]["Token"].AsObjectId, item.AsBsonDocument, RecivePlayerAchievements, ListStudioAchievement));
                                    }
                                }
                                else
                                {
                                    ListStudioAchievement.Children.Add(new TextBlock() { Margin = new Thickness(10), Text = "The player has collected all the achievements" });
                                }
                            }
                            else
                            {
                                foreach (var item in StudioAchievements["Achievements"].AsBsonArray)
                                {
                                    ListStudioAchievement.Children.Add(new ModelFind(PlayerDetail["Account"]["Token"].AsObjectId, item.AsBsonDocument, RecivePlayerAchievements, ListStudioAchievement));
                                }
                            }

                        }
                        else
                        {
                            ListStudioAchievement.Children.Add(new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10), Text = "The studio has not achieved!\nTo add an achievement,go to the achievements section and create one" });

                            DashboardGame.Notifaction(" Studio No achievement", Notifaction.StatusMessage.Warrning);
                        }

                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Recive", Notifaction.StatusMessage.Warrning);
                    }



                }
                );
            });


        }

        private void ShowoffPaneladdAchievements()
        {
            DoubleAnimation Anim1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));

            Anim1.Completed += (s, e) =>
            {

                PanelAddAchievements.Visibility = Visibility.Collapsed;
            };

            Storyboard.SetTargetProperty(Anim1, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim1, PanelAddAchievements.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim1);
            storyboard.Begin(this);

        }




        Action RefreshList;
    }

    public class ModelFind : StackPanel
    {
        public BsonDocument Detail;

        public ModelFind(ObjectId TokenPlayer, BsonDocument Detail, Action Refreshlist, StackPanel PlaceListContentAchievements)
        {
            Background = new SolidColorBrush(Colors.White);

            Margin = new Thickness(5);
            Cursor = Cursors.Hand;


            var Name = new StackPanel() { Orientation = Orientation.Horizontal };
            var Value = new StackPanel() { Orientation = Orientation.Horizontal };
            var ID = new StackPanel() { Orientation = Orientation.Horizontal };

            Name.Children.Add(new TextBlock() { Text = "Name: ", FontWeight = FontWeights.Bold });
            Name.Children.Add(new TextBlock() { Text = Detail["Name"].ToString() });

            Value.Children.Add(new TextBlock() { Text = "Value: ", FontWeight = FontWeights.Bold });
            Value.Children.Add(new TextBlock() { Text = Detail["Value"].ToString() });

            ID.Children.Add(new TextBlock() { Text = "Token: ", FontWeight = FontWeights.Bold });
            ID.Children.Add(new TextBlock() { Text = Detail["Token"].ToString() });


            this.Detail = Detail;
            Children.Add(Name);
            Children.Add(Value);
            Children.Add(ID);
            Children.Add(new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Gray),
                BorderThickness = new Thickness(0, 1, 0, 0)
            });


            //action select 
            MouseDown += (s, e) =>
            {
                Detail.Remove("Players");
                Detail.Remove("Created");
                Detail.Remove("Value");

                Debug.WriteLine(Detail);

                SDK.SDK_PageDashboards.DashboardGame.PageAchievements.AddPlayerAchievements(TokenPlayer, Detail, result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Achievement add to player", Notifaction.StatusMessage.Ok);
                        Refreshlist();
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Add ", Notifaction.StatusMessage.Error);
                    }
                });

            };



            MouseEnter += (s, e) =>
            {
                Background = new SolidColorBrush(Colors.Gainsboro);
            };

            MouseLeave += (s, e) =>
            {
                Background = new SolidColorBrush(Colors.White);
            };
        }

    }
}
