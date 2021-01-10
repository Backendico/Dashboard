using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Notifaction
{
    /// <summary>
    /// Interaction logic for DialogYesNo.xaml
    /// </summary>
    public partial class DialogYesNo : UserControl
    {

        MessageBoxResult ResultBox;

        public DialogYesNo(string Message)
        {
            InitializeComponent();
            TextMessage.Text = Message;
        }

        public async Task<MessageBoxResult> Result()
        {

            BTNAccept.MouseDown += (s, o) =>
            {
                ResultBox = MessageBoxResult.Yes;
            };

            BTNReject.MouseDown += (s, o) =>
            {
                ResultBox = MessageBoxResult.No;

            };

            while (ResultBox == MessageBoxResult.None)
            {
                await Task.Delay(1);
            }

            return ResultBox;
        }

        private void Close(object sender, EventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }
    }
}
