using Dashboard.GlobalElement;
using MongoDB.Bson;
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

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport
{
    /// <summary>
    /// Interaction logic for SubpageSupport.xaml
    /// </summary>
    public partial class SubpageSupport : UserControl
    {
        public SubpageSupport()
        {
            InitializeComponent();

            //init btnsener //send support
            BTNAddSupport.MouseDown += (o, s) =>
            {
                var Detailsupport = new BsonDocument
                {
                    { "Header" ,TextboxTitle.Text},
                    {"Priority",ComboBoxPriority.SelectedIndex },
                    {"Part",ComboBoxPart.SelectedIndex },
                };
                SDK.SDK_PageDashboards.DashboardGame.PageSupport.AddSupport(Detailsupport, result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Support Add \n You will receive the answer soon", Notifaction.StatusMessage.Ok);
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild add", Notifaction.StatusMessage.Error);
                    }

                });

            };
        }

        private void Close(object sender, EventArgs e)
        {
            DashboardGame.MainRoot.Children.Remove(this);
        }
    }
}
