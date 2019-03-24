using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class Services
    {
        //***************constructor

        /**
         * Creates a new empty Services object
         */
        public Services()
        {
            serviceList = new List<Service>();
        }//default constructor

        /** Reads all the services in the FILE_NAME text file into the collection.
         *  @throws ParseException if the fee is an not a valid double
         *  @throws IllegalArgumentException if the code, name or fee are invalid.
         */
        public void open()
        {
            try
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(FILE_NAME))
                {
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        //create service with this data
                        Service aService = new Service(line);

                        //add service to the collection
                        add(aService);
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }
        }//open

        /** Writes all the services in the collection to the FILE_NAME text file.
         *  @throws FileNotFoundException if the file cannot be created
         */
        public void close()
        {
            StreamWriter sw = new StreamWriter(FILE_NAME);
            foreach (Service aService in serviceList)
                sw.WriteLine(aService.toString());
            sw.Close();
        }//close

        /** Searches for a service with the given service code in the collection.
         *  @param serviceCode the code for which to search
         *  @return the service if found, otherwise null
         */
        public Service find(String serviceCode)
        {
            foreach (Service aService in serviceList)
                if (aService.getCode() == serviceCode)
                    return aService;

            return null;
        }//search

        /** Adds the given service to the collection.
         *  Services are stored in alphabetical order of service name to facilitate
         *  Provider Directory (Service Report) generation.
         *  @param aService the service to be added
         *  @throws IllegalArgumentException if the code of the service to be added
         *				is not unique.
         */
        public void add(Service aService)
        {
            Service tempService = find(aService.getCode());
            if (tempService != null) throw new System.ArgumentException
                        ("A service with this code already exists");
            for (int i = 0; i < serviceList.Count; i++)
            {
                if (aService.getName().CompareTo(serviceList[i].getName()) < 0)
                {
                    serviceList.Insert(i, aService);
                    return;
                }
            }
            //add to end of list
            serviceList.Add(aService);
        }//add

        /** Updates the given service in the collection.
         *  @param aService the service to be updated
         */
        public void update(Service aService)
        {
            //if the name of the service has been changed, the list may no longer be
            //in alphabetical order.  Thus, delete the service from the list
            //and add it again.
            delete(aService.getCode());
            add(aService);
        }//update

        /** Deletes the service with the given service number from the collection
         *  @param serviceCode the code of the service to delete
         */
        public void delete(String serviceCode)
        {
            for (int i = 0; i < serviceList.Count; i++)
                if (serviceList[i].getCode() == serviceCode)
                {
                    serviceList.RemoveAt(i);
                    return;
                }
        }//delete

        /** Returns all the services in the collection
         *  @return all the services
         */
        public List<Service> getAllOrderedByName()
        {
            return serviceList;
        }//getAllOrderedByName


        //***************instance variable	
        private List<Service> serviceList; //the collection of services

        //***************class variable 
        private static String FILE_NAME = "Services.txt";

    }//class Services
}
