using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios;
using Dashboard.GlobalElement;
using Dashboard.Properties;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageAUT.Login
{
    public partial class Login : UserControl
    {

        public Login()
        {
            InitializeComponent();

            //blure Controll
            DashboardGame.Dashboard.Blure(true);
            Unloaded += (s, e) =>
            {
                DashboardGame.Dashboard.Blure(false);
            };

            //action btn login
            BTNLogin.MouseDown += (s, e) =>
            {
                BTNLogin.IsEnabled = false;

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
                            DashboardGame.Dashboard.Root.Children.Remove(this);
                            DashboardGame.Dashboard.Blure(true);

                            DashboardGame.Dashboard.Root.Children.Add(new SubPageStudios());
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild Login", Notifaction.StatusMessage.Error);
                            BTNLogin.IsEnabled = true;
                        }
                    });
                }
                else
                {
                    DashboardGame.Notifaction("Username or Password Short", Notifaction.StatusMessage.Error);
                    BTNLogin.IsEnabled = true;
                }

            };

            //action btn register
            BTNRegister.MouseDown += (s, e) =>
            {
                var _Parent = (Parent as Grid);
                _Parent.Children.Remove(this);
                _Parent.Children.Add(new Register());
            };

            //action  btn recovery
            BTNRecovery.MouseDown += (s, e) =>
            {
                var _Parent = (Parent as Grid);
                _Parent.Children.Remove(this);
                _Parent.Children.Add(new PageRecoveryPassword());

            };
        }


    }
}
