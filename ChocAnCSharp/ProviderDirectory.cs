using System;

namespace ChocAnCSharp
{
    public class ProviderDirectory : Report
    {

        /**
         * Creates a new ProviderDirectory
         */
        public ProviderDirectory()
        {
            String title = "Provider Directory";

            sb.AppendLine(String.Format("{0,31} ", title));
            sb.AppendLine(String.Format("{0,-25} {1,-6} {2,8} ", "Service Name", "Code", "Fee"));
            sb.AppendLine(String.Format("{0,-25} {1,-6} {2,8} ", "------------", "----", "---"));

        }//default constructor

        /** Adds a line of detail about a service
         *  @param name the name of the service
         *  @param code the code of the service
         *  @param fee the fee payable for the service
         */
        public void addDetail(String name, String code, double fee)
        {
            sb.AppendLine(String.Format("{0,-25} {1,-6} {2,8:C2} ", name, code,
                                fee));
        }//addDetail


    }//class ProviderDirectory
}