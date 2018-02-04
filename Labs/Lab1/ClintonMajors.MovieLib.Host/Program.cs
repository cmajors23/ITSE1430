/*
 * ITSE 1430
 * 
 * Clinton Majors
 * 
 * 02/04/2018
 * 
 * Lab1
 * 
 * Section 1
 */


using System;
using System.Collections.Generic;


namespace ClintonMajors.MovieLib.Host
{
    class Program
    {
        static void Main( string[] args )
        {
            bool quit = false;
            while (!quit)
            {
                bool isEqual = quit.Equals(10);

                //Display menu
                char choice = DisplayMenu();


                //Process menu selection
                switch (Char.ToUpper(choice))
                {

                    //case 'L':
                    case 'L':
                    ListMovies(); break;

                    //case 'A':
                    case 'A':
                    AddMovie(); break;

                    //case 'R':
                    case 'R':
                    RemoveMovie(); break;

                    //case 'q':
                    case 'Q':
                    quit = true; break;
                };
            };
        }

 



        //Data for a Movie
        static List<string> _title = new List<string>();
        static List<decimal> _length = new List<decimal>();
        static List<string> _description = new List<string>();
        static List<bool> _owned = new List<bool>();


        private static void AddMovie()
        {
            Console.Clear();
            //Get Title
            string title = ReadString("Enter Title: ", true);
            // if title already exists in list remove from all lists and add new entry
            if (_title.Contains(title.ToString()))
            {
               
                int count = (_title.IndexOf(title.ToString()));
                _title.RemoveAt(count);
                _length.RemoveAt(count);
                _description.RemoveAt(count);
                _owned.RemoveAt(count);

                _title.Add(title);

            } else
            {

                _title.Add(title);
            }

            //Get description
            string description = ReadString("Enter an optional description: ", false);
            //check if description is null and if it is place "empty" string to value to ensure uniform size of all lists
            if (description == null)
            {
                description = "empty";
                _description.Add(description);
            } else
            {
                _description.Add(description);
            }

            //Get Length
            decimal length = ReadDecimal("Enter the optional length (in minutes): ", 0);
            _length.Add(length);

            //Get owned value
            string owned = ReadString("Do you own this movie(Y/N)?: ", true);

            //Convert string to bool and add to owned list
            if (owned.ToLower() == "y" || owned.ToLower() == "yes")
            {
                bool b_owned = true;
                _owned.Add(b_owned);

            } else
            {
                bool b_owned = false;
                _owned.Add(b_owned);
            }
            

 
            
            
        }

        private static void ListMovies()
        {
            Console.Clear();
            //Are there any Movies?


            if (_title.Count > 0)
            {
                // List every item in every list I know you dont like for loops but this is the only way I know how to do it.
                for (int i = 0; i < _title.Count; i++)
                {

                    Console.WriteLine(_title[i]);
                    Console.WriteLine(_description[i]);
                    Console.WriteLine("Run length = " + _length[i] + "mins");
                    if (_owned[i] == true)
                    {
                        Console.WriteLine("Status = Owned");
                    }else
                    {
                        Console.WriteLine("Status = Not Owned");
                    }
                    
                }
            } else
            {

                Console.WriteLine("No products");
                
            }

            Console.WriteLine("Press Enter To Continue");
            string input = Console.ReadLine();
        }

        private static void RemoveMovie()
        {
            Console.Clear();

            //Get name
            string Dtitle = ReadString(message: "Enter name: ", isRequired: true);

            //Confirm Removal
            string confirm = ReadString("Are you sure you want to delete the movie (Yes/No)?", false);

            if (confirm.ToLower() == "y" || confirm.ToLower() == "yes")
            {
                bool b_confirm = true;
                if (_title.Contains(Dtitle.ToString()))
                {
                    int count = _title.IndexOf(Dtitle.ToString());
                    _title.RemoveAt(count);
                    _length.RemoveAt(count);
                    _description.RemoveAt(count);
                    _owned.RemoveAt(count);

                    Console.WriteLine("Movie successfully removed.");
                    Console.WriteLine("Press Enter To Continue");
                    string inputl = Console.ReadLine();

                } else
                {
                    Console.WriteLine("Movie title specified does not exist in database");
                    Console.WriteLine("Press Enter To Continue");
                    string inputl = Console.ReadLine();
                }
            } else
            {
                bool b_confirm = false;

            }
        }
            
        private static char DisplayMenu()
        {


            do

            {
              Console.Clear();

                Console.WriteLine("L)ist Movies");

                Console.WriteLine("A)dd Movie");

                Console.WriteLine("R)emove Movie");

                Console.WriteLine("Q)uit\n");



                string input = Console.ReadLine();



                //Remove whitespace

                input = input.Trim();

                //input.ToLower();

                input = input.ToUpper();

                //if (input == "L")

                if (String.Compare(input, "L", true) == 0)
               
                     return input[0];

                 else if (input == "A")
                
                    return input[0];

                 else if (input == "R")
                

                    return input[0];

                 else if (input == "Q")
                

                    return input[0];
                


                    Console.WriteLine("Please choose a valid option");
                

            } while (true);
        }
        
        private static string ReadString( string message, bool isRequired )

        {

            do

            {

                Console.Write(message);



                string value = Console.ReadLine();



                //If not required or not empty

                if (!isRequired || value != "")

                    return value;



                Console.WriteLine("Value is required");

            } while (true);

        }

        private static decimal ReadDecimal( string message, decimal minValue )

        {

            do

            {

                Console.Write(message);



                string value = Console.ReadLine();



                //if (Decimal.TryParse(value, out decimal result) && result >= minValue)

                //    return result;



                if (Decimal.TryParse(value, out decimal result))

                {

                    //If not required or not empty

                    if (result >= minValue)

                        return result;

                };



                //Formatting strings

                //Console.WriteLine("Value must be >= {0}", minValue);

                string msg = String.Format("Value must be >= {0}", minValue);

                Console.WriteLine(msg);

            } while (true);

        }
    }

}
