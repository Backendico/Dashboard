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

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Links
{
  
    public partial class LinksPrimary : UserControl
    {
        public string TextLinks
        {
            get { return _TextLink; }
            set
            {
                TextLink.Text = value;
                _TextLink = value;
            }
        }

        string _TextLink;
        public LinksPrimary()
        {
            InitializeComponent();
        }
    }
}
