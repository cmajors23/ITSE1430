/*
 *  ITSE 1430 
 *  Clinton Majors
 *  Lab 3
*/
using System;
using System.Collections.Generic;


namespace ClintonMajors.MovieLib.Data
{
    public interface IMovieDatabase
    {
        /// <summary>Adds a Movie to the database.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <param name="message">The error message, if any.</param>
        /// <returns>The added movie.</returns>
        /// <remarks>
        /// Generates an error if:
        /// <paramref name="movie"/> is null or invalid.
        /// A movie with the same name already exists.
        /// </remarks>
        Movie Add(Movie movie, out string message );
    

        /// <summary>Gets a Movie to the database.</summary>
        /// <param name="movie">The movie to get.</param>
        /// <param name="message">The error message, if any.</param>
        /// <returns>The movie.</returns>
        /// <remarks>
        /// Generates an error if:
        /// <paramref name="movie"/> is null or invalid.
        /// A movie with the same name already exists.
        /// </remarks> 
        /// Movie Get(int id); 

        /// <summary>Gets all the movies.</summary>
        /// <returns>The list of movies.</returns>
        IEnumerable<Movie> GetAll();

        /// <summary>Removes a movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        /// <remarks>
        /// Returns an error if <paramref name="id"/> is less than or
        /// equal to zero.
        /// </remarks>
        void Remove(int id);

        /// <summary>Updates an existing product in the database.</summary>
        /// <param name="movie">The product to update.</param>
        /// <param name="message">The error message, if any.</param>
        /// <returns>The updated product.</returns>
        /// <remarks>
        /// Generates an error if:
        /// <paramref name="movie"/> is null or invalid.
        /// A movie with the same name already exists.
        /// The movie does not exist.
        /// </remarks>
        Movie Update(Movie movie, out string message);
        
    }
}
