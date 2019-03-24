using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class AccountsPayableReport : DateRangeReport
    {

        /**
         * Creates a new AccountsPayableReport.
         * @param anEndDate the date to be included in the date range
         */
        public AccountsPayableReport(DateTime anEndDate) : base(anEndDate)
        {
            //Add a title
            addHeading("Accounts Payable");

            //Add date
            String dateString = getEndDate().ToString(DATE_FORMAT);
            sb.AppendLine(String.Format("Week Ending: {0}", dateString));

            //Add detail headings	
            sb.AppendLine(String.Format("Provider Number   Consultations           Fee  " + "Provider Name"));
            sb.AppendLine(String.Format("---------------   -------------    ----------  " + "-------------"));
        }// constructor

        /** Adds a line of detail about a provider to the report
         *  @param providerNumber the provider's number
         *  @param noConsultations the number of consultations the provider had
         *                         during the week
         *  @param totalFee the total fee payable to the provider
         *  @param providerName the provider's name
         */
        public void addDetail(long providerNumber, int noConsultations,
                        double totalFee, String providerName)
        {
            String totalFeeString = totalFee.ToString("C2");
            sb.AppendLine(String.Format("{0,15}   {1,13}  {2,12}  {3}",
                providerNumber, noConsultations, totalFeeString, providerName));
        }//addDetail

        /** Adds a summary to the report
         *  @param totalNoConsultations the total number of consultations for all 
         *                              providers
         *  @param grandTotalFee the total fee payable to all providers
         *  @param providerCount the number of providers to be paid
         */
        public void addSummary(int totalNoConsultations, double grandTotalFee,
                                      int providerCount)
        {
            String grandTotalFeeString = grandTotalFee.ToString("C2");
            sb.AppendLine(String.Format("---------------   -------------    ----------  "
                + "-------------"));
            sb.AppendLine(String.Format("Totals:           {0,13}  {1,12}     {2}",
                        totalNoConsultations, grandTotalFeeString, providerCount));
        }//addSummary

    }//class AccountsPayableReport
}
