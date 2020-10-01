using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.GlobalElement;
using Dashboard.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PageAUT.Login
{
    public partial class Register : UserControl
    {

        public Register(Action InitDashbaord)
        {
            InitializeComponent();
            this.InitDashbaord = InitDashbaord;
        }

        private void _Login(object sender, MouseButtonEventArgs e)
        {
            var _parent = Parent as Grid;
            _parent.Children.Remove(this);
            _parent.Children.Add(new Login(InitDashbaord));
        }

        private void _Register(object sender, MouseButtonEventArgs e)
        {
            if (TextUsername.Text.Length >= 6 && TextPassword.Password.Length >= 6)
            {
                try
                {
                    new MailAddress(TextEmail.Text);

                    SDK.SDK_PageAUT.Register(TextUsername.Text, TextPassword.Password, TextEmail.Text, TextPhone.Text,
                        (Token) =>
                        {
                            DashboardGame.Notifaction("Registered", StatusMessage.Ok);

                            Settings.Default._id = Token;
                            Settings.Default.Save();


                            //remove page and Effect
                            var _parent = (Parent as Grid);
                            _parent.Children.Remove(this);
                            (_parent.FindName("PageDashboard") as Grid).Effect = null;
                            InitDashbaord();


                        },
                        () =>
                        {
                            DashboardGame.Notifaction("Faild to Register", StatusMessage.Error);
                        });
                }
                catch (Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, StatusMessage.Warrning);
                }

            }
            else
            {
                DashboardGame.Notifaction("Username or Password Short", StatusMessage.Error);
            }
        }

        private void CheackUsername(object sender, TextChangedEventArgs e)
        {
            var Text = sender as TextBox;

            if (Text.Text.Length >= 6)
            {
                SDK.SDK_PageAUT.CheackUsername(Text.Text, result =>
                {
                    if (result)
                    {
                        Text.Text += new Random().Next();
                        DashboardGame.Notifaction("Username available", StatusMessage.Warrning);
                    }

                    Text.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                });
            }
            else
            {
                Text.BorderBrush = new SolidColorBrush(Colors.Tomato);
            }
        }

        Action InitDashbaord;
    }
}
