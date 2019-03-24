using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class ServiceReportGenerator
    {
        /** Creates a new service report generator object which creates a new
         *  provider directory (service report).
         */
        public ServiceReportGenerator()
        {
            try
            {
                Services services = new Services();
                services.open();

                //Create a new provider directory
                report = new ProviderDirectory();

                //Get all services
                List<Service> allServices = services.getAllOrderedByName();

                //Add all services to provider directory
                foreach (Service aService in allServices)
                    report.addDetail(aService.getName(), aService.getCode(),
                            aService.getFee());

                services.close();

            }
            catch (Exception ex)
            {
                //occurs if the file cannot be created
                report.addErrorMessage(ex.Message);
            }
            //catch (ParseException ex)
            //{
            //    //occurs if the file format is incorrect
            //    report.addErrorMessage(ex.getMessage());
            //}
        }//default constructor

        /** Returns the report
         *  @return the report
         */
        public ProviderDirectory getReport()
        {
            return report;
        }//getReport

        //********************instance variable
        private ProviderDirectory report;

    }//class ServiceReportGenerator
}
