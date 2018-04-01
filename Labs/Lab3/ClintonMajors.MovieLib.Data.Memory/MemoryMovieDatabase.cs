/*
 *  ITSE 1430 
 *  Clinton Majors
 *  Lab 3
*/
using System;
using System.Collections.Generic;


namespace ClintonMajors.MovieLib.Data.Memory
{
    public class MemoryMovieDatabase : MovieDatabase
    {
        //Clone a movie
        private Movie Clone(Movie item)
        {
            var newMovie = new Movie();
            Copy(newMovie, item);

            return newMovie;
        }

        //Copy a movie from one object to another
        private void Copy(Movie target, Movie source)
        {
            target.Id = source.Id;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Length = source.Length;
            target.IsOwned = source.IsOwned;
        }

        protected override Movie AddCore(Movie movie)
        {
            // Clone the object
            movie.Id = _nextId++;
            _movies.Add(Clone(movie));

            // Return a copy
            return movie;
        }

        protected override IEnumerable<Movie> GetAllCore()
        {
            foreach (var movie in _movies)
            {
                if (movie != null)
                    yield return Clone(movie);
            }
        }

        protected override void RemoveCore(int id)
        {
            var existing = GetCore(id);
            if (existing != null)
                _movies.Remove(existing);
        }


        protected override Movie UpdateCore(Movie movie)
        {
            var existing = GetCore(movie.Id);

            // Clone the object
            Copy(existing, movie);

            //Return a copy
            return movie;
        }

        protected override Movie GetCore(int id)
        {
            foreach (var movie in _movies)
            {
                if (movie.Id == id)
                    return movie;
            };
            return null;
        }

        protected override Movie GetMovieByNameCore(string title)
        {
            foreach (var movie in _movies)
            {
                if (String.Compare(movie.Title, title, true) == 0)
                    return movie;
            };
            return null;
        }

        private readonly List<Movie> _movies = new List<Movie>();
        private int _nextId = 1;

      
    }
}
