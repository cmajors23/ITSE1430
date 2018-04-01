/*
 *  ITSE 1430 
 *  Clinton Majors
 *  Lab 3
*/
using System;
using System.Windows.Forms;

namespace ClintonMajors.MovieLib.Windows
{
     class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
