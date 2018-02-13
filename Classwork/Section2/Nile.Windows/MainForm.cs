using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            var product = new Product();

            Decimal.TryParse("123", out decimal price);
            product.Name = "Product A";
            product.Price = 50;
            product.IsDiscontinued = true;

            var price2 = product.ActualPrice;
            

            var name = product.Name;
            //var name = product.GetName();
            //var product = new Product();
            //product.SetName("Product A");
            // product.Description = "None";
           // var error = product.Validate();

            var str = product.ToString();

            var productB = new Product();
            //productB.SetName("Product B");
            // productB.Description = product.Description;

        }
    }
}
