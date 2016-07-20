using DistNorthwindWPF.ProductServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DistNorthwindWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductDTO product1, product2;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get & display product details for both product id's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            txtProduct1Details.Text = GetProduct(txtProductID1, ref product1);
            txtProduct2Details.Text = GetProduct(txtProductID2, ref product2);
        }

        /// <summary>
        /// Get product details for a single DTO
        /// </summary>
        /// <param name="txtProductID"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        private string GetProduct(TextBox txtProductID, ref ProductDTO product)
        {
            string result = "";
            try
            {
                int productID = Int32.Parse(txtProductID.Text);
                var client = new ProductServiceClient();
                product = client.GetProduct(productID);
                var sb = new StringBuilder();
                sb.Append("ProductID:" + product.ProductID.ToString() + "\n");
                sb.Append("ProductName:" + product.ProductName + "\n");
                sb.Append("UnitPrice:" +
                product.UnitPrice.ToString() + "\n");
                sb.Append("RowVersion:");
                foreach (var x in product.RowVersion.AsEnumerable())
                {
                    sb.Append(x.ToString());
                    sb.Append(" ");
                }
                result = sb.ToString();
            }
            catch (Exception ex)
            {
                result = "Exception: " + ex.Message.ToString();
            }
            return result;
        }

        /// <summary>
        /// Update the prices for both products (USES TRANSACTIONCSOPE)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdatePrice_Click(object sender, RoutedEventArgs e)
        {
            if (product1 == null)
            {
                txtUpdate1Results.Text = "Get product details first";
            }
            else if (product2 == null)
            {
                txtUpdate2Results.Text = "Get product details first";
            }
            else
            {
                bool update1Result = false, update2Result = false;
                using (var ts = new TransactionScope())
                {
                    txtUpdate1Results.Text = UpdatePrice(txtNewPrice1, ref product1, ref update1Result);
                    txtUpdate2Results.Text = UpdatePrice(txtNewPrice2, ref product2, ref update2Result);
                }
            }
        }

        /// <summary>
        /// Update the price details for a single DTO
        /// </summary>
        /// <param name="txtNewPrice"></param>
        /// <param name="product"></param>
        /// <param name="updateResult"></param>
        /// <returns></returns>
        private string UpdatePrice(TextBox txtNewPrice, ref ProductDTO product, ref bool updateResult)
        {
            string result = "";
            string message = "";

            try
            {
                product.UnitPrice = Decimal.Parse(txtNewPrice.Text);
                var client = new ProductServiceClient();
                updateResult = client.UpdateProduct(ref product, ref message);

                var sb = new StringBuilder();
                if (updateResult == true)
                {
                    sb.Append("Price updated to ");
                    sb.Append(txtNewPrice.Text.ToString());
                    sb.Append("\n");
                    sb.Append("Update result:");
                    sb.Append(updateResult.ToString());
                    sb.Append("\n");
                    sb.Append("Update message:");
                    sb.Append(message);
                    sb.Append("\n");
                    sb.Append("New RowVersion:");
                }
                else
                {
                    sb.Append("Price not updated to ");
                    sb.Append(txtNewPrice.Text.ToString());
                    sb.Append("\n");
                    sb.Append("Update result:");
                    sb.Append(updateResult.ToString());
                    sb.Append("\n");
                    sb.Append("Update message:");
                    sb.Append(message);
                    sb.Append("\n");
                    sb.Append("Old RowVersion:");
                }

                foreach (var x in product.RowVersion.AsEnumerable())
                {
                    sb.Append(x.ToString());
                    sb.Append(" ");
                }
                result = sb.ToString();

            }
            catch (Exception ex)
            {
                result = "Exception: " + ex.Message;
            }

            return result;
        }
    }
}
