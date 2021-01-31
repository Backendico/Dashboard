using Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonView;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System.Windows;
using System.Windows.Controls;

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

            BTNDelete.MouseDown += async (s, e) =>
             {
                 if (await DashboardGame.DialogYesNo("All information is deleted.\nAre you sure ? ") == MessageBoxResult.Yes)
                 {

                     Delete();
                 }
                 else
                 {
                     DashboardGame.Notifaction("Rejected", Notifaction.StatusMessage.Warrning);
                 }
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
            SDK.SDK_PageDashboards.DashboardGame.PageContent.EditContent(DetailContent["Settings"]["Token"].AsObjectId, DetailContent, result =>
            {
                if (result)
                {
                    Init();
                    DashboardGame.Notifaction("Saved", Notifaction.StatusMessage.Ok);
                }
                else
                {
                    DashboardGame.Notifaction("Faild Save", Notifaction.StatusMessage.Error);
                }
            });
        }

        public void Delete()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageContent.DeleteContent(DetailContent["Settings"]["Token"].AsObjectId, Result =>
            {
                if (Result)
                {
                    (Parent as WrapPanel).Children.Remove(this);
                    DashboardGame.Notifaction("Deleted", Notifaction.StatusMessage.Ok);
                }
                else
                {
                    DashboardGame.Notifaction("Faild Recieve", Notifaction.StatusMessage.Error);
                }

            });

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
