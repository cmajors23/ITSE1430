/*
 * ITSE 1430
 * Classwork
 */
using System;
using System.Windows.Forms;
using Nile.Data.Memory;

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

            RefreshUI();

        }

        private void RefreshUI()
        {
            var products = _database.GetAll();
        }

        //Just a method to play around with members of our Product class
        private void PlayingWithProductMembers ()
        { 
            //Create a new product
            var product = new Product();

            //Cannot use properties as out parameters
            Decimal.TryParse("123", out var price);
            product.Price = price;

            //Get the property Name, no need for a function
            var name = product.Name;
            //var name = product.GetName();

            //Set the property Name, Price and IsDiscontinued
            product.Name = "Product A";
            product.Price = 50;
            product.IsDiscontinued = true;

            //ActualPrice is calculated so you cannot set it
            //product.ActualPrice = 10;
            var price2 = product.ActualPrice;                
            
            //product.SetName("Product A");
            //product.Description = "None";

            //Validate the product
            var error = product.Validate();

            //Convert anything to a string
            var str = product.ToString();

            //Create another product
            var productB = new Product();
            //productB.Name = "Product B";
            //productB.SetName("Product B");
            //productB.Description = product.Description;            

            //Validate the new product
            error = productB.Validate();
        }

        #region Event Handlers

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;
            var form = new ProductDetailForm();
            //form.Text = "Add Product";

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //add to database
            _database.Add(form.Product, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            
        }

 


private void OnProductEdit( object sender, EventArgs e )

        {
            //get the first product
            var products = _database.GetAll();
            var product = (products.Length > 0) ? products[0] : null;
            if (product == null)
                return;

            var form = new ProductDetailForm();
            form.Text = "Add Product";
            //form.Product = _product; 

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            
            //editing the product
            _database.Edit(form.Product, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            //_products[index] = form.Product;
            
        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            //get the first product
            var products = _database.GetAll();
            var product = (products.Length > 0) ? products[0] : null;
            if (product == null)
                return;

            if (!ShowConfirmation("Are you sure?", "Remove Product"))
                return;
            
            
        }        
        
        private void OnHelpAbout( object sender, EventArgs e )
        {
            MessageBox.Show(this, "Not implemented", "Help About", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion
        private MemoryProductDatabase _database = new MemoryProductDatabase();

        private bool ShowConfirmation ( string message, string title )
        {
            return MessageBox.Show(this, message, title
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes;
        }

        private Product[] _products;

        private void MainForm_Load( object sender, EventArgs e )
        {

        }

        private void dataGridView1_CellContentClick( object sender, DataGridViewCellEventArgs e )
        {

        }
    }
}
