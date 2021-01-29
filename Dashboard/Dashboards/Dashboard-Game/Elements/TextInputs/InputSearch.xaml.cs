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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.TextInputs
{
    /// <summary>
    /// Interaction logic for InputSearch.xaml
    /// </summary>
    public partial class InputSearch : UserControl
    {
        public InputSearch()
        {
            InitializeComponent();

            MouseEnter += (s, e) =>
            {
                Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#e5e5e5");
            };

            MouseLeave += (s, e) =>
            {
                Root.BorderBrush = new SolidColorBrush(Colors.Transparent);
            };

            MouseDown += (s, e) => {

                Debug.WriteLine("hi");
                Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");
            };
            TextArea.MouseDown+= (s, e) =>
            {
                Debug.WriteLine("hi");
                Root.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0F62FE");

            };
        }

        private void UserControl_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }
    }
}
