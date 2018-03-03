/*
 *  ITSE 1430 
 *  Clinton Majors
 *  Lab 2
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClintonMajors.MovieLib
{
    public class Movie
    {
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }
   
        public string Title
        {
            get { return _title ?? ""; }
            set { _title = value; }
        }
 
        public bool IsOwned { get; set; }
       
        public decimal Length { get; set; } = 0;

        public string Validate()
        {
            //Name is required
            if (String.IsNullOrEmpty(_title))
                return "Name cannot be empty";

            //Length >= 0
            if (Length < 0)
                return "Length must be >= 0";

            return "";
        }

        private string _title;
        private string _description;

    }
}
