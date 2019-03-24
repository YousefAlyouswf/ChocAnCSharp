using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class AccountsPayableReportGenerator
    {

        /** Creates a new accounts payable report generator which creates a new 
         *  accounts payable report
         *  @param endDate a date within the week for which the report is to be
         *         generated
         *  @throws FileNotFoundException if the file cannot be created.
         */
        public AccountsPayableReportGenerator(DateTime endDate)
        {
            Claims claims = null;
            Services services = null;
            Providers providers = null;

            //create a new accounts payable report
            report = new AccountsPayableReport(endDate);

            try
            {
                //create and open claims, services and providers collections
                claims = new Claims();
                claims.open();
                services = new Services();
                services.open();
                providers = new Providers();
                providers.open();

                int totalNoConsultations = 0; //accumulates number of consultations
                int providerCount = 0;     //counts number of providers to be paid
                double grandTotalFee = 0;  //accumulates all fees payable

                //get all providers
                List<Person> allProviders = providers.getAll();

                //for each provider
                foreach (Person person in allProviders)
                {
                    int noConsultations = 0;  //counts this provider's claims
                    double totalFee = 0; //accumulates fees payable to this provider 

                    Provider provider = (Provider)person;

                    //get all claims submitted by this provider
                    List<Claim> providerClaims =
                        claims.findByProvider(provider.getNumber());

                    //for each claim
                    foreach (Claim nextClaim in providerClaims)
                    {
                        //test whether within date range
                        if (nextClaim.getSubmissionDate() > (report.getStartDateRange())
                            && nextClaim.getSubmissionDate() < (report.getEndDateRange()))
                        {
                            //get service fee
                            double serviceFee;
                            Service service
                                = services.find(nextClaim.getServiceCode());
                            if (service == null)
                                serviceFee = 0;   //indicates invalid code
                            else serviceFee = service.getFee();

                            //increment number of consultations
                            noConsultations++;

                            //accumulate fees payable
                            totalFee += serviceFee;
                        }//if date in specified week
                    }//for each claim

                    if (noConsultations > 0)
                    {
                        //add provider detauls to report
                        report.addDetail(provider.getNumber(), noConsultations
                                , totalFee, provider.getName());

                        //accumulate number of consultations for all providers
                        totalNoConsultations += noConsultations;

                        //accumulate fees payable for all providers
                        grandTotalFee += totalFee;

                        //increment provider count
                        providerCount++;
                    }
                }//for each provider

                //add summary to report		
                report.addSummary(totalNoConsultations, grandTotalFee
                                    , providerCount);
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
        public AccountsPayableReport getReport()
        {
            return report;
        }//getReport

        //********************instance variable
        private AccountsPayableReport report;

    }//class AccountsPayableReportGenerator
}
