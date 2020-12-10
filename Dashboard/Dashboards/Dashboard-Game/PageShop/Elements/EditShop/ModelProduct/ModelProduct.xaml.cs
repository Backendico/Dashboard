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

namespace Dashboard.Dashboards.Dashboard_Game.PageShop.Elements.EditShop.ModelProduct
{
    /// <summary>
    /// Interaction logic for ModelProduct.xaml
    /// </summary>
    public partial class ModelProduct : UserControl
    {
        public ModelProduct(BsonDocument DetailProduct, EditProducts Edit)
        {
            InitializeComponent();

            Debug.WriteLine(DetailProduct);
            TextName.Text = DetailProduct["Name"].ToString();
            TextAvatarLink.Text = DetailProduct["Avatar"].ToString();


      

            Edit.Save();
        }


    }
}
