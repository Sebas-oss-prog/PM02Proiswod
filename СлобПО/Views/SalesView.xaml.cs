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
    public partial class SalesView : UserControl
    {
        public SalesView()
        {
            InitializeComponent();
            LoadProducts();
            LoadSales();
        }

        private void LoadProducts()
        {
            string query = "SELECT КодТовара, Наименование FROM Товары";
            cmbProducts.ItemsSource = Database.ExecuteQuery(query).DefaultView;
            cmbProducts.DisplayMemberPath = "Наименование";
            cmbProducts.SelectedValuePath = "КодТовара";
        }

        private void LoadSales()
        {
            string query = "SELECT p.Наименование, s.Количество, s.ЦенаЗаЕдиницу, s.Сумма, s.ДатаПродажи " +
                          "FROM Продажи s JOIN Товары p ON s.КодТовара = p.КодТовара";
            SalesGrid.ItemsSource = Database.ExecuteQuery(query).DefaultView;
        }

        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProducts.SelectedValue == null ||
                string.IsNullOrEmpty(txtQuantity.Text) ||
                string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            int productId = (int)cmbProducts.SelectedValue;
            decimal quantity = decimal.Parse(txtQuantity.Text);
            decimal price = decimal.Parse(txtPrice.Text);
            decimal total = quantity * price;

            string query = $"INSERT INTO Продажи (КодТовара, Количество, ЦенаЗаЕдиницу, Сумма) " +
                         $"VALUES ({productId}, {quantity}, {price}, {total})";

            Database.ExecuteNonQuery(query);
            LoadSales();

            // Очистка полей
            txtQuantity.Text = "";
            txtPrice.Text = "";
        }
    }
}
