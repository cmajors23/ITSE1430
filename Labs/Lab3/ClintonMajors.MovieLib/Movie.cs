/*
 *  ITSE 1430 
 *  Clinton Majors
 *  Lab 2
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClintonMajors.MovieLib
{
    public class Movie : IValidatableObject
    {
        /// <summary>Gets or sets the product ID.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }

        /// <summary>Gets or sets the Title.</summary>
        /// <value></value>
        public string Title
        {
            get { return _title ?? ""; }
            set { _title = value; }
        }

        /// <summary>Determines if the movie is owned.</summary>
        public bool IsOwned { get; set; }

        /// <summary>Gets or sets the Length of the movie.</summary>
        public decimal Length { get; set; } = 0;

        /// <summary>Validate the product.</summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation results.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            //Title is required
            if (String.IsNullOrEmpty(_title))
                errors.Add(new ValidationResult("Name cannot be empty",
                             new[] { nameof(Title) }));

            //Length >= 0
            if (Length < 0)
                errors.Add(new ValidationResult("Length must be >= 0",
                            new[] { nameof(Length) }));

            return errors;
        }

        private string _title;
        private string _description;

    }
}
