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

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop
{
    /// <summary>
    /// Interaction logic for EditShop.xaml
    /// </summary>
    public partial class EditShop : UserControl
    {
        public EditShop()
        {
            InitializeComponent();
        }

        public void Close(object S, RoutedEventArgs Event)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }

        internal void ChangePage(object s,RoutedEventArgs eventArgs)
        {

        }
    }


}
