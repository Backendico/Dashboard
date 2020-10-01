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

namespace Dashboard.Dashboards.Dashboard_Game.Notifaction
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class Dialog : UserControl
    {
       

        public Dialog(string _Message, string Header)
        {
            InitializeComponent();
            TextMessage.Text = _Message;

            DashboardGame.MainRoot.Children.Add(this);
            DashboardGame._Dashboard.Effect = new BlurEffect();
            TextHeader.Text = Header;
        }



   
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            DashboardGame.MainRoot.Children.Remove(this);
            DashboardGame._Dashboard.Effect = null;
        }
    }
}
