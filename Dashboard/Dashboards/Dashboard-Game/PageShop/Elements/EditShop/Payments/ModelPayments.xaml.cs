using MongoDB.Bson;
using System.Windows.Controls;

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
