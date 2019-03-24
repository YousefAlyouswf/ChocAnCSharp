using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public abstract class DateRangeReport : Report
    {

        /** Creates a new report with a date range.  The date range is set to 
         *  the week that includes the end date given.  A Friday is expected for the
         *  end date.  If a Saturday is given as the end date, the end date is 
         *  adjusted to the Friday preceding the Saturday.
         *  If a Sunday through to Thursday is given as the end date, the end date
         *  is adjusted to the following Friday.
         *  @param anEndDate the date to be included in the date range
         */
        public DateRangeReport(DateTime anEndDate) : base()
        {
            //Ensure that the end date is a Friday.
            setDates(anEndDate);

            //initialize the date and date time formatters
            //dateTimeFormatter = new SimpleDateFormat();
            //dateTimeFormatter.applyPattern(DATE_TIME_FORMAT);
            //dateFormatter = new SimpleDateFormat();
            //dateFormatter.applyPattern(DATE_FORMAT);

        }//DateRangeReport constructor

        /** Returns the end date (Friday) of the week of this report
         */
        public DateTime getEndDate()
        {
            return endDate;
        }//getEndDate

        /** Returns the upper bound (exclusive) of the date range for this report
         */
        public DateTime getEndDateRange()
        {
            return endDateRange;
        }//getEndDateRange

        /** Returns the lower bound (exclusive) of the date range for this report
         */
        public DateTime getStartDateRange()
        {
            return startDateRange;
        }//getStartDateRange

        // Sets the dates for the report.  The date range is midnight Friday 
        // of the previous week to 1 second before midnight of the week
        // within which the given date falls, inclusive.
        private void setDates(DateTime givenDate)
        {
            Calendar calendar = new GregorianCalendar();

            //adjust time to 00:00:00 -- dates entered by manager already have 
            //time as 00:00:00, but when testing the accounting procedure
            //the time is the current system time
            givenDate = calendar.AddHours(givenDate, 0);
            givenDate = calendar.AddMinutes(givenDate, 0);
            givenDate = calendar.AddSeconds(givenDate, 0);
            givenDate = calendar.AddMilliseconds(givenDate, 0);

            // Set end date to the Friday of the week in which it falls
            //i.e a Saturday is set to the preceding Friday
            //Sunday to Thursday is set to the following Friday
            givenDate = givenDate.AddDays(-(int)givenDate.DayOfWeek).AddDays(5);
            endDate = givenDate;

            //Use Saturday 00:00:00 am as end of range (exclusive)
            givenDate = givenDate.AddDays(-(int)givenDate.DayOfWeek).AddDays(6);
            endDateRange = givenDate;

            //Use the previous Friday 12:59:59 pm as 
            //the beginning of range (exclusive)
            givenDate = givenDate.AddDays(-(int)givenDate.DayOfWeek).AddDays(-5);
            givenDate = calendar.AddSeconds(givenDate, -1);
            startDateRange = givenDate;
        }//setDates

        /** Simulates sending a report by email.  The report is saved to a
         *  text file named by the "email address" given plus the report date
         *  plus the extension .txt.
         *  @param emailAddress the email address to which the report must be sent
         *  @throws FileNotFoundException if the file cannot be created
         */
        new public void sendByEmail(String emailAddress)
        {
            String endDateString = endDate.ToString(DATE_FORMAT);
            String fileName = emailAddress + " " + endDateString;
            base.sendByEmail(fileName);
        }//sendByEmail

        //**********************class variables 	
        /** Format used for dates
         */
        public static String DATE_FORMAT = "MM-dd-yyyy";
        /** Format used for date and time
         */
        public static String DATE_TIME_FORMAT = "MM-dd-yyyy HH-mm-ss";
        /** Used to format dates
         */
        //protected static SimpleDateFormat dateFormatter;
        /** Used to format dates and times
         */
        //protected static SimpleDateFormat dateTimeFormatter;

        //**********************instance variables
        private DateTime endDate;
        private DateTime endDateRange;
        private DateTime startDateRange;

    }//class DateRangeReport
}
