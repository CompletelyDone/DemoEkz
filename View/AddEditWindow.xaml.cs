using Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private bool isEdit;
        private DBManager manager = new DBManager();
        private Product product;
        public AddEditWindow()
        {
            InitializeComponent();
            isEdit = false;
        }
        public AddEditWindow(string articleProduct)
        {
            InitializeComponent();
            isEdit = true;
            AddButton.Content = "Изменить";
            product = manager.db.Product.Where(o => o.Articul == articleProduct).FirstOrDefault();
            TBArticle.Text = product.Articul;
            TBCategory.Text = product.Category;
            TBCost.Text = product.Cost.ToString();
            TBDiscription.Text = product.Discription;
            TBManufacturer.Text = product.Manufacturer;
            TBUnit.Text = product.Unit.Title;
            TBUnit.IsEnabled = false;
            TBTitle.Text = product.Title;
            TBPhoto.Text = product.Photo;
        }

        private void AddEditButton(object sender, RoutedEventArgs e)
        {
            int tmpCost;
            if(isEdit && Int32.TryParse(TBCost.Text, out tmpCost))
            {
                product.Articul = TBArticle.Text;
                product.Manufacturer = TBManufacturer.Text;
                product.Discription = TBDiscription.Text;
                product.Category = TBCategory.Text;
                product.Title = TBTitle.Text;
                product.Cost = tmpCost;
                product.Photo = TBPhoto.Text;
                manager.db.SaveChanges();
                this.Close();
            }
            else if(Int32.TryParse(TBCost.Text, out tmpCost))
            {
                product = new Product();
                product.Articul = TBArticle.Text;
                product.Manufacturer = TBManufacturer.Text;
                product.Discription = TBDiscription.Text;
                product.Category = TBCategory.Text;
                product.Title = TBTitle.Text;
                product.Cost = tmpCost;
                product.Photo = TBPhoto.Text;
                manager.db.Product.Add(product);
                manager.db.SaveChanges();
                this.Close();
            }
        }

        private void CancellationButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
