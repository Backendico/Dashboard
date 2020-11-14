using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.Dashboards.Dashboard_Game.SubPages.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
            TextID.Text = SettingUser.CurentDetailStudio["Token"].ToString();
            TextToken.Text = SettingUser.CurentDetailStudio["Creator"].ToString();
            TextCreated.Text = SettingUser.CurentDetailStudio["Created"].ToLocalTime().ToString();
            TextDatabase.Text = SettingUser.CurentDetailStudio["Database"].ToString();


            //page Setting

            BTNState.MouseDown += (s, e) =>
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
              

            };

            BTNUpdate.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new SubPageUpdate.SubPageUpdate());
            };

            //Page Monetize
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
                    Debug.WriteLine(CurentMonetiz.ToString());
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

            BTNAddAchievements.MouseDown += (s, e) =>
            {
                if (CurentMonetiz["Cash"].AsInt32 - 10000 >= 0)
                {
                    CurentMonetiz["Achievements"] = CurentMonetiz["Achievements"].AsInt32 + 2;

                    NewMonetiz["Achievements"] = NewMonetiz["Achievements"].AsInt32 + 2;

                    NewMonetiz["Cash"] = NewMonetiz["Cash"].AsInt32 + 10000;

                    CurentMonetiz["Cash"] = CurentMonetiz["Cash"].AsInt32 - 10000;


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
                    {"Achievements",0 },
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
                TextLogsNewValue.Text = "";
                TextAchievementsNewValue.Text = "";
            };

            //acatin btn pay/save
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
                                        { "Leaderboards",CurentMonetiz["Leaderboards"].AsInt32},
                                        { "Apis",CurentMonetiz["Apis"].AsInt32 },
                                        { "Logs", CurentMonetiz["Logs"].AsInt32 },
                                        {"Achievements",CurentMonetiz["Achievements"].AsInt32 },
                                        { "Players", CurentMonetiz["Players"].AsInt32},
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
                                TextLogsNewValue.Text = "";
                                TextAchievementsNewValue.Text = "";
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

            BTNPayments.MouseDown += (s, e) =>
            {
                DoubleAnimation Anim = new DoubleAnimation(0, 400, TimeSpan.FromSeconds(0.3));

                Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
                Storyboard.SetTargetName(Anim, PaymentList.Name);

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(Anim);
                storyboard.Begin(this);

            };

            BTNCollaps.MouseDown += (s, e) =>
            {
                ClosePaymentList();
            };




            //page Charge Wallet
            BTNAddMoney.MouseDown += (s, e) =>
            {
                DoubleAnimation Anim = new DoubleAnimation(0, 400, TimeSpan.FromSeconds(0.3));

                Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
                Storyboard.SetTargetName(Anim, PanelChargeMoney.Name);

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(Anim);
                storyboard.Begin(this);

            };
            BTNCloseCharge.MouseDown += (s, e) =>
            {
                CloseCharge();
            };


            //Page Buy money
            BTNPaytoBank.MouseDown += (s, e) =>
            {
                try
                {
                    if (TextAmount_BackToBank.Text.Length < 5 && int.Parse(TextAmount_BackToBank.Text) < 10000)
                        throw new Exception("The amount is below 10,000 Rials");

                    if (TextName_BackToBank.Text.Length < 2)
                        throw new Exception("The name is short");

                    _ = long.Parse(TextPhone_BackToBank.Text);
                    _ = int.Parse(TextAmount_BackToBank.Text);
                    _ = new MailAddress(TextEamil_BackToBank.Text);
                    if (TextDesc_BackToBank.Text.Length <= 1)
                        TextDesc_BackToBank.Text = "N/A";

                    var Request = new BsonDocument()
                    {
                        {"amount",int.Parse(TextAmount_BackToBank.Text )},
                        {"order_id",new Random().Next() },
                        {"name",TextName_BackToBank.Text },
                        {"phone",TextPhone_BackToBank.Text },
                        {"mail",TextEamil_BackToBank.Text },
                        {"desc",TextDesc_BackToBank.Text },
                        {"callback","http://193.141.64.203/payments/callback" }
                    };

                    SDK.SDK_PageDashboards.DashboardGame.PageStudios.OpenGatePaye(Request, result =>
                    {
                        if (result.ElementCount >= 1)
                        {
                            var Detail = new BsonDocument()
                            {
                                {"Request",Request },
                                {"DetailPay",result },
                                {"Detail",new BsonDocument{ {"Studio",SettingUser.CurentDetailStudio["Database"] } ,{"Token",SettingUser.Token } } }
                            };

                            SDK.SDK_PageDashboards.DashboardGame.PageStudios.AddPaytoList(Detail, async ResultAdd =>
                             {
                                 if (ResultAdd)
                                 {
                                     DashboardGame.Notifaction("pls pay", StatusMessage.Ok);
                                     Process.Start(result["link"].AsString);

                                     var serilseDetailPay = new BsonDocument
                                     {
                                        {"id",result["id"] },
                                        {"order_id",Request["order_id"] }
                                     };

                                     var Result = new BsonDocument();

                                     while (true)
                                     {
                                         var Query = await SDK.SDK_PageDashboards.DashboardGame.PageStudios.CheackPay(serilseDetailPay);

                                         if (Query.ElementCount >= 1)
                                         {
                                             if (Query["status"].ToInt32() == 10)
                                             {
                                                 SDK.SDK_PageDashboards.DashboardGame.PageStudios.VerifiePay(serilseDetailPay, ResultVerifiePay =>
                                                 {
                                                     if (ResultVerifiePay)
                                                     {
                                                         DashboardGame.Notifaction("Payment is done.", StatusMessage.Ok);
                                                         ReciveMonetize(PageMonetiz, new DependencyPropertyChangedEventArgs());
                                                     }
                                                     else
                                                     {
                                                         DashboardGame.Notifaction("Payment failed.Please contact support", StatusMessage.Error);
                                                         ReciveMonetize(PageMonetiz, new DependencyPropertyChangedEventArgs());
                                                     }
                                                 });
                                                 break;
                                             }
                                             else if (Query["status"].ToInt32() == 7)
                                             {
                                                 DashboardGame.Notifaction("Payment is Cancel", StatusMessage.Error);
                                                 ReciveMonetize(PageMonetiz, new DependencyPropertyChangedEventArgs());
                                                 CloseCharge();
                                                 break;
                                             }

                                         }
                                         else
                                         {
                                             await Task.Delay(1);
                                         }
                                     }


                                 }
                                 else
                                 {
                                     DashboardGame.Notifaction("Faild Pay", StatusMessage.Error);
                                 }

                             });

                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Pay", StatusMessage.Error);
                        }

                    });

                }
                catch (Exception ex)
                {

                    DashboardGame.Notifaction(ex.Message, StatusMessage.Error);
                }
            };


            //copys
            TextToken.MouseDown += GlobalEvents.CopyText;
            TextDatabase.MouseDown += GlobalEvents.CopyText;
            TextID.MouseDown += GlobalEvents.CopyText;

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }


        void ClosePaymentList()
        {
            if (PaymentList.Height >= 1)
            {
                DoubleAnimation Anim = new DoubleAnimation(400, 0, TimeSpan.FromSeconds(0.3));

                Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
                Storyboard.SetTargetName(Anim, PaymentList.Name);

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(Anim);
                storyboard.Begin(this);
            }

        }

        void CloseCharge()
        {
            if (PanelChargeMoney.Height >= 1)
            {
                DoubleAnimation Anim = new DoubleAnimation(400, 0, TimeSpan.FromSeconds(0.3));

                Storyboard.SetTargetProperty(Anim, new PropertyPath("Height"));
                Storyboard.SetTargetName(Anim, PanelChargeMoney.Name);

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(Anim);
                storyboard.Begin(this);
            }

        }

        //global
        private void ChangePage(object sender, RoutedEventArgs e)
        {
            CurentPage.Visibility = Visibility.Collapsed;
            CurentBTNHeader.BorderBrush = new SolidColorBrush(Colors.Transparent);

            ClosePaymentList();
            CloseCharge();

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
                            {"Achievements",0 },
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


                            //achievements
                            if (result["Achievements"].AsInt32 <= 999)
                            {
                                TextAchievementsValue.Text = result["Achievements"].ToString();
                            }
                            else if (result["Achievements"].AsInt32 >= 1000 && result["Achievements"].AsInt32 <= 999999)
                            {
                                TextAchievementsValue.Text = (result["Achievements"].AsInt32 / 1000).ToString() + "K";
                            }
                            else if (result["Achievements"].AsInt32 >= 1000000)
                            {
                                TextAchievementsValue.Text = (result["Achievements"].AsInt32 / 1000000).ToString() + "M";
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
                        DashboardGame.Notifaction("Faild Recive", StatusMessage.Error);
                    });
            }
        }


        void RecivePayments()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.RecivePaymentList(
                result =>
                {
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
            //Player controller

            if (CurentMonetiz["Cash"].AsInt32 >= 10000)
            {
                BTNPlayer.Visibility = Visibility.Visible;
            }
            else
            {
                BTNPlayer.Visibility = Visibility.Collapsed;
            }

            //leaderboard controller
            if (CurentMonetiz["Cash"].AsInt32 >= 10000)
            {
                BTNLeaderboards.Visibility = Visibility.Visible;
            }
            else
            {
                BTNLeaderboards.Visibility = Visibility.Collapsed;

            }


            //Api controller
            if (CurentMonetiz["Cash"].AsInt32 >= 20000)
            {
                BTNAPIs.Visibility = Visibility.Visible;
            }
            else
            {
                BTNAPIs.Visibility = Visibility.Collapsed;
            }

            //Achievements controller
            if (CurentMonetiz["Cash"].AsInt32 >= 10000)
            {
                BTNAddAchievements.Visibility = Visibility.Visible;
            }
            else
            {
                BTNAddAchievements.Visibility = Visibility.Collapsed;
            }

            //log controller
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
            TextLogsNewValue.Text = " + " + NewMonetiz["Logs"].ToString();
            TextAchievementsNewValue.Text = "+" + NewMonetiz["Achievements"].ToString();

            TextTomanNumber.Text = NewMonetiz["Cash"].AsInt32.ToString("#,##0");

        }

    }

}
