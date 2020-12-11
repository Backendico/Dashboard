using Dashboard.GlobalElement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop.ModelProduct
{
    /// <summary>
    /// Interaction logic for ModelProduct.xaml
    /// </summary>
    public partial class ModelProduct : UserControl
    {
        IStoreSetting MainSetting;
        BsonDocument DetailProduct;

        public ModelProduct(BsonDocument DetailProduct, IStoreSetting setting)
        {
            InitializeComponent();
            
            MainSetting = setting;
            this.DetailProduct = DetailProduct;

            TextCreated.Text = this.DetailProduct["Created"].ToLocalTime().ToString();
            
            TextToken.Text = this.DetailProduct["Token"].ToString();
            TextToken.MouseDown += GlobalEvents.CopyText;

            TextName.Text = this.DetailProduct["Name"].ToString();
            TextAvatarLink.Text = this.DetailProduct["Avatar"].ToString();


            //action expiration
            IsExpiration.Checked += (s, e) =>
            {
                Calender.Visibility = Visibility.Visible;
                this.DetailProduct["IsExpiraton"] = true;
                UpdateProduct();
            };
            IsExpiration.Unchecked += (s, e) =>
            {
                Calender.Visibility = Visibility.Collapsed;
                this.DetailProduct["IsExpiraton"] = false;
                UpdateProduct();
            };

            //change name
            TextName.TextChanged += (s, e) =>
            {
                if (TextName.Text.Length>=1)
                {
                    this.DetailProduct["Name"] = TextName.Text;
                    UpdateProduct();
                }
                else
                {
                    DashboardGame.Notifaction("Name Is Short", Notifaction.StatusMessage.Error);
                }
            };

            //change avatar
            TextAvatarLink.LostFocus += (s, e) =>
            {


            };

            BTNSave.MouseDown += (s, e) =>
            {
                setting.Save();
            };
        }

        void UpdateProduct()
        {

            for (int i = 0; i < MainSetting.DetailStore["Products"].AsBsonArray.Count; i++)
            {
                if (MainSetting.DetailStore["Products"].AsBsonArray[i].AsBsonDocument["Token"].AsObjectId == DetailProduct["Token"])
                {
                    MainSetting.DetailStore["Products"].AsBsonArray[i] = DetailProduct;
                }
            }
        }
    }
}
