using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class EFTReport : DateRangeReport
    {

        /**
         * Creates a new EFT Report.
         * @param anEndDate the date to be included in the date range
         */
        public EFTReport(DateTime anEndDate) : base(anEndDate)
        {
        }// constructor

        /** Adds a line of detail about a provider to be paid to the report
         *  @param providerNumber the provider's number
         *  @param totalFee the total fee payable to the provider
         *  @param providerName the provider's name
         */
        public void addDetail(long providerNumber, double totalFee, String providerName)
        {
            String totalFeeString = totalFee.ToString("C2");
            sb.AppendLine(String.Format("{0,9}  {1,12}  {2}", providerNumber, totalFeeString, providerName));
        }//addDetail

    }//class EFTReport
}
