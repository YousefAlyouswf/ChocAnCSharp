using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    /** Entity class that models a ChocAn provider.  It is a subclass of Person.
 *  @author Jean Naude
 *  @version 1.0 March 2009
 */
    public class Provider : Person
    {
        //****************constructors

        /** Creates a default provider, allocating a unique number and setting all
         *  instance variables, except the name, to empty strings.
         */
        public Provider() : base()
        {
            //no default value for type
        }//Provider default constructor

        /** Creates a provider from a string representation of the provider.
         *  @param data the string representation
         *  @throws NumberFormatException if the provider number is not a valid 
         *          integer
         *  @throws IllegalArgumentException if any of the values are invalid.
         *  @throws ArrayIndexOutOfBoundsException if there are not enough values 
         *             in the string
         */
        public Provider(String data) //: base(data)
        {
            fromString(data);
        }//Provider constructor using string data


        //****************accessor method

        /** Returns the type of the provider as a single character
         *  @return the provider's type
         */
        public char getType()
        {
            return type;
        }//getType

        //****************mutator method

        /** Changes the type of the provider
         *  @param newType the new type of the provider
         *  @throws IllegalArgumentException if newType is not one of the 
         *             allowed type characters
         */
        public void setType(char newType)
        {
            if (PROVIDER_TYPES.IndexOf(newType) < 0)
                throw new System.ArgumentException(PROVIDER_TYPE_HELP);
            type = newType;
        }//setType

        //****************utility methods

        /** Returns a string representation of the provider consisting of the values,
          *  converted to strings, of all the instance variables separated by
          *  the character SEPARATOR.
          *  @return the string representation of the provider.
          */
        public override String toString()
        {
            return base.toString() + SEPARATOR + type;
        }//toString

        /** Changes all the instance variables to the values given by the string 
         *  representation of the provider.
         *  @param data the string representation of the provider
         *  @throws NumberFormatException if the provider number is 
         *             not a valid integer
         *  @throws IllegalArgumentException if any of the values are
         *             invalid.
         *  @throws IndexOutOfBoundsException if there are 
         *             not enough values in the string
         */
        new public void fromString(String data)
        {
            base.fromString(data);
            setType(data[data.Length - 1]);
        }//fromString

        /** Returns a string representation of the provider in a format that is
         *  suitable for text display.
         *  @return a formatted string representation of the provider
         */
        new public String toFormattedString()
        {
            return base.toFormattedString() + "\nType:           " + type;
        }//toFormattedString


        //****************instance variable
        private char type;  //The only allowable types are Dietitician (D), 
                            //Internist (I) and Exercise Specialist (E)

        //****************class variables
        private static String PROVIDER_TYPES = "DIE";
        /** Message giving the characters that are valid for the provider type
         */
        public static String PROVIDER_TYPE_HELP = "Provider type must be "
                    + "one of the following characters: D(ietitian), "
                    + "I(nternist) or E(xercise Specialist)";

    }//class Provider
}
