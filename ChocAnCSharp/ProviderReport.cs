using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class ProviderReport : DateRangeReport
    {

        /**
         * Creates a new ProviderReport.
         * @param aProvider the provider whose details and claims are to be included
         * @param anEndDate the date to be included in the date range
         */
        public ProviderReport(Provider aProvider, DateTime anEndDate) : base(anEndDate)
        {
            //Add a title
            addHeading("Claim Submissions");

            //Add date and provider details
            String dateString = getEndDate().ToString(DATE_FORMAT);
            sb.AppendLine(String.Format("Week Ending: {0} {1,20}  Provider Number: {2}",
                dateString, "", aProvider.getNumber()));
            sb.AppendLine(String.Format("Provider Name: {0,-30} Address: {1}",
                aProvider.getName(), aProvider.getStreet()));
            sb.AppendLine(String.Format("{0,54} {1}", "", aProvider.getCity()));
            sb.AppendLine(String.Format("{0,54} {1}", "", aProvider.getState()));
            sb.AppendLine(String.Format("{0,54} {1}", "", aProvider.getZip()));

            //Add detail headings	
            sb.AppendLine(String.Format("Submission Date-Time  Service Date    Code       Fee"
                                    + "  Member No.  Member Name"));
            sb.AppendLine(String.Format("--------------------  ------------    ----       ---"
                                    + "  ----------  -----------"));
            detailCount = 0;
        }// constructor

        /** Adds a line of detail about a claim submitted
	     *  @param submitDate the date of submission
	     *  @param serviceDate the date of the service provided
	     *  @param memberNumber the number of the member to whom the service was
	     *                      rendered
	     *  @param memberName the name of the member to whom the service was rendered
	     *  @param serviceCode the code of the service that was provided
	     *  @param serviceFee the fee for the service provided
	     */
        public void addDetail(DateTime submitDate, DateTime serviceDate, long memberNumber,
                        String memberName, String serviceCode, double serviceFee)
        {
            String submitDateString = submitDate.ToString(DATE_TIME_FORMAT);
            String serviceDateString = serviceDate.ToString(DATE_FORMAT);
            String serviceFeeString = serviceFee.ToString("C2");
            sb.AppendLine(String.Format("{0,20}  {1,12}  {2,6}  {3,8}  {4,10}  {5}",
                submitDateString, serviceDateString, serviceCode, serviceFeeString,
                memberNumber, memberName));
            detailCount++;
        }//addDetail

        /** Adds a summary to the report
	     *  @param noConsultations the number of consultations the provider had
	     *  @param totalFee the total fee payable to the provider
	     */
        public void addSummary(int noConsultations, double totalFee)
        {
            sb.AppendLine(String.Format("Number of consultations: {0}", noConsultations));
            String totalFeeString = totalFee.ToString("C2");
            sb.AppendLine(String.Format("Total Fee: {0}", totalFeeString));
        }//addSummary	

        /** Returns the number of lines of detail added to the report
	     *  @return the number of lines of detail
	     */
        public int getDetailCount()
        {
            return detailCount;
        }//getDetailCount

        //************************instance variable
        private int detailCount;

    }//class ProviderReport
}
