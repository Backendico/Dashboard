using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop.Payments
{
    /// <summary>
    /// Interaction logic for ModelPayments.xaml
    /// </summary>
    public partial class ModelPayments : UserControl
    {
        public ModelPayments(BsonDocument DetailPayments)
        {
            InitializeComponent();
            TextToken.Text = DetailPayments["Token"].ToString();
            TextPlayer.Text = DetailPayments["Player"].ToString();
            TextDetail.Text = DetailPayments["Detail"].ToString();
            TextCreated.Text = DetailPayments["Created"].ToLocalTime().ToString();
            TextToken.MouseDown += GlobalElement.GlobalEvents.CopyText;
            TextPlayer.MouseDown += GlobalElement.GlobalEvents.CopyText;
        }
    }
}
