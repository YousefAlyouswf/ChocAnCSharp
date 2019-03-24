using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class EFTReportGenerator
    {
        /** Creates a new EFT report generator which creates a new 
         *  EFT report.
         *  @param endDate a date within the week for which the report is to be
         *         generated
         *  @throws FileNotFoundException if the file cannot be created.
         */
        public EFTReportGenerator(DateTime endDate)
        {
            Claims claims = null;
            Services services = null;
            Providers providers = null;

            //create a new EFT report
            report = new EFTReport(endDate);

            try
            {
                //create and open claims, services and providers collections					
                claims = new Claims();
                claims.open();
                services = new Services();
                services.open();
                providers = new Providers();
                providers.open();

                //get all providers
                List<Person> allProviders = providers.getAll();

                //for each provider
                foreach (Person person in allProviders)
                {
                    double totalFee = 0; //accumulates fees payable to provider

                    Provider provider = (Provider)person;

                    //get all claims submitted by provider
                    List<Claim> providerClaims =
                        claims.findByProvider(provider.getNumber());

                    //for each claim
                    foreach (Claim nextClaim in providerClaims)
                    {
                        //test whether claim is witin date range
                        if (nextClaim.getSubmissionDate() > (report.getStartDateRange())
                            && nextClaim.getSubmissionDate() < (report.getEndDateRange()))
                        {
                            //get service fee
                            double serviceFee;
                            Service service = services.find(nextClaim.getServiceCode());
                            if (service == null)
                                throw new System.Exception("Invalid Code");
                            else serviceFee = service.getFee();

                            //accumulate fees payable
                            totalFee += serviceFee;

                        }//if date in specified week
                    }//for each claim				

                    if (totalFee > 0)
                    {
                        //add fees payable detail to report
                        report.addDetail(provider.getNumber(), totalFee,
                                          provider.getName());
                    }
                }//for each provider		
            }//try
            catch (Exception ex)
            {
                report.addErrorMessage(ex.Message);
            }
            finally
            {
                if (claims != null) claims.close();
                if (providers != null) providers.close();
                if (services != null) services.close();
            }

        }//constructor

        /** Returns the report
         *  @return the report
         */
        public EFTReport getReport()
        {
            return report;
        }//getReport

        //********************instance variable
        private EFTReport report;

    }//class EFTReportGenerator
}
