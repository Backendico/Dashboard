using System;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Notifaction
{
    public partial class Dialog : UserControl
    {
        public Dialog(string _Message, string Header)
        {
            InitializeComponent();
            TextMessage.Text = _Message;

            DashboardGame.Dashboard.Root.Children.Add(this);
            DashboardGame.Dashboard.Blure(true);
            TextHeader.Text = Header;
        }




        private void Storyboard_Completed(object sender, EventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
            DashboardGame.Dashboard.Blure(false);
        }
    }
}
