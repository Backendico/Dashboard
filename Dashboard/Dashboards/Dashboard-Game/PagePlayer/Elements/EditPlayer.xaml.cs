using Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem;
using Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelAchievements;
using Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements.ModelLog;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    public partial class EditPlayer : UserControl, IEditLeaderboard, IEditAchievements, IEditLogs
    {
        IEditPlayer Editor;

        Grid CurentPage;
        Button CurentBTNHeader;




        public EditPlayer(IEditPlayer EditPlayer)
        {
            InitializeComponent();
            //frist init
            Editor = EditPlayer;

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
                TextCountTags.Text = EditPlayer.DetailPlayer["Account"]["Tags"].AsBsonArray.Count.ToString();

            }
            catch (Exception)
            {
                EditPlayer.DetailPlayer["Account"].AsBsonDocument.Add("Tags", new BsonArray());
                EditPlayer.Save();
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

            //action tag
            Tags.MouseDown += (s, e) =>
            {
                _ = new TagsSystem(EditPlayer.DetailPlayer["Account"]["Tags"].AsBsonArray, () =>
                {
                    TextCountTags.Text = EditPlayer.DetailPlayer["Account"]["Tags"].AsBsonArray.Count.ToString();
                    EditPlayer.Save();
                });
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
                                        PanelChangePassword.Visibility = System.Windows.Visibility.Visible;
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
                            PanelChangePassword.Visibility = System.Windows.Visibility.Collapsed;
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
            //btn action leaderboards
            BTNAaddLeaderboardShow.MouseDown += (s, e) =>
            {
                ShowpanelAddLeaderboard();
            };

            //close panel leaderboard
            PanelAddLeaderboard.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelLeaderboard();
                }
            };


            #endregion

            #region PageAchievements
            //action BTN Achievements
            BTNAchievements.Click += (s, e) =>
            {
                IniAchievements();
            };

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

            //Init Logs
            BTNLogs.Click += (s, e) =>
            {
                InitLogs();
            };


            //action btn send log
            BTNSendLog.MouseDown += (s, e) =>
            {
                if (TextboxHeader.Text.Length >= 1 && TextboxDescription.Text.Length >= 1)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PagePlayers.AddLogPlayer(Editor.DetailPlayer["Account"]["Token"].AsObjectId, TextboxHeader.Text, TextboxDescription.Text, result =>
                    {
                        if (result)
                        {
                            DashboardGame.Notifaction("Log Added", Notifaction.StatusMessage.Ok);
                            InitLogs();
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
            BTNClearLogs.MouseDown += async (s, e) =>
             {
                 if (await DashboardGame.DialogYesNo("All information will be lost.\nare you sure ? ") == MessageBoxResult.Yes)
                 {
                     SDK.SDK_PageDashboards.DashboardGame.PagePlayers.ClearLog(Editor.DetailPlayer["Account"]["Token"].AsObjectId, result =>
                    {
                     if (result)
                     {
                         DashboardGame.Notifaction("Logs Clear", Notifaction.StatusMessage.Ok);
                         InitLogs();
                     }
                     else
                     {
                         DashboardGame.Notifaction("Faild Clear", Notifaction.StatusMessage.Error);
                     }

                 });

                 }
                 else
                 {
                     DashboardGame.Notifaction("Canceled", Notifaction.StatusMessage.Warrning);
                 }
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
                Count += 100;
                TextSeeMoreNumber.Text = Count.ToString();
            };

            #endregion


            InitLeaderboards();
        }

        #region Global

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
                    }
                    break;
                case "BTNAchievements":
                    {
                        CurentPage = PageAchievements;
                        PageAchievements.Visibility = Visibility.Visible;
                        CurentBTNHeader = BTNAchievements;
                        BTNAchievements.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    }
                    break;
                default:
                    Debug.WriteLine("Not Set");
                    break;
            }
        }



        private void Close(object sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }

        #endregion



        #region PageLeaderboards


        public void AddLeaderbord(string NameLeaderboard, long Score)
        {
            var Serilise = new BsonDocument {
                {"Leaderboard",NameLeaderboard },
                {"Score",Score }
            };

            for (int i = 0; i < Editor.DetailPlayer["Leaderboards"].AsBsonArray.Count; i++)
            {
                if (Editor.DetailPlayer["Leaderboards"].AsBsonArray[i]["Leaderboard"] == NameLeaderboard)
                {
                    Editor.DetailPlayer["Leaderboards"].AsBsonArray.RemoveAt(i);
                }
            }


            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.AddPlayer(NameLeaderboard, Editor.DetailPlayer["Account"]["Token"].AsObjectId, Score, result =>
            {
                Editor.DetailPlayer["Leaderboards"].AsBsonArray.Add(Serilise);
                InitLeaderboards();
                ShowOffPanelLeaderboard();
            });
        }

        public void InitLeaderboards()
        {
            PlaceContentLeaderboard.Children.Clear();

            //init Player leaderboards
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.RecievePlayerLeaderboard(Editor.DetailPlayer["Account"]["Token"].AsObjectId, result =>
               {
                   if (result.ElementCount >= 1)
                   {
                       foreach (var item in result["Leaderboards"].AsBsonArray)
                       {
                           PlaceContentLeaderboard.Children.Add(new ModelLeaderboard.LeaderaboardPlayer(item["Leaderboard"].ToString(), item["Score"].ToInt64()));
                       }
                   }
                   else
                   {
                       DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                   }
               });

            //ini Studio Leaderboard
            SDK.SDK_PageDashboards.DashboardGame.PageLeaderboard.Reciveleaderboards((Result) =>
            {
                PlaceLeaderboardStudio.Children.Clear();

                if (Result.ElementCount >= 1)
                {
                    foreach (var item in Result["List"].AsBsonArray)
                    {
                        PlaceLeaderboardStudio.Children.Add(new ModelLeaderboard.ModelLeaderboards(item.AsBsonDocument, this));
                    }
                }
                else
                {

                    DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                }

            });
        }

        //action show panel leaderboards 
        private void ShowpanelAddLeaderboard()
        {
            InitLeaderboards();

            PanelAddLeaderboard.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddLeaderboard.Name);
            Storyboard Story = new Storyboard();
            Story.Children.Add(Anim);
            Story.Begin(this);
        }

        private void ShowOffPanelLeaderboard()
        {
            PanelAddLeaderboard.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddLeaderboard.Visibility = Visibility.Collapsed;

                PlaceLeaderboardStudio.Children.Clear();
            };
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddLeaderboard.Name);
            Storyboard Story = new Storyboard();
            Story.Children.Add(Anim);
            Story.Begin(this);
        }

        #endregion


        #region Page Achivements

        public void IniAchievements()
        {

            //ini Studio achievements
            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.ReciveAchievements(result =>
            {
                ListStudioAchievement.Children.Clear();

                if (result.ElementCount >= 1)
                {

                    foreach (var item in result["Achievements"].AsBsonArray)
                    {
                        ListStudioAchievement.Children.Add(new ModelAchievementStudio(item.AsBsonDocument, this));
                    }

                }
                else
                {
                    DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                    ShowPanelAddAchievements();
                }
            });

            //init Player Achievements
            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.PlayerAchievements(Editor.DetailPlayer["Account"]["Token"].AsObjectId, Result =>
            {
                PlaceContentAchievements.Children.Clear();


                if (Result.ElementCount >= 1)
                {
                    foreach (var item in Result["Achievements"].AsBsonArray)
                    {
                        PlaceContentAchievements.Children.Add(new ModelAchievements.ModelAchievements(item.AsBsonDocument, this));
                    }
                }

            });
        }

        public void AddAchievements(BsonDocument DetailNewachievements)
        {
            var Serilse = new BsonDocument
            {
                {"Token",DetailNewachievements["Token"] },
                {"Name",DetailNewachievements["Name"] },
            };

            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.AddPlayerAchievements(Editor.DetailPlayer["Account"]["Token"].AsObjectId, Serilse, Result =>
            {
                if (Result)
                {
                    DashboardGame.Notifaction("Achievement Added", Notifaction.StatusMessage.Ok);
                }
                else
                {
                    DashboardGame.Notifaction("Achievement already added", Notifaction.StatusMessage.Warrning);
                }
                IniAchievements();

            });

        }

        public void RemoveAchievements(BsonDocument DetailAchievements)
        {

            SDK.SDK_PageDashboards.DashboardGame.PageAchievements.RemoveAchievementsPlayer(Editor.DetailPlayer["Account"]["Token"].AsObjectId, DetailAchievements, result =>
            {
                if (result)
                {
                    DashboardGame.Notifaction("Removed", Notifaction.StatusMessage.Ok);
                    IniAchievements();
                }
                else
                {
                    DashboardGame.Notifaction("Faild Remove", Notifaction.StatusMessage.Error);
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

        #endregion


        #region PageLog

        int Count = 100;

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


        public void InitLogs()
        {
            Count = 100;
            TextSeeMoreNumber.Text = Count.ToString();

            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.RecivePlayerlog(Editor.DetailPlayer["Account"]["Token"].AsObjectId, Count, result =>
            {

                PlaceContentLogs.Children.Clear();

                if (result.ElementCount >= 1)
                {
                    foreach (var item in result["Logs"].AsBsonArray)
                    {
                        PlaceContentLogs.Children.Add(new ModelLogPlayer(item.AsBsonDocument));
                    }
                }
                else
                {
                    ShowPanelAddLogs();
                    DashboardGame.Notifaction("No Content", Notifaction.StatusMessage.Warrning);
                }
            });
        }

        #endregion
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

    public interface IEditLeaderboard
    {
        void InitLeaderboards();
        void AddLeaderbord(string NameLeaderboard, long Score);
    }

    public interface IEditAchievements
    {
        void IniAchievements();
        void AddAchievements(BsonDocument DetailNewAchievement);
        void RemoveAchievements(BsonDocument DetailAchievements);
    }

    public interface IEditLogs
    {
        void InitLogs();
    }
}

