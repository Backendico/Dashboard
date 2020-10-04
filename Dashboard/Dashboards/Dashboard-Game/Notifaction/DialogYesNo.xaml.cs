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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Close (object sender, EventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }
    }
}
