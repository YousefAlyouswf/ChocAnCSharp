using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    /** Entity class that models persons who can be providers or members.
     *	 @author Jean Naude
     *  @version 1.0 March 2009
     */
    public abstract class Person
    {

        //*********************Constructors

        /** Creates a default person, allocating a unique number and setting all
         *  instance variables to empty strings, except the name, which is required.
         */
        public Person()
        {
            //allocate the next number as this person's unique number
            number = nextNumber;

            //increment next number ready for the next person created
            nextNumber++;
            //set attributes (except name) to empty strings      
            street = "";
            city = "";
            state = "";
            zip = "";
            email = "";
        }//Person default constructor

        /** Creates a person object from a string representation of the person.
         *  @param data the string representation
         *  @throws NumberFormatException if the person number is not a valid integer
         *  @throws IllegalArgumentException if any of the values are
         *             invalid.
         *  @throws ArrayIndexOutOfBoundsException if there are not enough values 
         *             in the string
         */
        public Person(string data)
        {
            fromString(data);
        }//Person constructor using string data


        //********************accessor methods

        /** Returns the person's unique number
         *  @return the person's unique number
         */
        public long getNumber()
        {
            return number;
        }//getNumber

        /**
         * Returns the person's name
         * @return the person's name
         */
        public string getName()
        {
            return name;
        }//getName	

        /**
         * Returns the person's street address
         * @return the person's street address
         */
        public string getStreet()
        {
            return street;
        }//getStreet

        /**
         * Returns the person's city
         * @return the person's city
         */
        public string getCity()
        {
            return city;
        }//getCity

        /**
         * Returns the person's state
         * @return the person's state
         */
        public string getState()
        {
            return state;
        }//getState

        /**
         * Returns the person's zip code
         * @return the person's zip code
         */
        public string getZip()
        {
            return zip;
        }//getZip

        /**
         * Returns the person's email address
         * @return the person's email address
         */
        public string getEmail()
        {
            return email;
        }//getEmail

        //public string getStatus()
        //{
        //    return status;
        //}
        //**********************mutator methods

        /** Changes the person's name
         *  @param aName the new name
         *  @throws IllegalArgumentException if aName is null, an empty string
         *             or longer than NAME_LENGTH characters
         */
        public void setName(string aName)
        {
            if (aName == null || aName.Length == 0)
                throw new System.ArgumentException("A name is required");
            else if (aName.Length > NAME_LENGTH)
                throw new System.ArgumentException("Name may not be more than "
                    + NAME_LENGTH + " characters");
            name = aName;
        }//setName		

        /** Changes the person's street address
         *  @param aStreet the new street address
         *  @throws IllegalArgumentException if aStreet is not null
         *             and is longer than STREET_LENGTH characters
         */
        public void setStreet(string aStreet)
        {
            if (aStreet == null) aStreet = "";
            else if (aStreet.Length > STREET_LENGTH) //street is not required
                throw new System.ArgumentException("Street may not be more than "
                    + STREET_LENGTH + " characters");
            street = aStreet;
        }//setStreet

        /** Changes the person's city
         *  @param aCity the new city
         *  @throws IllegalArgumentException if aCity is not null
         *             and is longer than CITY_LENGTH characters
         */
        public void setCity(string aCity)
        {
            if (aCity == null) aCity = "";
            else if (aCity.Length > CITY_LENGTH) //city is not required
                throw new System.ArgumentException("City may not be more than "
                        + CITY_LENGTH + " characters");
            city = aCity;
        }//setCity

        /** Changes the person's state
         *  @param aState the new state
         *  @throws IllegalArgumentException if aState is not 
         *             exactly STATE_LENGTH letters, null or an empty string
         */
        public void setState(string aState)
        {
            if (aState == null) aState = "";
            else if (aState.Length > 0) //state is not required
            {
                if (aState.Length != STATE_LENGTH)
                    throw new System.ArgumentException("State must be exactly "
                             + STATE_LENGTH + " letters");
                //test whether each character is a letter
                for (int i = 0; i < STATE_LENGTH; i++)
                    if (!Char.IsLetter(aState, i))
                        throw new System.ArgumentException("State must be "
                            + STATE_LENGTH + " letters only");
            }

            state = aState;
        }//setState

        /** Changes the person's zip code
         *  @param aZip the new zip code
         *  @throws IllegalArgumentException if aZip 
         *             is not null or an empty string and
         *             is not composed entirely of digits or
         *             is longer than ZIP_LENGTH digits 
         */
        public void setZip(string aZip)
        {
            if (aZip == null) aZip = "";         //Replace null with an empty string
            else if (aZip.Length > 0)          //if given, zip must be 5 digits
            {
                //test for correct length
                if (aZip.Length != ZIP_LENGTH)
                    throw new System.ArgumentException("Zip code must be exactly "
                            + ZIP_LENGTH + " digits");

                //test whether each character is a digit
                for (int i = 0; i < ZIP_LENGTH; i++)
                    if (!char.IsDigit(aZip, i))
                        throw new System.ArgumentException("Zip code must be "
                                + ZIP_LENGTH + " digits only");
            }

            zip = aZip;
        }//setZip

        /** Changes the person's email address.  No validation is done.
         *  @param anEmail the new email address
         */
        public void setEmail(string anEmail)
        {
            if (anEmail == null) anEmail = "";
            email = anEmail;
        }//setEmail

        //public void setStatus(string aStatus)
        //{
        //    if (aStatus == null) aStatus = "";
        //    status = aStatus;
        //}
        //***********************utility methods

        /** Returns a string representation of the person consisting of the values,
         *  converted to strings, of all the instance variables separated by
         *  the character SEPARATOR.
         *  @return the string representation of the person.
         */
        public virtual string toString()
        {
            return "" + number + SEPARATOR + name + SEPARATOR
             + street + SEPARATOR + city + SEPARATOR + state + SEPARATOR
             + zip + SEPARATOR + email; //+ SEPARATOR + status;
        }//toString

        /** Changes all the instance variables to the values given by the string 
         *  representation of the person.
         *  @param data the string representation of the person
         *  @throws NumberFormatException if the person number 
         *				 is not a valid integer
         *  @throws IllegalArgumentException if any of the values are
         *             invalid.
         *  @throws IndexOutOfBoundsException if there are 
         *             not enough values in the string
         */
        public void fromString(string data)
        {
            string[] fields = data.Split(SEPARATOR);
            number = int.Parse(fields[0]);
            setName(fields[1]);
            setStreet(fields[2]);
            setCity(fields[3]);
            setState(fields[4]);
            setZip(fields[5]);
            setEmail(fields[6]);
            //setStatus(fields[7]);
        }//fromString

        /** Returns a string representation of the person in a format that is
         *  suitable for text display.
         *  @return a formatted string representation of the person
         */
        public string toFormattedString()
        {
            string personString = "Number:         " + number
                                    + "\nName:           " + name
                                    + "\nStreet Address: " + street
                                    + "\nCity:           " + city
                                    + "\nState:          " + state
                                    + "\nZip Code:       " + zip
                                    + "\nEmail:          " + email;
                                    //+ "\nStatus:         " + status;
            return personString;
        }//toFormattedString

        //*********************class methods

        /** Returns the number that will be allocated to the next person created.
         *  @return the next person number
         */
        public static long getNextNumber()
        {
            return nextNumber;
        }//getNextNumber

        /** Changes the number that will be allocated to the next person created.
         *  @param aNextNumber the new next number
         *  @throws IllegalArgumentException if aNextNumber is less than the
         *             current next number.
         */
        public static void setNextNumber(long aNextNumber)
        {
            if (aNextNumber < nextNumber)
                throw new System.ArgumentException("The Person next number cannot be decreased");
            nextNumber = aNextNumber;
        }//setNextNumber


        //********************instance variables
        private long number;   // cater for 9 digits
        private string name;
        private string street;
        private string city;
        private string state;
        private string zip;
        private string email;
        //private string status;

        //*******************class variables
        private static long nextNumber;

        /** The character used to separate fields in the string representation
         *  of the person
         */
        protected static char SEPARATOR = '#';

        /** maximum number of characters for name
         */
        public static int NAME_LENGTH = 25;

        /** maximum number of characters for street
         */
        public static int STREET_LENGTH = 25;

        /** maximum number of characters for city
         */
        public static int CITY_LENGTH = 14;

        /** number of letters for state
         */
        public static int STATE_LENGTH = 2;

        /** number of digits for zip code
         */
        public static int ZIP_LENGTH = 5;

    }//Person
}
