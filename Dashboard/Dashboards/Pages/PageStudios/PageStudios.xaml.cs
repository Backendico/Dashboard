using Dashboard.GlobalElement;
using MongoDB.Bson;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.Aut
{
    /// <summary>
    /// Interaction logic for PageStudios.xaml
    /// </summary>
    public partial class PageStudios : UserControl
    {
        public PageStudios(ObjectId Token)
        {
            InitializeComponent();

            SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveStudios(Result =>
            {
                
              
            });

        }


     
    }
}
