using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class SchedulerInterface
    {

        /**
         * Creates a new SchedulerInterface which then runs the accounting procedure.
         */
        public SchedulerInterface()
        {
            //for communciation with the tester
            UserInterface ui = new UserInterface();
            ui.message("\nRunning the accounting procedure ...\n");

            //Use today's date for all reports
            DateTime now = DateTime.Today;

            try
            {
                //Generate provider reports
                ui.message("Generating the providers' reports ...");
                Providers providers = new Providers();
                providers.open();
                List<Person> allProviders = providers.getAll();
                foreach (Person person in allProviders)
                {
                    Provider provider = (Provider)person;
                    ProviderReportGenerator gen = new ProviderReportGenerator(provider, now);
                    ProviderReport theReport = gen.getReport();
                    if (theReport.getDetailCount() > 0)
                        theReport.sendByEmail(provider.getName());
                }
                providers.close();

                //Generate member reports
                ui.message("Generating the members' reports ...");
                Members members = new Members();
                members.open();
                List<Person> allMembers = members.getAll();
                foreach (Person person in allMembers)
                {
                    Member member = (Member)person;
                    MemberReportGenerator gen = new MemberReportGenerator(member, now);
                    MemberReport theReport = gen.getReport();
                    if (theReport.getDetailCount() > 0)
                        theReport.sendByEmail(member.getName());
                }
                members.close();

                //Generate accounts payable report
                ui.message("Generating the accounts payable report ...");
                AccountsPayableReportGenerator generator
                    = new AccountsPayableReportGenerator(now);
                generator.getReport().sendByEmail("Accounts Payable");

                //Generate EFT data
                ui.message("Generating the EFT data ...");
                EFTReportGenerator eftGenerator = new EFTReportGenerator(now);
                eftGenerator.getReport().print("EFT Data");

                ui.message("\nAccounting procedure completed successfully.\n\n");
            }
            catch (FileNotFoundException ex)
            {
                //occurs if a file cannot be created
                ui.errorMessage(ex.Message);
            }

        }//default constructor

        //************************************************************
        /**
         * Runs the accounting procedure independently of subsystems
         * @param args not used
         */
        public static void main(String[] args)
        {
            try
            {
                new SchedulerInterface();
            }
            catch (Exception)
            {
                UserInterface ui = new UserInterface();
                ui.message("\nEnd of test run.\n");
            }
        }//main	
    }//SchedulerInterface
}
