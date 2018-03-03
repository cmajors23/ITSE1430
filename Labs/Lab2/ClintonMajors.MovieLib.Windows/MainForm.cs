using System;
using System.Windows.Forms;

namespace ClintonMajors.MovieLib.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMovieAdd(object sender, EventArgs e)
        {
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Movie");

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Add" the product
            _movie = form.Movie;
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
           if ( _movie == null )
               return;

            var form = new MovieDetailForm(_movie);
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Editing" the product
            _movie = form.Movie;
        }

        private void OnMovieRemove(object sender, EventArgs e)
        {
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;

            //Remove product
            _movie = null;
        }

        private void OnHelpAbout(object sender, EventArgs e)
        {
            var form = new AboutBox1();
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
        }

        private bool ShowConfirmation(string message, string title)
        {
            return MessageBox.Show(this, message, title
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private Movie _movie;
    }
}
