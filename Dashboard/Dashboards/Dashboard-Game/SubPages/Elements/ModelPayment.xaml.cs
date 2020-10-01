using MongoDB.Bson;
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

            TextID.Text = DetailPay["ID"].ToString();
            TextCreated.Text = DetailPay["Created"].ToLocalTime().ToString();
            TextCash.Text = DetailPay["Cash"].ToString();
            TextCreator.Text = DetailPay["Creator"].ToString();

            BTNShowDetail.MouseDown += (obj, e) =>
            {
                var Message = "";
                foreach (var item in DetailPay)
                {
                    Message += item.Name +" : "+ item.Value.ToString() + "\n";
                }
                Message += "\n" + "In this transaction, the above information has replaced the values";

                DashboardGame.Dialog(Message, "Detail Pay");

            };
        }

    }
}
