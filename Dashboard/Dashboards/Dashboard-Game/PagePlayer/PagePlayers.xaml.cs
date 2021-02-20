using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.PagePlayer
{
    /// <summary>
    /// Interaction logic for PagePlayers.xaml
    /// </summary>
    public partial class PagePlayers : UserControl
    {
        int PlayerCount = 0;
        int ReciveCount = 100;

        public PagePlayers()
        {
            InitializeComponent();
            RecivePlayersList();

            //close panel add player
            PanelAddPlayer.MouseDown += (e, s) =>
            {
                if (s.Source.GetType() == typeof(Grid))
                {
                    ShowOffSubpagePlayer();
                }
            };

            //close panel search
            PanelSearch.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelSearch();
                }
            };

            //show panel search
            BTNSearch.MouseDown += (s, e) =>
            {
                ShowOpenPanelSearch();
            };

            //show paneladdplayer
            BTNShowPanelAdd.MouseDown += (s, e) =>
            {
                ShowSubpagePlayer();
            };

            //actin add player
            BTNaddPlayer.MouseDown += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveMonetize(result =>
                {

                    //limit filter
                    if (PlayerCount + 1 <= result["Players"])
                    {
                        SDK.SDK_PageDashboards.DashboardGame.PagePlayers.CreatPlayer(TextBoxUsername.Text, TextBoxPassword.Password,
                         (resultCreated) =>
                         {
                             RecivePlayersList();

                             ShowOffSubpagePlayer();

                             DashboardGame.Notifaction("Player Added", StatusMessage.Ok);

                             //add log
                             var Detail = new BsonDocument
                             {
                     {"Username",TextBoxUsername.Text },
                     {"Password",TextBoxPassword.Password },
                     {"LocalTime",DateTime.Now }
                             };

                             SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Add Player", $"You have added  \" {TextBoxUsername.Text} \" player", Detail, false, resultlog => { });
                         }
                        );
                    }
                    else
                    {
                        DashboardGame.Notifaction("Cannot create new player. Please buy more players than payments", StatusMessage.Error);
                    }

                }, () => { });

            };

            BTNSeeMorePlayer.MouseDown += (s, e) =>
            {
                ReciveCount += 100;
                TextSeeMoreNumber.Text = ReciveCount.ToString();
                RecivePlayersList();
            };

        }


        /// <summary>
        /// 1:recive list players top 100
        /// 2: instant eachplayer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RecivePlayersList()
        {
            PlaceContentUser.Children.Clear();

            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.RecieveListPlayer(ReciveCount,
                result =>
                {
                    if (result["ListPlayers"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in result["ListPlayers"].AsBsonArray)
                        {
                            PlaceContentUser.Children.Add(new ModelAbstractUser(item.AsBsonDocument, RecivePlayersList));
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No content", StatusMessage.Warrning);
                        ShowSubpagePlayer();
                    }

                    //init total player
                    TextTotalPlayer.Text = $"( {result["Players"]} )   total players";

                    PlayerCount = result["Players"].ToInt32();
                });

        }


        //Subpage Add player
        void ShowSubpagePlayer()
        {
            PanelAddPlayer.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));

            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

        }


        public void ShowOffSubpagePlayer()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddPlayer.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        private void CheackUsername(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchUsername(textbox.Text, Result =>
            {
                if (Result)
                {

                    textbox.BorderBrush = new SolidColorBrush(Colors.Tomato);
                    textbox.Text += new Random().Next();
                    DashboardGame.Notifaction("Username found ", StatusMessage.Error);
                }
                else
                {
                    textbox.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                }

            });

        }


        //Subpage search
        BsonDocument CurentSearchResult;

        void ShowOpenPanelSearch()
        {
            PanelSearch.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelSearch.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void ShowOffPanelSearch()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelSearch_Result.Visibility = Visibility.Collapsed;

                PanelSearch.Visibility = Visibility.Collapsed;

                TextBoxUsername_Search.Text = "";
                TextBoxEmail_Search.Text = "";
                TextBoxToken_Search.Text = "";
            };

            Storyboard.SetTargetName(Anim, PanelSearch.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


        void ChangeSearchTexts(BsonDocument Result)
        {
            PanelSearch_Result.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 130, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelSearch_Result.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

            CurentSearchResult = Result;

            TextUsername_Result.Text = Result["Account"]["Username"].ToString();
            TextEmail_Result.Text = Result["Account"]["Email"].ToString();
            TextToken_Result.Text = Result["Account"]["Token"].ToString();

        }

        private void SearchUsername(object sender, MouseButtonEventArgs e)
        {
            //cheack leght username
            if (TextBoxUsername_Search.Text.Length >= 6)
            {
                //recive detail
                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchUsername(TextBoxUsername_Search.Text,
                    result =>
                    {
                        ChangeSearchTexts(result);
                    },
                    () =>
                    {
                        DashboardGame.Notifaction("Not Found", StatusMessage.Warrning);
                    });

            }
            else
            {
                DashboardGame.Notifaction("Username Short(More than 6 charecter)", StatusMessage.Error);
            }
        }


        private void SearchEmail(object sender, MouseButtonEventArgs e)
        {
            //cheack email valid
            try
            {
                _ = new System.Net.Mail.MailAddress(TextBoxEmail_Search.Text);
                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchEmail(TextBoxEmail_Search.Text,
                    result =>
                    {
                        ChangeSearchTexts(result);
                    },
                    () =>
                    {
                        DashboardGame.Notifaction("Not Found", StatusMessage.Warrning);

                    });
            }
            catch (Exception ex)
            {
                DashboardGame.Notifaction(ex.Message, StatusMessage.Error);
            }
        }


        private void SearchToken(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _ = ObjectId.Parse(TextBoxToken_Search.Text);

                SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchToken(TextBoxToken_Search.Text,
                    result =>
                    {
                        ChangeSearchTexts(result);
                    },
                    () =>
                    {
                        DashboardGame.Notifaction("Not Found", StatusMessage.Warrning);
                    });
            }
            catch (Exception ex)
            {
                DashboardGame.Notifaction(ex.Message, StatusMessage.Error);

            }


        }

        private void OpenEditPlayer(object sender, MouseButtonEventArgs e)
        {
            ShowOffPanelSearch();
        }


    }
}
