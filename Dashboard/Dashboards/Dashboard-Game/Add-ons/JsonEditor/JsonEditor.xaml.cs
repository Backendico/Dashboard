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

namespace Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonEditor
{

    public partial class JsonEditor : UserControl
    {
        public JsonEditor(BsonDocument JsonData)
        {
            InitializeComponent();

            PlaceElement.Children.Add(new ElementObject(JsonData));

            BTNAddElement.MouseDown += (s, e) =>
            {
                PlaceElement.Children.Add(new ElementString(new BsonElement("New Name", "New Value")));
            };

        }
    }

    public class Elements : Grid
    {

        internal new TextBox Name;
        internal TextBox Value;
        internal ComboBox Type;
        internal TextBlock ShowMore;


        public void Init(BsonElement Data)
        {
            Background = new SolidColorBrush(Colors.Transparent);

            Margin = new Thickness(0, 5, 0, 5);

            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            ColumnDefinitions.Add(new ColumnDefinition() { });
            ColumnDefinitions.Add(new ColumnDefinition() { });
            ColumnDefinitions.Add(new ColumnDefinition() { });


            var Delete = new TextBlock() { Cursor = Cursors.Hand, Visibility = Visibility.Collapsed, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, FontSize = 15, Text = "\xF78A", FontFamily = new FontFamily("Segoe MDL2 Assets"), Foreground = new SolidColorBrush(Colors.Tomato) };
            Children.Add(Delete);
            SetColumn(Delete, 0);

            var ShowMore = new TextBlock() { Cursor=Cursors.Hand, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Visibility = Visibility.Collapsed, FontSize = 15, Text = "\xE974", FontFamily = new FontFamily("Segoe MDL2 Assets") };
            var Show = false;
            ShowMore.MouseDown += (s, e) =>
            {
                if (!Show)
                {
                    Show = true;
                    ShowMore.Text = "\xE972";
                }
                else
                {
                    Show = false;
                    ShowMore.Text = "\xE974";
                }
            };

            Children.Add(ShowMore);
            SetColumn(ShowMore, 1);



            var Name = new TextBox() { Margin = new Thickness(0, 0, 5, 0), BorderBrush = new SolidColorBrush(Colors.Transparent), MinWidth = 100, Text = Data.Name, Foreground = new SolidColorBrush(Colors.Black) };
            Children.Add(Name);
            SetColumn(Name, 2);

            var Value = new TextBox() { Margin = new Thickness(0, 0, 5, 0), BorderBrush = new SolidColorBrush(Colors.Transparent), MinWidth = 100, Text = Data.Value.ToString(), Foreground = new SolidColorBrush(Colors.Black) };
            SetColumn(Value, 3);
            Children.Add(Value);

            var Type = new ComboBox();
            Type.Items.Add(BsonType.Array);
            Type.Items.Add(BsonType.Binary);
            Type.Items.Add(BsonType.Boolean);
            Type.Items.Add(BsonType.DateTime);
            Type.Items.Add(BsonType.Decimal128);
            Type.Items.Add(BsonType.Document);
            Type.Items.Add(BsonType.Double);
            Type.Items.Add(BsonType.EndOfDocument);
            Type.Items.Add(BsonType.Int32);
            Type.Items.Add(BsonType.Int64);
            Type.Items.Add(BsonType.JavaScript);
            Type.Items.Add(BsonType.JavaScriptWithScope);
            Type.Items.Add(BsonType.MaxKey);
            Type.Items.Add(BsonType.MinKey);
            Type.Items.Add(BsonType.Null);
            Type.Items.Add(BsonType.ObjectId);
            Type.Items.Add(BsonType.RegularExpression);
            Type.Items.Add(BsonType.String);
            Type.Items.Add(BsonType.Symbol);
            Type.Items.Add(BsonType.Timestamp);
            Type.Items.Add(BsonType.Undefined);
            Children.Add(Type);
            SetColumn(Type, 4);
            Type.SelectedItem = Data.Value.BsonType;


            //actions
            MouseEnter += (s, e) =>
            {
                Delete.Visibility = Visibility.Visible;
                Background = new SolidColorBrush(Colors.WhiteSmoke);
            };

            MouseLeave += (s, e) =>
            {
                Delete.Visibility = Visibility.Collapsed;
                if (!Show)
                {
                    Background = new SolidColorBrush(Colors.Transparent);
                }
            };


            this.Name = Name;
            this.Value = Value;
            this.Type = Type;
            this.ShowMore = ShowMore;
        }

    }

    public class ElementObject : StackPanel
    {
        public ElementObject(BsonDocument Data)
        {
            foreach (var item in Data.AsBsonDocument)
            {

                switch (item.Value.BsonType)
                {
                    case BsonType.EndOfDocument:
                        break;
                    case BsonType.Double:
                        break;
                    case BsonType.String:
                        {
                            Children.Add(new ElementString(item));
                        }
                        break;
                    case BsonType.Document:
                        break;
                    case BsonType.Array:
                        {
                            Children.Add(new ElementsArray(item));
                        }
                        break;
                    case BsonType.Binary:
                        break;
                    case BsonType.Undefined:
                        break;
                    case BsonType.ObjectId:
                        break;
                    case BsonType.Boolean:
                        break;
                    case BsonType.DateTime:
                        break;
                    case BsonType.Null:
                        break;
                    case BsonType.RegularExpression:
                        break;
                    case BsonType.JavaScript:
                        break;
                    case BsonType.Symbol:
                        break;
                    case BsonType.JavaScriptWithScope:
                        break;
                    case BsonType.Int32:
                        break;
                    case BsonType.Timestamp:
                        break;
                    case BsonType.Int64:
                        break;
                    case BsonType.Decimal128:
                        break;
                    case BsonType.MinKey:
                        break;
                    case BsonType.MaxKey:
                        break;
                    default:
                        break;
                }
            }

        }

    }

    public class ElementString : Elements
    {
        public ElementString(BsonElement Data)
        {
            Init(Data);
        }
    }

    public class ElementsArray : Elements
    {
        public ElementsArray(BsonElement Data)
        {
            Init(Data);

            Value.Text = "Array";
            Value.IsEnabled = false;
            ShowMore.Visibility = Visibility.Visible;
        }
    }




}
