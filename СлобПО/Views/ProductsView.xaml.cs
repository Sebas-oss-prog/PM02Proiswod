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
using System.Data;

namespace СлободскоеПО.Views
{
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            string query = "SELECT * FROM Товары";
            ProductsGrid.ItemsSource = Database.ExecuteQuery(query).DefaultView;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtCategory.Text) ||
                string.IsNullOrEmpty(txtUnit.Text) ||
                string.IsNullOrEmpty(txtPurchasePrice.Text) ||
                string.IsNullOrEmpty(txtSellingPrice.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            string query = $"INSERT INTO Товары (Наименование, Категория, ЕдиницаИзмерения, ЦенаЗакупки, ЦенаПродажи) " +
                         $"VALUES ('{txtName.Text}', '{txtCategory.Text}', '{txtUnit.Text}', " +
                         $"{txtPurchasePrice.Text.Replace(",", ".")}, {txtSellingPrice.Text.Replace(",", ".")})";

            Database.ExecuteNonQuery(query);
            LoadProducts();

            // Очистка полей
            txtName.Text = "";
            txtCategory.Text = "";
            txtUnit.Text = "";
            txtPurchasePrice.Text = "";
            txtSellingPrice.Text = "";
        }
    }
}
