using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Tooltips
{
    /// <summary>
    /// Interaction logic for ToolTipPrimary.xaml
    /// </summary>
    public partial class ToolTipPrimary : UserControl
    {
        public ToolTipPrimary()
        {
            InitializeComponent();

            BTNShowTooltip.MouseEnter += (s, e) =>
            {
                PanelTooltip.Visibility = Visibility.Visible;
            };
            BTNShowTooltip.MouseLeave += (s, e) =>
            {
                PanelTooltip.Visibility = Visibility.Collapsed;
            };
        }
    }
}
