using MongoDB.Bson;
using System;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.Elements
{
    /// <summary>
    /// Interaction logic for ModelPayment.xaml
    /// </summary>
    public partial class ModelPayment : UserControl
    {
        public ModelPayment(BsonDocument DetailPay)
        {
            InitializeComponent();
            try
            {

                TextID.Text = DetailPay["ID"].ToString();
                TextCreated.Text = DetailPay["Created"].ToLocalTime().ToString();
                TextCash.Text = DetailPay["Cash"].ToString();
                TextCreator.Text = DetailPay["Creator"].ToString();

            }
            catch (Exception)
            {
                TextID.Text = DetailPay["Request"]["order_id"].ToString();
                TextCreated.Text = DetailPay["Detail"]["Created"].ToLocalTime().ToString();
                TextCash.Text = DetailPay["Request"]["amount"].ToString();
                TextCreator.Text = DetailPay["Detail"]["Token"].ToString();

            }


            BTNShowDetail.MouseDown += (obj, e) =>
            {
                var Message = "";
                foreach (var item in DetailPay)
                {
                    Message += item.Name + " : " + item.Value.ToString() + "\n";
                }
                Message += "\n" + "In this transaction, the above information has replaced the values";

                DashboardGame.Dialog(Message, "Detail Pay");

            };
        }

    }
}
