using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class ManagerInterface
    {

        /**
         * Creates a new Reporting Subsystem
         */
        public ManagerInterface()
        {
            //set up user interface and menu
            ui = new UserInterface();
            String menuText = "1.\tProvider Report\n" +
                          "2.\tMember Report\n" +
                          "3.\tAccounts Payable Report\n" +
                          "4.\tQuit\n";

            int choice;
            do
            {
                ui.message("\t\t\tReporting Subsystem\n");

                //display menu and get choice
                choice = ui.menu(menuText);
                switch (choice)
                {
                    //use case Produce Provider Report
                    case 1: providerReport(); break;

                    //use case Produce Member Report
                    case 2: memberReport(); break;

                    //use case Produce Accounts Payable Report
                    case 3: accountsPayableReport(); break;

                    //Quit
                    case 4: break;
                    default: ui.errorMessage("Invalid choice.  Please re-enter."); break;
                }
            } while (choice != 4);

        }// default Constructor

        // Scenario: Request Provider Report
        private void providerReport()
        {
            try
            {
                //Get the provider number
                long number = ui.promptForLong("Provider Number: ");

                //Search for the provider number in the collection
                Providers providers = new Providers();
                providers.open();
                Provider theProvider = providers.find(number);
                providers.close();

                if (theProvider == null)
                {
                    ui.errorMessage("Invalid Provider Number");
                    return;
                }

                //Get the end date
                DateTime endDate = ui.promptForDate("End date of week ("
                            + UserInterface.DATE_FORMAT + "): ");

                //Generate report for specified week
                ProviderReportGenerator generator
                        = new ProviderReportGenerator(theProvider, endDate);
                ProviderReport report = generator.getReport();

                //Display the report
                report.display(ui);

                //"Print" (save the report to a file) if the user wishes
                String answer = ui.promptForString("Save the report? (Y)es or (N)o: ");
                if (answer != null && answer.Length >= 1 &&
                            Char.ToUpper(answer[0]) == 'Y')
                {
                    report.print(theProvider.getName());
                    ui.message("The report has been saved as "
                        + report.getFileName() + "\n");
                }//if
                else ui.message("Report not saved");
            }
            catch (FileNotFoundException ex)
            {
                //occurs if the file cannot be created
                ui.errorMessage(ex.Message);
            }

        }//providerReport

        // Scenario: Request Member Report
        private void memberReport()
        {
            try
            {
                //Get the member number
                long number = ui.promptForLong("Member Number: ");

                //Search for the member in the collection
                Members members = new Members();
                members.open();
                Member theMember = members.find(number);
                members.close();

                if (theMember == null)
                {
                    ui.errorMessage("Invalid Member Number");
                    return;
                }

                //Get the end date
                DateTime endDate = ui.promptForDate("End date of week ("
                                + UserInterface.DATE_FORMAT + "): ");

                //Generate report for specified week
                MemberReportGenerator generator
                        = new MemberReportGenerator(theMember, endDate);
                MemberReport report = generator.getReport();

                //Display the report
                report.display(ui);

                //"Print" (save the report to a file) if the user wishes
                String answer = ui.promptForString
                    ("Save the report? (Y)es or (N)o: ");
                if (answer != null && answer.Length >= 1 &&
                            Char.ToUpper(answer[0]) == 'Y')
                {
                    report.print(theMember.getName());
                    ui.message("The report has been saved as "
                        + report.getFileName() + "\n");
                }//if	
                else ui.message("Report not saved");
            }
            catch (FileNotFoundException ex)
            {
                //occurs if the file cannot be created
                ui.errorMessage(ex.Message);
            }
        }//memberReport


        // Scenario: Request Accounts Payable Report
        private void accountsPayableReport()
        {
            try
            {
                //Get the end date
                DateTime endDate = ui.promptForDate("End date of week (" +
                            UserInterface.DATE_FORMAT + "): ");

                //Generate report for specified week
                AccountsPayableReportGenerator generator
                        = new AccountsPayableReportGenerator(endDate);
                AccountsPayableReport report = generator.getReport();

                //Display the report
                report.display(ui);

                //"Print" (save the report to a file) if the user wishes
                String answer = ui.promptForString
                    ("Save the report? (Y)es or (N)o: ");
                if (answer != null && answer.Length >= 1 &&
                            Char.ToUpper(answer[0]) == 'Y')
                {

                    report.print("Accounts Payable");
                    ui.message("The report has been saved as "
                        + report.getFileName() + "\n");
                }
                else ui.message("Report not saved");

            }
            catch (FileNotFoundException ex)
            {
                //occurs if the file cannot be created
                ui.errorMessage(ex.Message);
            }
        }//accountsPayableReport

        //***********************instance variable
        private UserInterface ui;

        //***********************************************************************
        /**
         * Starts the reporting subsystem independently of the other subsystems
         * @param args not used
         */
        public static void main(String[] args)
        {
            try
            {
                new ManagerInterface();
            }
            catch (Exception)
            {
                UserInterface ui = new UserInterface();
                ui.message("\nEnd of test run.\n");
            }
        }//main


    }//class ManagerInterface
}
