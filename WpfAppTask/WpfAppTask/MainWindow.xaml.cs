using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppTask.Context;
using WpfAppTask.DataAccess.Abstract;
using WpfAppTask.DataAccess.Concrete;
using WpfAppTask.Models;

namespace WpfAppTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDbContext context = new MyDbContext();

        IGenericRepositoryPattern<Category> _categories;
        IGenericRepositoryPattern<Product> _products;

        public MainWindow()
        {
            InitializeComponent();

            _categories = new GenericRepository<Category>();
            _products=new GenericRepository<Product>();

            //Add();

            //Eagle Loading
            datagrid_category.ItemsSource = _categories.GetAll().Include(c=>c.Products).ToList();
        }
        private void Add()
        {
            using (var ctx = new MyDbContext())
            {
               
                Product p1 = new Product() { Name = "Acer", Price = 1599.99m };
                Product p2 = new Product() { Name = "Iphone 12",Price = 3499.99m };
                Product p3 = new Product() { Name = "Galaxy S10",Price = 2899.99m };
                Product p4 = new Product() { Name = "Asus",Price = 2599.99m };
                ctx.Products.Add(p1);
                ctx.Products.Add(p2);
                ctx.Products.Add(p3);
                ctx.Products.Add(p4);

                Category c1 = new Category() { Name = "Notebook" };
                Category c2 = new Category() { Name = "Phone" };
          
                ctx.Categories.Add(c1);
                ctx.Categories.Add(c2);

                c1.Products.Add(p1);
                c1.Products.Add(p4);

                c2.Products.Add(p2);
                c2.Products.Add(p3);

                ctx.SaveChanges();
            }
        }

        private void btn_products_Click(object sender, RoutedEventArgs e)
        {
            var category = datagrid_category.SelectedItem as Category;
            var result = _products.Query(p => p.CategoryId == category.Id).Select(p=>new { p.Id,p.Name,p.Price,p.CategoryId}).ToList();
            datagrid_product.ItemsSource = result;
        }
    }
}
