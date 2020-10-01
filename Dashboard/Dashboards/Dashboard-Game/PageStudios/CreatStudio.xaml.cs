using Dashboard.GlobalElement;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.PageStudios
{
    /// <summary>
    /// Interaction logic for CreatStudio.xaml
    /// </summary>
    public partial class CreatStudio : UserControl
    {
        public CreatStudio()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }

        private void Install(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.CreatStudio(TextBoxNameStudio.Text, result =>
            {
                if (result)
                {
                    DashboardGame.Notifaction("Install Compellited", Notifaction.StatusMessage.Ok);
                }
                else
                {
                    DashboardGame.Notifaction("Install Faild", Notifaction.StatusMessage.Error);
                }

            });

        }

    }
}
