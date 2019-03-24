using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;    // Added for FileNotFoundException
using System.Globalization;

namespace ChocAnCSharp
{
    //import java.util.Scanner;
    //import java.util.NoSuchElementException;
    //import java.util.Date;
    //import java.text.SimpleDateFormat;
    //import java.text.ParseException;
    //import java.text.NumberFormat;
    //import java.util.Locale;

    /** The UserInterface class encapsulates all the user interaction 
     *  with the system.
     *  This implementation uses the console for input and output.
     *  @author Jean Naude
     *  @version 1.0 March 2009
     */
    public class UserInterface
    {
        public int menu(string menuText)
        {
            try
            {

                message("\n");
                message(menuText);  //display menu text
                message("\n Choice: ");
                String input = read();
                int choice;
                bool res = int.TryParse(input, out choice); //read choice as an integer
                if (!res) //not a valid integer
                {
                    errorMessage("Please enter digits only.");
                    return menu(menuText);  //give the user another chance
                }
                return choice;
            }
            catch (FormatException)        //not a valid integer
            {
                errorMessage("Please enter digits only.");
                return menu(menuText);              //give the user another chance
            }
            catch (InvalidOperationException ex)
            {
                //End of input, either of a test case using an input file
                //or the user presses Ctrl + Z (Windows)
                //or Ctrl + D (Unix) to exit the system.
                throw ex;
            }
            catch (Exception ex)       //all other exceptions
            {
                errorMessage(ex.Message);
                return 0;
            }
        }

        /** Displays the message as an error message
	 *  @param msg the message to display
	 */
        private string read()
        {
            return Console.ReadLine();
        }

        private void write(string str)
        {
            Console.WriteLine(str);
        }

        public void errorMessage(string msg)
        {
            Console.WriteLine("\n\n***Error: " + msg + "\n");
            //Make sure the user takes notice.  Similar to modal
            //error dialog in a GUI
            Console.Write("Press enter to continue ...");
            Console.ReadLine();
        }//errorMessage

        /** Displays the message as a standard message on a new line.
         *  @param msg the message to display
         */

        public void message(string msg)
        {
            Console.Write("\n" + msg);
        }//message

        /** Displays a prompt to the user for a string 
	 *  and returns the user's input
	 *  @param prompt the prompt
	 *  @return the user input 
	 */
        public string promptForString(string prompt)
        {
            message(prompt);
            String input = read();
            return input;
        }//promptForString

        /** Diplays a prompt to the user for a long integer 
	 *  and returns the user's input
	 *  @param prompt the prompt
	 *  @return the user input 
	 */
        public long promptForLong(string prompt)
        {
            try
            {
                message(prompt);    //display prompt
                String input = read();
                long number;
                bool res = long.TryParse(input, out number);//convert to long
                if (!res) //not a valid long
                {
                    errorMessage("Please enter digits only.");
                    return promptForLong(prompt);  //give the user another chance
                }
                return number;
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
                return promptForLong(prompt);  //give the user another chance
            }
        }//getLong

        /** Diplays a prompt to the user for a date and returns the user's input
	 *  @param prompt the prompt
	 *  @return the user input 
	 */
        public DateTime promptForDate(string prompt)
        {
            try
            {
                message(prompt);  //display prompt
                String dateString = read();  //read input
                DateTime date;
                bool res = DateTime.TryParseExact(dateString, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None,
                                                    out date); //convert to a date
                if (!res)
                {
                    errorMessage("Invalid date. Please follow format given.");
                    return promptForDate(prompt); //give the user another chance
                }
                return date;
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
                return promptForDate(prompt); //give the user another chance
            }

        }//promptForDate

        /** Displays a prompt to the user for a double value and 
	 *  returns the user's input
	 *  @param prompt the prompt
	 *  @return the user input
	 */
        public double promptForDouble(string prompt)
        {
            try
            {
                message(prompt);
                String input = read();
                double value;
                bool res = double.TryParse(input, out value); //convert to a double
                if (!res) //not a valid double
                {
                    errorMessage("Please enter digits and at most one decimal point");
                    return promptForDouble(prompt);  //give the user another chance
                }
                return value;
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
                return promptForDouble(prompt);  //give the user another chance
            }
        }//promptForDouble

        /** Displays a prompt to the user for a double value, and returns the 
	 *  user's input. If the user gives no value, the default value is returned.
	 *  @param prompt the prompt
	 *  @param defaultValue the defaultValue
	 *  @return the user input or the defaultValue
	 */
        public double promptForDouble(string prompt, double defaultValue)
        {
            try
            {
                message(prompt);
                String input = read();
                //test for no value
                if (input != null && input.Length > 0)
                {
                    double value;
                    double.TryParse(input, out value); //convert to double
                    return value;
                }
                else return defaultValue;  //no value given -- return default value
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
                return promptForDouble(prompt);  //give the user another chance
            }
        }//promptForDouble

        /** Returns the value given formatted as currency
	 *  @param value the value to be formatted
	 *  @return the value as a currency string
	 */
        public String formatAsCurrency(double value)
        {
            decimal dvalue = Convert.ToDecimal(value);
            return value.ToString("C2");
        }//formatAsCurrency


        //******************instance variable	
        //private Scanner input;

        /** Required format for input of dates
	     */
        public static String DATE_FORMAT = "MM-dd-yyyy";
    }//class UserInterface

}
