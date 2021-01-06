using MongoDB.Bson;
using System.Windows;
using System.Windows.Controls;

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
