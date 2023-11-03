using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBManager manager;
        public MainWindow()
        {
            InitializeComponent();
            manager = new DBManager();
            ProductList.ItemsSource = manager.db.Product.Include(o => o.Unit).ToList();
        }
        public void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            AddEditWindow window = new AddEditWindow();
            window.Show();
        }
        public void OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            Product product = ProductList.SelectedItem as Product;
            if(product != null)
            {
                AddEditWindow window = new AddEditWindow(product.Articul);
                window.Show();
            }
        }
        public void OnRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            Product product = ProductList.SelectedItem as Product;
            if(product != null )
            {
                manager.db.Product.Remove(product);
                manager.db.SaveChanges();
            }
        }
    }
}
