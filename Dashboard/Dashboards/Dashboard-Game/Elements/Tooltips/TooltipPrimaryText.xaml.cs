using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Tooltips
{
    /// <summary>
    /// Interaction logic for TooltipPrimaryText.xaml
    /// </summary>
    public partial class TooltipPrimaryText : UserControl
    {
        public TooltipPrimaryText()
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
