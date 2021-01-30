using Dashboard.GlobalElement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.DropDown
{
    /// <summary>
    /// Interaction logic for DropDownItem.xaml
    /// </summary>
    public partial class DropDownItem : UserControl
    {

        public DropDownItem()
        {
            InitializeComponent();
            MouseEnter += (s, e) =>
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#d0e2ff");
            };

            MouseLeave += (s, e) =>
            {
                Background = new SolidColorBrush(Colors.Transparent);
            };

            MouseDown += (s, e) =>
            {
                GlobalEvents.FindParent<DropDownPrimary>(this);
            };


        }



    }
}
