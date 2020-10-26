using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageUpdate
{
    /// <summary>
    /// Interaction logic for SubPageUpdate.xaml
    /// </summary>
    public partial class SubPageUpdate : UserControl
    {
        public SubPageUpdate()
        {
            InitializeComponent();
        }

        public void Close(object Sender, RoutedEventArgs E)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);

            Version version = new Version();

            //Debug.WriteLine(Application.ResourceAssembly.GetName().Version is version) ;
        }
    }
}
