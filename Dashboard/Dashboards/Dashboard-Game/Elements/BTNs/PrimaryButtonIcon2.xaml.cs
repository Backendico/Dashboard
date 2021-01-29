using Dashboard.Dashboards.Dashboard_Game.Elements.Icons;
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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for PrimaryButtonIcon2.xaml
    /// </summary>
    public partial class PrimaryButtonIcon2 : UserControl
    {
        public IconType Icon
        {
            set
            {
                TextIcon.Text = new Icons.Icons()[value];
                TextTooltip.Text = value.ToString();
            }
        }


        public PrimaryButtonIcon2()
        {
            InitializeComponent();

            GotFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(2);
            };

            LostFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(0);
            };
            MouseEnter += (s, e) =>
            {
                Tooltip.Visibility = Visibility.Visible;
            };
            MouseLeave += (s, e) => { Tooltip.Visibility = Visibility.Collapsed; };
        }
    }
}
