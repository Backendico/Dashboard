using System;
using System.Windows.Controls;

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
            MouseDown += (s, e) =>
            {
                Worker();
            };

        }


        public event Action Worker;
    }
}
