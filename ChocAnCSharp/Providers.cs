using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class Providers : Persons
    {

        /**
         * Creates a new empty Providers object
         */
        public Providers()
        {
        }//default constructor

        /** Reads all the providers in the FILE_NAME text file into the collection.
         *  @throws NumberFormatException if the provider number is not a valid 
         *          integer
         *  @throws IllegalArgumentException if any of the values are invalid.
         *  @throws ArrayIndexOutOfBoundsException if there are not enough values 
         *             in the string
         */
        new public void open()
        {
            //Initialize the next number if this has not already been done
            if (!(Person.getNextNumber() > 0))
                base.open();

            try
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(FILE_NAME))
                {
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        //create a provider with this data
                        Provider aProvider = new Provider(line);

                        //add provider to collection
                        add(aProvider);
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }
        }//open

        /** Writes all the providers in the collection to the FILE_NAME text file.
         *  @throws FileNotFoundException if the file cannot be created
         */
        new public void close()
        {
            base.close();
            saveToFile(FILE_NAME);
        }//close

        /** Searches for a provider with the given provider number in the collection.
         *  @param providerNumber the number for which to search
         *  @return the provider if found, otherwise null
         */
        public Provider find(long providerNumber)
        {
            return (Provider)search(providerNumber);
        }//find


        //******************class variable
        private static String FILE_NAME = "Providers.txt";  //default store

    }//class Providers
}
