using Dashboard.GlobalElement;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageAUT.Login
{
    /// <summary>
    /// Interaction logic for PageRecoveryPassword.xaml
    /// </summary>
    public partial class PageRecoveryPassword : UserControl
    {
        public PageRecoveryPassword()
        {
            InitializeComponent();

            //action btn login
            BTNLogin.MouseDown += (s, e) =>
            {
                var _parent = Parent as Grid;
                _parent.Children.Remove(this);
                _parent.Children.Add(new Login());
            };

            BTNStep1.MouseDown += (s, e) =>
            {
                try
                {

                    SDK.SDK_PageAUT.Recovery1(new System.Net.Mail.MailAddress(EmailStep1.Text), result =>
                    {
                        if (result)
                        {
                            Step1.Visibility = System.Windows.Visibility.Collapsed;
                            Step2.Visibility = System.Windows.Visibility.Visible;
                            DashboardGame.Notifaction($"Code send to Email {EmailStep1.Text}", Notifaction.StatusMessage.Ok);
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild send email recovery", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                catch (System.Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }

            };
            BTNStep2.MouseDown += (s, e) =>
            {
                try
                {
                    SDK.SDK_PageAUT.Recovery2(new System.Net.Mail.MailAddress(EmailStep1.Text), int.Parse(TextCode.Text), result =>
                    {
                        if (result)
                        {
                            Step2.Visibility = System.Windows.Visibility.Collapsed;
                            Step3.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            DashboardGame.Notifaction("Verification code not match ", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                catch (System.Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }
            };

            BTNStep3.MouseDown += (s, e) =>
            {
                try
                {
                    if (TextPassword1.Password != TextPassword2.Password)
                        throw new System.Exception("Password Not Match");

                    SDK.SDK_PageAUT.Recovery3(new System.Net.Mail.MailAddress(EmailStep1.Text), int.Parse(TextCode.Text), TextPassword2.Password, Result =>
                     {
                         if (Result)
                         {
                             DashboardGame.Notifaction("Password Changed", Notifaction.StatusMessage.Ok);

                             var _parent = Parent as Grid;
                             _parent.Children.Remove(this);
                             _parent.Children.Add(new Login());

                         }
                         else
                         {
                             DashboardGame.Notifaction("Faild Change ", Notifaction.StatusMessage.Error);
                         }
                     });
                }
                catch (System.Exception ex)
                {
                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }


            };

        }
    }
}
