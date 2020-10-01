﻿using Dashboard.GlobalElement;
using MongoDB.Bson;
using RestSharp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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

            //init pageLeaderboard
            try
            {
                InitLeaderboard(PlayerDetail["Leaderboards"]["List"].AsBsonDocument);
            }
            catch (Exception)
            {
                PlayerDetail.Add("Leaderboards", new BsonDocument { { "List", new BsonDocument { } } });

                InitLeaderboard(PlayerDetail["Leaderboards"]["List"].AsBsonDocument);
            }


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
                        Debug.WriteLine("Set logs");

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
            MessageBox.Show("Sample acceptable link\n\n https://example.com \n \n or\n \n http://example.com ");
        }
        private void LanguageHelp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("ISO is the language standard (ISO 639-1) \n for example:\n \n\n Persian : (fa) \n \n or\n \n English : (en) ");
        }
        private void CountryHelp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("ISO is the country  standard (ISO 3166 ALPHA3) \n for example:\n \n\n Iran : (IRN) \n \n or\n \n Belgien : (BEL) ");
        }


        private void Delete(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.Delete(PlayerDetail["Account"]["Token"].ToString(), result =>
            {
                if (result)
                {
                    MessageBox.Show("Deleted !");
                    RefreshList(null, null);
                    (Parent as Grid).Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Faild Delete! :(");
                }

            });

        }

        private void Save(object sender, RoutedEventArgs e)
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
            MessageBox.Show("Copyed !");
        }


        //page Leaderboards

        /// <summary>
        /// if Player have a leaderboard
        /// </summary>
        /// <param name="Leaderboards"></param>
        public void InitLeaderboard(BsonDocument Leaderboards)
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
                    InitLeaderboard(PlayerDetail["Leaderboards"]["List"].AsBsonDocument);
                },
                () =>
                {
                    DashboardGame.Notifaction("Not Change", Notifaction.StatusMessage.Warrning);
                });
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }


        Action<object, RoutedEventArgs> RefreshList;
    }
}
