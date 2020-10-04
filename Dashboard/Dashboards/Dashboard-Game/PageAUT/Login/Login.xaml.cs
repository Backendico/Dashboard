using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios;
using Dashboard.GlobalElement;
using Dashboard.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PageAUT.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        public Login(Action InitDashbaord)
        {
            InitializeComponent();
            this.InitDashbaord = InitDashbaord;
        }

        private void _Login(object sender, MouseButtonEventArgs e)
        {
            if (TextUsername.Text.Length >= 6 && TextPassword.Password.Length >= 6)
            {
                SDK.SDK_PageAUT.Login(TextUsername.Text, TextPassword.Password, (resul, Token) =>
                {
                    if (resul)
                    {
                        //login
                        DashboardGame.Notifaction("Logined", Notifaction.StatusMessage.Ok);
                        Settings.Default._id = Token;
                        Settings.Default.Save();

                        //remove page and Effect
                        var _parent = (Parent as Grid);
                        _parent.Children.Remove(this);
                        (_parent.FindName("PageDashboard") as Grid).Effect = null;

                        DashboardGame.Dashboard.Root.Children.Add(new SubPageStudios());

                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Login", Notifaction.StatusMessage.Error);

                    }
                });

            }
            else
            {
                DashboardGame.Notifaction("Username or Password Short", Notifaction.StatusMessage.Error);
            }
        }

        private void _Register(object sender, MouseButtonEventArgs e)
        {
            var _Parent = (Parent as Grid);
            _Parent.Children.Remove(this);
            _Parent.Children.Add(new Register(InitDashbaord));

        }

        Action InitDashbaord;
    }
}
