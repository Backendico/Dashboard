using System;
using System.Collections.Generic;
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

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageHelps
{
    /// <summary>
    /// Interaction logic for SubPageHelp.xaml
    /// </summary>
    public partial class SubPageHelp : UserControl
    {
        public SubPageHelp()
        {
            InitializeComponent();
        }

        void Close(object sender,RoutedEventArgs e )
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }
    }
}
