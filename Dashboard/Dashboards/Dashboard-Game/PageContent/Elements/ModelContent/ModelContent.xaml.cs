using Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonEditor;
using Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonView;
using Dashboard.Dashboards.Dashboard_Game.PageContent.Elements.EditContent;
using Dashboard.GlobalElement;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageContent.Elements.ModelContent
{

    public partial class ModelContent : UserControl, IEditContent
    {
        public BsonDocument DetailContent { get; set; }

        public ModelContent(BsonDocument DetailContent)
        {
            InitializeComponent();
            this.DetailContent = DetailContent;

            Init();


            //copy text token
            TextToken.MouseDown += GlobalEvents.CopyText;

            //action btn View
            BTNView.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new JsonView(DetailContent["Content"].AsBsonDocument));
            };

            BTNEdit.MouseDown += (s, e) =>
            {
                DashboardGame.Dashboard.Root.Children.Add(new EditContent.EditContent(this));
            };
        }

        public void Init()
        {
            TextNameStore.Text = DetailContent["Settings"]["Name"].ToString();
            TextToken.Text = DetailContent["Settings"]["Token"].ToString();
            TextElementCount.Text = DetailContent["Content"].AsBsonDocument.ElementCount.ToString();
            TextIndex.Text = DetailContent["Settings"]["Index"].ToString();
            TextCreated.Text = DetailContent["Settings"]["Created"].ToLocalTime().ToString();
        }

        public void Save()
        {

        }

        public void Delete()
        {

        }
    }

    public interface IEditContent
    {
        void Init();
        void Save();

        void Delete();

        BsonDocument DetailContent { get; set; }
    }
}
