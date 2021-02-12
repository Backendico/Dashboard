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

namespace Dashboard.Dashboards.Pages.SubPages.PageKeyvalue
{
    /// <summary>
    /// Interaction logic for SubPageKeyvalue.xaml
    /// </summary>
    public partial class SubPageKeyvalue : UserControl
    {
        public SubPageKeyvalue()
        {
            InitializeComponent();

            BTNAddAchievement.Work += () =>
            {

            };

            BTNListView.Worker += () => {
                PanelGridView.Visibility = Visibility.Collapsed;
                PanelListView.Visibility = Visibility.Visible;

            };
            BTNGridView.Worker += () => {
                PanelGridView.Visibility = Visibility.Visible;
                PanelListView.Visibility = Visibility.Collapsed;
            };

        }
    }
}
