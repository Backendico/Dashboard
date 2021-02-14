using Dashboard.Dashboards.Dashboard_Game.PageAUT.Recovery;
using Dashboard.Dashboards.Dashboard_Game.PageAUT.Register;
using Dashboard.GlobalElement;
using System.Diagnostics;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.Aut
{
    /// <summary>
    /// Interaction logic for PageLogin.xaml
    /// </summary>
    public partial class PageLogin : UserControl
    {
        public PageLogin()
        {
            InitializeComponent();

            BTNLogin.Worker += () =>
            {
                SDK.SDK_PageAUT.Login(TextboxUsername.Text, TextboxPassword.Text, (Result, Token) =>
                {
                    if (Result)
                    {
                        (Parent as Grid).Children.Add(new PageStudios());
                        (Parent as Grid).Children.Remove(this);
                    }
                    else
                    {
                        Debug.WriteLine("Notifacton Error Login");
                    }

                });

            };

            BTNRegister.Worker += () =>
            {
                (Parent as Grid).Children.Add(new PageRegister());
                (Parent as Grid).Children.Remove(this);
            };

            BTNRecovery.Worker += () =>
            {
                (Parent as Grid).Children.Add(new PageAccountRecovery());
                (Parent as Grid).Children.Remove(this);

            };
        }
    }
}
