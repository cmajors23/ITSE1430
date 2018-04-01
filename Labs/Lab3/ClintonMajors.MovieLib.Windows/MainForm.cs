/*
 *  ITSE 1430 
 *  Clinton Majors
 *  Lab 3
*/
using ClintonMajors.MovieLib.Data.Memory;
using System;
using System.Windows.Forms;
using System.Linq;

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

            //"Add" the movie
            _database.Add(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))

                MessageBox.Show(message);

            RefreshUI();
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
            //Get selected product

            var movie = GetSelectedMovie();

            if (movie == null)

            {

                MessageBox.Show(this, "No movie selected", "Error",

                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            };



            EditMovie(movie);
        }

        private void OnMovieRemove(object sender, EventArgs e)
        {
            //Get selected product

            var movie = GetSelectedMovie();

            if (movie == null)

            {

                MessageBox.Show(this, "No product selected", "Error",

                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            };



            DeleteMovie(movie);
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
        private void DisplayError(string message)
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        #region Private Members



        //Helper method to handle deleting movies

        private void DeleteMovie(Movie movie)

        {

            if (!ShowConfirmation("Are you sure?", "Remove Movie"))

                return;



            //Remove movie

            _database.Remove(movie.Id);



            RefreshUI();

        }



        //Helper method to handle editing movies

        private void EditMovie(Movie movie)

        {

            var form = new MovieDetailForm(movie);

            var result = form.ShowDialog(this);

            if (result != DialogResult.OK)

                return;



            //Update the movie

            form.Movie.Id = movie.Id;

            _database.Update(form.Movie, out var message);

            if (!String.IsNullOrEmpty(message))

                MessageBox.Show(message);



            RefreshUI();

        }



        private Movie GetSelectedMovie()

        {

            //TODO: Use the binding source

            //Get the first selected row in the grid, if any

            if (dataGridView1.SelectedRows.Count > 0)

                return dataGridView1.SelectedRows[0].DataBoundItem as Movie;



            return null;

        }



        private void RefreshUI()

        {

            //Get movies

            var movies = _database.GetAll();

            //Bind to grid

            bindingSource1.DataSource = movies.ToList();

        }

        private Data.IMovieDatabase _database = new MemoryMovieDatabase();

        #endregion

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            EditMovie(movie);

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteMovie(movie);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditMovie(movie);
            };

        }
    }
}
