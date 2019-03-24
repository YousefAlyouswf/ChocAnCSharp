using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    class ChocAnSystem
    {

        /**
	     * Creates a new ChocAnSystem.
	     */
        public ChocAnSystem()
        {
            UserInterface ui = new UserInterface();

            string menuText = "1.\tProvider Subsystem\n" +
                          "2.\tMaintenance Subsystem\n" +
                          "3.\tReporting Subsystem\n" +
                          "4.\tAccounting Procedure\n" +
                          "5.\tQuit\n";

            int choice;
            try
            {
                do
                {
                    ui.message("\t\t\tChocoholics Anonymous\n\n");

                    //display the menu and read the choice
                    choice = ui.menu(menuText);
                    switch (choice)
                    {
                        //Start subsystem chosen
                        case 1: new ProviderInterface(); break;
                        case 2: new OperatorInterface(); break;
                        case 3: new ManagerInterface(); break;
                        case 4: new SchedulerInterface(); break;
                        case 5: break;
                        default: ui.errorMessage("Invalid choice.  Please re-enter."); break;
                    }
                } while (choice != 5);
            }
            catch (Exception)
            {
                ui.message("\nEnd of test run.\n");
            }
        }//default constructor

        /** Starts the testing interface
	     */
        static void Main(string[] args)
        {
            new ChocAnSystem();
        }//main	

    }//class ChocAnSystem
}
