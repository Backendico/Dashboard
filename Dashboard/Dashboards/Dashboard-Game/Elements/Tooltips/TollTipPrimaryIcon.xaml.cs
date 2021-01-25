using System.Windows;
using System.Windows.Controls;

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
