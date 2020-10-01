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
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages
{
    /// <summary>
    /// Interaction logic for SubPageSupport.xaml
    /// </summary>
    public partial class SubPageSupport : UserControl
    {
        public SubPageSupport()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }
    }
}
