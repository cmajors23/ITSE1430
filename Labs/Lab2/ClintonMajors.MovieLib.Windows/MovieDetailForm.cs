using System;
using System.Windows.Forms;

namespace ClintonMajors.MovieLib.Windows
{
    public partial class MovieDetailForm : Form
    {
        #region Construction

        public MovieDetailForm()
        {
            InitializeComponent();
        }

        public MovieDetailForm(string title) : this() //: base()
        {

            Text = title;
        }

        public MovieDetailForm(Movie movie) : this("Edit Movie")
        {

            Movie = movie;
        }
        #endregion

        public Movie Movie { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
            if (Movie != null)
            {
                _txtName.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtLength.Text = Movie.Length.ToString();
                _chkIsOwned.Checked = Movie.IsOwned;
            };

         ValidateChildren();
        }

        #region Event Handlers
        private void _btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            // Create product
            var movie = new Movie();
            movie.Title = _txtName.Text;
            movie.Description = _txtDescription.Text;
            movie.Length = ConvertToLength(_txtLength);
            movie.IsOwned = _chkIsOwned.Checked;

            //Validate
          var message = movie.Validate();
            if (!String.IsNullOrEmpty(message))
            {
             DisplayError(message);
              return;
            };

            //Return from form
            Movie = movie;
            DialogResult = DialogResult.OK;

            Close();
        }

  

        #endregion

        private void DisplayError(string message)
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        private decimal ConvertToLength(TextBox control)
        {
            if (Decimal.TryParse(control.Text, out var length))
                return length;

            return -1;
        }

        private void _txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textbox = sender as TextBox;

            if (String.IsNullOrEmpty(textbox.Text))
            {
                _errorProvider.SetError(textbox, "Name is required");
                e.Cancel = true;
            }
            else
                _errorProvider.SetError(textbox, "");
        }

        private void _txtLength_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var textbox = sender as TextBox;
            var price = ConvertToLength(textbox);
            if (price < 0)
            {
                _errorProvider.SetError(textbox, "Price must be >= 0");
                e.Cancel = true;
            }
            else
                _errorProvider.SetError(textbox, "");
        }
    }
}


