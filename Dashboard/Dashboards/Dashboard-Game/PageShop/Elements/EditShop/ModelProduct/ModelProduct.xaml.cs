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

            TextName.Text = this.DetailProduct["Name"].ToString();
            TextAvatarLink.Text = this.DetailProduct["Avatar"].ToString();

            IsExpiration.Checked += (s, e) =>
            {
                Calender.Visibility = Visibility.Visible;
                UpdateProduct();
            };
            IsExpiration.Unchecked += (s, e) =>
            {
                Calender.Visibility = Visibility.Collapsed;
                UpdateProduct();
            };


        }

        void UpdateProduct()
        {

            foreach (var item in MainSetting.DetailStore["Products"].AsBsonArray)
            {
                if (item.AsBsonDocument["Token"].AsObjectId==DetailProduct["Token"])
                {
                    Debug.WriteLine("hi");
                }
            }
        }


    }
}
