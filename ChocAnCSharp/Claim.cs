using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    //import java.util.Date;
    //import java.text.SimpleDateFormat;
    //import java.text.ParseException;

    /** Entity class that models a claim submitted by a ChocAn provider for a service
     *  rendered to a ChocAn member.
     *  @author Jean Naude
     *  @version 1.0 March 2009
     */
    public class Claim
    {
        //********************constructors

        /**
         * Creates a new claim, and sets the submission date and time
         * to the current system date and time
         */
        public Claim()
        {
			submissionDate = DateTime.Now;
        }//constructor

        /**
         * Creates a new claim with the given values and sets the submission date 
         * and time	to the current system date and time
         *	@param aServiceCode the service code of the service provided
         * @param aProviderNumber the provider number of the provider of the service
         * @param aMemberNumber the member number of the member 
         *                      who received the service
         * @param aServiceDate the date the service was provided
         * @throws IllegalArgumentException if any of the values given are invalid
         */
        public Claim(String aServiceCode, long aProviderNumber, long aMemberNumber
                    , DateTime aServiceDate) : this()
        {
            setServiceCode(aServiceCode);
            setProviderNumber(aProviderNumber);
            setMemberNumber(aMemberNumber);
            setServiceDate(aServiceDate);
        }//Claim parameterized constructor

        /** Creates a claim from the string representation of a claim.
         *  @param data the string representation of the claim
        *  @throws NumberFormatException if the provider or member number is 
        *             not a valid integer
        *  @throws IllegalArgumentException if any of the values are
        *             invalid.
        *  @throws IndexOutOfBoundsException if there are 
        *             not enough values in the string
        *  @throws ParseException if the submission date and time or the 
        *             service date is not in the correct format
         */
        public Claim(String data)
        {
            fromString(data);
        }//Claim constructor using string data


        //*************************accessor methods

        /** Returns the date and time that the claim was submitted
         *  @return the submission date and time
         */
        public DateTime getSubmissionDate()
        {
			return submissionDate;
        }//getSubmissionDate

        /** Returns the code of the service that was rendered
         *  @return the service code of the claim
         */
        public String getServiceCode()
        {
            return serviceCode;
        }//getServiceCode

        /** Returns the provider number of the provider that rendered the service
         *	 @return the provider number
         */
        public long getProviderNumber()
        {
            return providerNumber;
        }//getProviderNumber

        /** Returns the member number of the member who received the service
         *  @return the member number
         */
        public long getMemberNumber()
        {
            return memberNumber;
        }//getMemberNumber


        /** Returns the date the service was rendered
         *  @return the service date
         */
        public DateTime getServiceDate()
        {
            return serviceDate;
        }//getServiceDate	


        //**********************mutator methods

        /** Changes the service code
         *	 @param aCode the new service code
         *  @throws IllegalArgumentException if the code given is null or empty,
         *          if the code does not consist of digits only,
         *          or if the code is longer than CODE_LENGTH digits
         */
        public void setServiceCode(String aCode)
        {
            if (aCode == null || aCode.Length == 0)
                throw new System.ArgumentException("A service code is required");
            else if (aCode.Length > CODE_LENGTH)
                throw new System.ArgumentException("Service code may not be more than "
                    + CODE_LENGTH + " digits");
            else
                for (int i = 0; i < aCode.Length; i++)
                    if (!Char.IsDigit(aCode[i]))
                        throw new System.ArgumentException("Service code must consist "
                                + "of digits only");
            serviceCode = aCode;
        }//setServiceCode

        /** Changes the provider number
         *  @param aNumber the new provider number
         *  @throws IllegalArgumentException if the provider number is negative
         */
        public void setProviderNumber(long aNumber)
        {
            if (aNumber < 0)
                throw new System.ArgumentException("Invalid provider number");
            providerNumber = aNumber;
        }//setProviderNumber

        /** Changes the member number
         *  @param aNumber the new member number
         *  @throws IllegalArgumentException if the member number is negative
         */
        public void setMemberNumber(long aNumber)
        {
            if (aNumber < 0)
                throw new System.ArgumentException("Invalid member number");
            memberNumber = aNumber;
        }//setMemberNumber

        /** Changes the date the service was rendered
         *  @param aDate the service date
         *  @throws IllegalArgumentException if the date is null
         */
        public void setServiceDate(DateTime aDate)
        {
            if (aDate == null)
                throw new System.ArgumentException("The service date is required");
            else serviceDate = aDate;
        }//setServiceDate	

        /** Returns a string representation of the claim consisting of the values,
          *  converted to strings, of all the instance variables separated by
          *  the character SEPARATOR.
          *  @return the string representation of the claim.
          */
        public String toString()
        {
            String submissionDateString = submissionDate.ToString(DATE_TIME_FORMAT);
            String serviceDateString = serviceDate.ToString(DATE_FORMAT);
            return submissionDateString + "\t"
                + serviceCode + "\t"
				+ providerNumber + "\t"
				+ memberNumber + "\t"
				+ serviceDateString;
        }//toString

        /** Changes all the instance variables to the values given by the string 
         *  representation of the claim.
         *  @param data the string representation of the cliam
         *  @throws NumberFormatException if the provider or member number is 
         *             not a valid integer
         *  @throws IllegalArgumentException if any of the values are
         *             invalid.
         *  @throws IndexOutOfBoundsException if there are 
         *             not enough values in the string
         *  @throws ParseException if the submission date and time or the 
         *             service date is not in the correct format
         */
        private void fromString(String data)
        {
            String[] fields = data.Split(SEPARATOR);
            DateTime.TryParseExact(fields[0], DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None,
                                                    out submissionDate); //convert to a date
            setServiceCode(fields[1]);
            setProviderNumber(int.Parse(fields[2]));
            setMemberNumber(int.Parse(fields[3]));
            DateTime.TryParseExact(fields[4], DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None,
                                                    out serviceDate); //convert to a date
            setServiceDate(serviceDate);
        }//fromString

        //*********************instance variables
        private DateTime submissionDate;
        private String serviceCode;
        private long providerNumber;
        private long memberNumber;
        private DateTime serviceDate;

        //**********************class variables
        /** Maximum number of digits in service code
         */
        public static int CODE_LENGTH = 6;

        private static String DATE_FORMAT = "MM-dd-yyyy";
        private static String DATE_TIME_FORMAT = "MM-dd-yyyy HH-mm-ss";
        private static char SEPARATOR = '#';

    }//class Claim
}

