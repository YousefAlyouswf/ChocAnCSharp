using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class MemberReport : DateRangeReport
    {

        /**
         * Creates a new MemberReport.
         * @param aMember the member whose details and services received 
         *                are to be included
         * @param anEndDate the date to be included in the date range
         */
        public MemberReport(Member aMember, DateTime anEndDate) : base(anEndDate)
        {
            //Add a title
            addHeading("Services Received");

            //Add date and member details
            String dateString = getEndDate().ToString(DATE_FORMAT);
            sb.AppendLine(String.Format("Week Ending: {0} {1,20}  Member Number: {2}", dateString, "", aMember.getNumber()));
            sb.AppendLine(String.Format("Member Name: {0,-32} Address: {1}", aMember.getName(), aMember.getStreet()));
            sb.AppendLine(String.Format("{0,54} {1}", "", aMember.getCity()));
            sb.AppendLine(String.Format("{0,54} {1}", "", aMember.getState()));
            sb.AppendLine(String.Format("{0,54} {1}", "", aMember.getZip()));

            //Add detail headings	
            sb.AppendLine(String.Format("Service Date    Service               Provider"));
            sb.AppendLine(String.Format("------------    -------               --------"));
            detailCount = 0;
        }// constructor

        /** Adds a line of detail about a service received by the member.
         *  @param serviceDate the date of the service
         *  @param serviceName the name of the service
         *  @param providerName the name of the provider who rendered the service
         */
        public void addDetail(DateTime serviceDate, String serviceName,
                                     String providerName)
        {
            String serviceDateString = serviceDate.ToString(DATE_FORMAT);
            sb.AppendLine(String.Format("{0,12}    {1,-12}  {2}",
                serviceDateString, serviceName, providerName));
            detailCount++;
        }//addDetail

        /** Returns the number of lines of detail added to the report
         *  @return the number of lines of detail
         */
        public int getDetailCount()
        {
            return detailCount;
        }//getDetailCount

        //************************instance variable
        private int detailCount;

    }//class MemberReport
}
