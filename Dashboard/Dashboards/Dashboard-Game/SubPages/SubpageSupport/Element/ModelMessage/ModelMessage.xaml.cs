using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element.ModelMessage
{
    /// <summary>
    /// Interaction logic for ModelMessage.xaml
    /// </summary>
    public partial class ModelMessage : UserControl
    {
        public ModelMessage(BsonDocument DetailMessage)
        {
            InitializeComponent();
            TextTime.Text = DetailMessage["Created"].ToLocalTime().ToString();

            switch (DetailMessage["Sender"].ToInt32())
            {

                case 0:
                    TextSender.Text = "\xE77B ";
                    break;
                case 1:
                    TextSender.Text = "\xE95B";
                    break;
                default:
                    TextSender.Text = "Not Set";
                    break;
            }

            switch (DetailMessage["Sender"].ToInt32())
            {
                case 0:
                    HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case 1:
                    HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                default:
                    HorizontalAlignment = HorizontalAlignment.Stretch;
                    break;
            }



            TextMessage.Text = DetailMessage["Message"].AsString;

        }
    }
}
