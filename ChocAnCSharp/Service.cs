using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class Service
    {
        //**********************constructors

        /** Creates a new default service with default values
         */
        public Service()
        {
        }//default constructor


        /** Creates a new service with the given values
         *  @param aCode the service code
         *  @param aName the service name
         *  @param aFee the service fee
         *  @throws IllegalArgumentException if any of the values are invalid
         */
        public Service(String aCode, String aName, double aFee)
        {
            setCode(aCode);
            setName(aName);
            setFee(aFee);
        }//parameterized Service constructor

        /** Creates a service object from a string representation of the service.
         *  @param data the string representation
         *  @throws IllegalArgumentException if the code, name or fee are invalid.
         *  @throws ParseException if the fee is an not a valid double
         */
        public Service(String data)
        {
            fromString(data);
        }//Service constructor using string data


        //**********************accessor methods

        /** Returns the service's code
         *  @return the service code
         */
        public String getCode()
        {
            return code;
        }//getCode

        /** Returns the service's name
         *  @return the service name
         */
        public String getName()
        {
            return name;
        }//getName

        /** Returns the service's fee
         *  @return the service fee
         */
        public double getFee()
        {
            return fee;
        }//getCode

        //***********************mutator methods

        /** Changes the code of the service
         *  @param aCode the new code
         *  @throws IllegalArgumentException if aCode is null, an empty string,
         *             longer than CODE_LENGTH characters or 
         *             contains characters other than digits 
         */
        public void setCode(String aCode)
        {
            if (aCode == null || aCode.Length == 0)
                throw new System.ArgumentException("A service code is required");
            else if (aCode.Length > CODE_LENGTH)
                throw new System.ArgumentException
                    ("The service code may not be more than "
                        + CODE_LENGTH + " digits");
            else
            {
                //check that each character is a digit
                for (int i = 0; i < aCode.Length; i++)
                    if (!Char.IsDigit(aCode[i]))
                        throw new System.ArgumentException
                            ("The service code may contain digits only");
            }
            code = aCode;
        }//setCode

        /** Changes the name of the service
         *  @param aName the new name
         *  @throws IllegalArgumentException if aName is null, an empty string 
         *             or more than NAME_LENGTH characters
         */
        public void setName(String aName)
        {
            if (aName == null || aName.Length == 0)
                throw new System.ArgumentException("A service name is required");
            else if (aName.Length > NAME_LENGTH)
                throw new System.ArgumentException
                    ("The service name may not be more than "
                        + NAME_LENGTH + " characters");
            name = aName;
        }//setName

        /** Changes the fee of the service
         *  @param aFee the new fee
         *  @throws IllegalArgumentException if aFee is negative or
         *             greater than MAX_FEE
         */
        public void setFee(double aFee)
        {
            if (aFee < 0 || aFee >= MAX_FEE)
                throw new System.ArgumentException
                    ("The fee must be between $0 and " + MAX_FEE.ToString("C2"));
            fee = aFee;
        }//setFee

        /** Returns a string representation of the service consisting of the values,
         *  converted to strings, of all the instance variables separated by
         *  the character SEPARATOR.
         *  @return the string representation of the service.
         */
        public String toString()
        {
            return code + SEPARATOR + name + SEPARATOR + fee.ToString();    //fee.ToString("C2");
        }//toString

        /** Changes all the instance variables to the values given by the string 
         *  representation of the service.
         *  @param data the string representation of the person
         *  @throws ParseException if the service fee is not a valid double
         *  @throws IllegalArgumentException if any of the values are
         *             invalid.
         *  @throws IllegalStateException if there are not enough values 
         *             in the string
         */
        public void fromString(String data)
        {
            String[] fields = data.Split(SEPARATOR);
            setCode(fields[0]);
            setName(fields[1]);
            setFee(Convert.ToDouble(fields[2]));
        }//fromString

        /** Returns a string representation of the service in a format that is
         *  suitable for text display.
         *  @return a formatted string representation of the service
         */
        public String toFormattedString()
        {
            String serviceString = "Code:  " + code
                               + "\nName:  " + name
                               + "\nFee:   " + fee.ToString("C2");
            return serviceString;
        }//toFormattedString

        //******************instance variables
        private String code;
        private String name;
        private double fee;

        //******************class variables
        /** Maximum number of digits for a service code
         */
        public static int CODE_LENGTH = 6;
        /** Maximum number of characters for a service name
         */
        public static int NAME_LENGTH = 20;
        /** Maximum fee for a service
         */
        public static double MAX_FEE = 1000;

        private static char SEPARATOR = '#';

    }//class Service
}
