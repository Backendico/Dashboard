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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Tooltips
{
    /// <summary>
    /// Interaction logic for TollTipPrimaryIcon.xaml
    /// </summary>
    public partial class TollTipPrimaryIcon : UserControl
    {
        public TollTipPrimaryIcon()
        {
            InitializeComponent();

            MouseEnter += (s, e) =>
            {
                PanelTooltip.Visibility = Visibility.Visible;
            };
            MouseLeave += (s, e) =>
            {
                PanelTooltip.Visibility = Visibility.Collapsed;
            };
        }
    }
}
