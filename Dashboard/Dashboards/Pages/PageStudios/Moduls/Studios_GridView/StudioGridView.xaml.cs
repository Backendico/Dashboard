using MongoDB.Bson;
using System.Diagnostics;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.PageStudios.Moduls.Studios_GridView
{
    /// <summary>
    /// Interaction logic for StudioGridView.xaml
    /// </summary>
    public partial class StudioGridView : UserControl
    {
        public StudioGridView(BsonDocument Detail)
        {
            InitializeComponent();
            TextCreatedTime.Text = Detail["Created"].ToLocalTime().ToString();
            TextStudio.Text = Detail["Name"].ToString();
            TextType.Text = Detail["Type"].ToString();
            TextUniqeID.Text = Detail["Token"].ToString();
            TextTokenCreator.Text = Detail["Creator"].ToString();
            TextDatabase.Text = Detail["Database"].ToString();
            Debug.WriteLine(Detail);
        }
    }
}
