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

namespace Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.Player_GridShow.SeeMoreOption
{

    public partial class SeeMoreOption : UserControl
    {

        public string Text
        {
            get
            {
                return TextOption.Text;
            }
            set
            {
                TextOption.Text = value;
            }
        }
        public SeeMoreOption()
        {
            InitializeComponent();

            MouseEnter += (s, e) =>
            {

                Background = (Brush)new BrushConverter().ConvertFromString("#f4f4f4");
            };

            MouseLeave += (s, e) =>
            {

                Background = new SolidColorBrush(Colors.Transparent);
            };
        }


    }
}
