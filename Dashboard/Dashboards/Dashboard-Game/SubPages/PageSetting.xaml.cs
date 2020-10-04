using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.SubPages.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages
{
    public partial class PageSetting : UserControl
    {

        public Grid CurentPage;
        public Button CurentBTNHeader;
        public BsonDocument FristMonetiz = new BsonDocument();
        public BsonDocument CurentMonetiz = new BsonDocument();
        public BsonDocument NewMonetiz = new BsonDocument();

        public PageSetting()
        {
            InitializeComponent();
            CurentPage = PageSettings;
            CurentBTNHeader = BTNSetting;

            //fill frist Setting
            TextName.Text = SettingUser.CurentDetailStudio["Name"].ToString();
            TextType.Text = SettingUser.CurentDetailStudio["Type"].ToString();
            TextToken.Text = SettingUser.CurentDetailStudio["Token"].ToString();
            TextCreator.Text = SettingUser.CurentDetailStudio["Creator"].ToString();
            TextCreated.Text = SettingUser.CurentDetailStudio["Created"].ToLocalTime().ToString();
            TextDatabase.Text = SettingUser.CurentDetailStudio["Database"].ToString();



            //action btn
            BTNPlayer.MouseDown += (s, obj) =>
            {
                if (CurentMonetiz["Cash"].AsInt32 - 10000 >= 0)
                {

                    CurentMonetiz["Players"] = CurentMonetiz["Players"].AsInt32 + 1000;
                    NewMonetiz["Players"] = NewMonetiz["Players"].AsInt32 + 1000;

                    NewMonetiz["Cash"] = NewMonetiz["Cash"].AsInt32 + 10000;

                    CurentMonetiz["Cash"] = CurentMonetiz["Cash"].AsInt32 - 10000;

                    BTNRevite.Visibility = Visibility.Visible;
                }

                Cheackcash();
                RechangeNew();
            };

            BTNLeaderboards.MouseDown += (s, obj) =>
            {
                if (CurentMonetiz["Cash"].AsInt32 - 10000 >= 0)
                {
                    CurentMonetiz["Leaderboards"] = CurentMonetiz["Leaderboards"].AsInt32 + 1;

                    NewMonetiz["Leaderboards"] = NewMonetiz["Leaderboards"].AsInt32 + 1;

                    NewMonetiz["Cash"] = NewMonetiz["Cash"].AsInt32 + 10000;

                    CurentMonetiz["Cash"] = CurentMonetiz["Cash"].AsInt32 - 10000;

                    BTNRevite.Visibility = Visibility.Visible;
                }
                Cheackcash();
                RechangeNew();
            };

            BTNAPIs.MouseDown += (s, obj) =>
            {
                if (CurentMonetiz["Cash"].AsInt32 - 10000 >= 0)
                {
                    CurentMonetiz["Apis"] = CurentMonetiz["Apis"].AsInt32 + 3000;

                    NewMonetiz["Apis"] = NewMonetiz["Apis"].AsInt32 + 3000;

                    NewMonetiz["Cash"] = NewMonetiz["Cash"].AsInt32 + 20000;

                    CurentMonetiz["Cash"] = CurentMonetiz["Cash"].AsInt32 - 20000;


                    BTNRevite.Visibility = Visibility.Visible;
                }
                Cheackcash();
                RechangeNew();
            };

            BTNStudio.MouseDown += (s, obj) =>
            {
                if (CurentMonetiz["Cash"].AsInt32 - 10000 >= 0)
                {
                    CurentMonetiz["Studios"] = CurentMonetiz["Studios"].AsInt32 + 1;

                    NewMonetiz["Studios"] = NewMonetiz["Studios"].AsInt32 + 1;

                    NewMonetiz["Cash"] = NewMonetiz["Cash"].AsInt32 + 60000;

                    CurentMonetiz["Cash"] = CurentMonetiz["Cash"].AsInt32 - 60000;


                    BTNRevite.Visibility = Visibility.Visible;
                }
                Cheackcash();
                RechangeNew();
            };

            BTNLogs.MouseDown += (s, obj) =>
            {
                if (CurentMonetiz["Cash"].AsInt32 - 10000 >= 0)
                {
                    CurentMonetiz["Logs"] = CurentMonetiz["Logs"].AsInt32 + 100;

                    NewMonetiz["Logs"] = NewMonetiz["Logs"].AsInt32 + 100;

                    NewMonetiz["Cash"] = NewMonetiz["Cash"].AsInt32 + 30000;

                    CurentMonetiz["Cash"] = CurentMonetiz["Cash"].AsInt32 - 30000;


                    BTNRevite.Visibility = Visibility.Visible;
                }

                Cheackcash();
                RechangeNew();
            };


            BTNRevite.MouseDown += (s, obj) =>
            {
                NewMonetiz = new BsonDocument
                        {
                            {"Players",0 },
                            {"Leaderboards",0 },
                            {"Apis",0 },
                            {"Studios",0 },
                            {"Logs",0 },
                            {"Cash",0 }
                        };

                //change curentmonetize to defult
                CurentMonetiz.Clear();
                foreach (var item in FristMonetiz)
                {
                    CurentMonetiz.Add(item.Name, item.Value);
                }

                BTNRevite.Visibility = Visibility.Collapsed;

                Cheackcash();
                RechangeNew();

                TextPlayerNewValue.Text = "";
                TextLeaderboardsNewValue.Text = "";
                TextApisNewValue.Text = "";
                TextStudiosNewValue.Text = "";
                TextLogsNewValue.Text = "";
            };


            BTNPay.MouseDown += async (s, obj) =>
            {
                if (NewMonetiz == CurentMonetiz)
                {
                    DashboardGame.Notifaction("Not Change", StatusMessage.Warrning);
                }
                else
                {
                    if (await DashboardGame.DialogYesNo($"\" {NewMonetiz["Cash"].AsInt32.ToString("#,##0") } \" Tomans will be deducted from your account credit \n Are you sure ? ") == MessageBoxResult.Yes)
                    {
                        var Detail = new BsonDocument
                                    {
                                        { "Leaderboards",NewMonetiz["Leaderboards"].AsInt32 + CurentMonetiz["Leaderboards"].AsInt32},
                                        { "Apis",NewMonetiz["Apis"].AsInt32 + CurentMonetiz["Apis"].AsInt32 },
                                        { "Studios",NewMonetiz["Studios"].AsInt32 + CurentMonetiz["Studios"].AsInt32 },
                                        { "Logs",NewMonetiz["Logs"].AsInt32 + CurentMonetiz["Logs"].AsInt32 },
                                        { "Players",NewMonetiz["Players"].AsInt32 + CurentMonetiz["Players"].AsInt32},
                                        { "Creator" ,SettingUser.CurentDetailStudio["Creator"]},
                                        { "Cash",CurentMonetiz["Cash"].AsInt32 }
                                    };


                        SDK.SDK_PageDashboards.DashboardGame.PageStudios.AddPayment(Detail, ResultPay =>
                        {
                            if (ResultPay)
                            {
                                DashboardGame.Notifaction("Tanks for buy", StatusMessage.Ok);
                                ReciveMonetize(PageMonetiz, new DependencyPropertyChangedEventArgs());

                                TextPlayerNewValue.Text = "";
                                TextLeaderboardsNewValue.Text = "";
                                TextApisNewValue.Text = "";
                                TextStudiosNewValue.Text = "";
                                TextLogsNewValue.Text = "";
                                TextTomanNumber.Text = "0";
                                BTNRevite.Visibility = Visibility.Collapsed;


                                //log
                                SDK.SDK_PageDashboards.DashboardGame.PageLog.AddLog("Payment", $"{NewMonetiz["Cash"].AsInt32.ToString("#,##0") } \" Tomans will be deducted from your account credit", NewMonetiz, false, result => { });
                            }
                            else
                            {
                                DashboardGame.Notifaction("Fail Pay", StatusMessage.Error);
                            }

                        });


                    }
                    else
                    {
                        DashboardGame.Notifaction("Reject Pay", StatusMessage.Error);
                    }

                }

            };


            TextToken.MouseDown += (s, e) =>
            {
                Clipboard.SetText(TextToken.Text);
                DashboardGame.Notifaction("Token Copied", StatusMessage.Ok);
            };
        }


        private void Close(object sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }


        //global
        private void ChangePage(object sender, RoutedEventArgs e)
        {
            CurentPage.Visibility = Visibility.Collapsed;
            CurentBTNHeader.BorderBrush = new SolidColorBrush(Colors.Transparent);

            switch ((sender as Button).Name)
            {
                case "BTNSetting":
                    {
                        CurentPage = PageSettings;
                        CurentBTNHeader = BTNSetting;
                    }
                    break;
                case "BTNMonetiz":
                    {
                        CurentPage = PageMonetiz;
                        CurentBTNHeader = BTNMonetiz;
                    }
                    break;
                default:
                    Debug.WriteLine("Not Set");
                    break;
            }

            CurentPage.Visibility = Visibility.Visible;
            CurentBTNHeader.BorderBrush = new SolidColorBrush(Colors.DarkOrange);

        }

        private void OpenState(object sender, MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.Status(
                result =>
                {
                    var Text = "";
                    foreach (var item in result)
                    {
                        Text += item.Name + ": " + item.Value.ToString() + "\n";
                    }

                    DashboardGame.Dialog(Text, "Server State");
                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Recive", StatusMessage.Error);
                });
        }

        private void ReciveMonetize(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender as Grid).Visibility == Visibility.Visible)
            {
                SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveMonetize(
                    result =>
                    {

                        //Fill Payment
                        ContentPlacePay.Children.Clear();
                        RecivePayments();

                        //fill CurentMonetiz  & Frist monetiz
                        CurentMonetiz.Clear();
                        FristMonetiz.Clear();
                        foreach (var item in result)
                        {
                            CurentMonetiz.Add(item.Name, item.Value);

                            FristMonetiz.Add(item.Name, item.Value);
                        }

                        NewMonetiz = new BsonDocument
                        {
                            {"Players",0 },
                            {"Leaderboards",0 },
                            {"Apis",0 },
                            {"Studios",0 },
                            {"Logs",0 },
                            {"Cash",0 }
                        };

                        ReChange();
                        Cheackcash();

                        //frist insert 
                        void ReChange()
                        {
                            //players
                            if (result["Players"].AsInt32 <= 999)
                            {
                                TextPlayerValue.Text = result["Players"].ToString();
                            }
                            else if (result["Players"].AsInt32 >= 1000 && result["Players"].AsInt32 <= 999999)
                            {
                                TextPlayerValue.Text = (result["Players"].AsInt32 / 1000).ToString() + "K";
                            }
                            else if (result["Players"].AsInt32 >= 1000000)
                            {
                                TextPlayerValue.Text = (result["Players"].AsInt32 / 1000000).ToString() + "M";
                            }

                            //Leaderboard
                            if (result["Leaderboards"].AsInt32 <= 999)
                            {
                                TextLeaderboardsValue.Text = result["Leaderboards"].ToString();
                            }
                            else if (result["Leaderboards"].AsInt32 >= 1000 && result["Leaderboards"].AsInt32 <= 999999)
                            {
                                TextLeaderboardsValue.Text = (result["Leaderboards"].AsInt32 / 1000).ToString() + "K";
                            }
                            else if (result["Leaderboards"].AsInt32 >= 1000000)
                            {
                                TextLeaderboardsValue.Text = (result["Players"].AsInt32 / 1000000).ToString() + "M";
                            }

                            //Apis
                            if (result["Apis"].AsInt32 <= 999)
                            {
                                TextApisValue.Text = result["Apis"].ToString();
                            }
                            else if (result["Apis"].AsInt32 >= 1000 && result["Apis"].AsInt32 <= 999999)
                            {
                                TextApisValue.Text = (result["Apis"].AsInt32 / 1000).ToString() + "K";
                            }
                            else if (result["Apis"].AsInt32 >= 1000000)
                            {
                                TextApisValue.Text = (result["Apis"].AsInt32 / 1000000).ToString() + "M";
                            }

                            //Studios
                            if (result["Studios"].AsInt32 <= 999)
                            {
                                TextStudiosValue.Text = result["Studios"].ToString();
                            }
                            else if (result["Studios"].AsInt32 >= 1000 && result["Studios"].AsInt32 <= 999999)
                            {
                                TextStudiosValue.Text = (result["Studios"].AsInt32 / 1000).ToString() + "K";
                            }
                            else if (result["Studios"].AsInt32 >= 1000000)
                            {
                                TextStudiosValue.Text = (result["Studios"].AsInt32 / 1000000).ToString() + "M";
                            }

                            //Logs
                            if (result["Logs"].AsInt32 <= 999)
                            {
                                TextLogsValue.Text = result["Logs"].ToString();
                            }
                            else if (result["Logs"].AsInt32 >= 1000 && result["Logs"].AsInt32 <= 999999)
                            {
                                TextLogsValue.Text = (result["Logs"].AsInt32 / 1000).ToString() + "K";
                            }
                            else if (result["Logs"].AsInt32 >= 1000000)
                            {
                                TextLogsValue.Text = (result["Logs"].AsInt32 / 1000000).ToString() + "M";
                            }


                            TextCash.Text = result["Cash"].AsInt32.ToString("#,##0") + " T";

                            if (result["Cash"] <= 0)
                            {
                                TextNoCash.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                TextNoCash.Visibility = Visibility.Collapsed;
                            }

                            Cheackcash();
                        }

                    },
                    () =>
                    {
                        DashboardGame.Notifaction("Faild Recive", Notifaction.StatusMessage.Error);
                    });
            }
        }

        void RecivePayments()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.RecivePaymentList(
                result =>
                {
                    Debug.WriteLine(result["Detail"].AsBsonArray.Count);

                    foreach (var item in result["Detail"].AsBsonArray)
                    {
                        ContentPlacePay.Children.Add(new ModelPayment(item.AsBsonDocument));
                    }
                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Recive List Payment", StatusMessage.Error);
                });
        }


        /// <summary>
        /// cheack cash and Disable btns
        /// </summary>
        void Cheackcash()
        {
            if (CurentMonetiz["Cash"].AsInt32 >= 10000)
            {
                BTNPlayer.Visibility = Visibility.Visible;
            }
            else
            {
                BTNPlayer.Visibility = Visibility.Collapsed;
            }

            if (CurentMonetiz["Cash"].AsInt32 >= 10000)
            {
                BTNLeaderboards.Visibility = Visibility.Visible;
            }
            else
            {
                BTNLeaderboards.Visibility = Visibility.Collapsed;

            }

            if (CurentMonetiz["Cash"].AsInt32 >= 20000)
            {
                BTNAPIs.Visibility = Visibility.Visible;
            }
            else
            {
                BTNAPIs.Visibility = Visibility.Collapsed;
            }


            if (CurentMonetiz["Cash"].AsInt32 >= 60000)
            {
                BTNStudio.Visibility = Visibility.Visible;
            }
            else
            {
                BTNStudio.Visibility = Visibility.Collapsed;
            }


            if (CurentMonetiz["Cash"].AsInt32 >= 30000)
            {
                BTNLogs.Visibility = Visibility.Visible;
            }
            else
            {
                BTNLogs.Visibility = Visibility.Collapsed;
            }

        }

        /// <summary>
        /// Set texts newvalue
        /// </summary>
        void RechangeNew()
        {
            TextPlayerNewValue.Text = " + " + NewMonetiz["Players"].ToString();
            TextLeaderboardsNewValue.Text = " + " + NewMonetiz["Leaderboards"].ToString();
            TextApisNewValue.Text = " + " + NewMonetiz["Apis"].ToString();
            TextStudiosNewValue.Text = " + " + NewMonetiz["Studios"].ToString();
            TextLogsNewValue.Text = " + " + NewMonetiz["Logs"].ToString();

            TextTomanNumber.Text = NewMonetiz["Cash"].AsInt32.ToString("#,##0");

        }

    }
}
