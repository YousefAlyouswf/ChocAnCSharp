using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class Persons
    {

        //*******************constructor
        /**
         * Creates a new empty Persons object
         */
        public Persons()
        {
            personList = new List<Person>();
        }//default constructor

        //*******************utility methods

        /** Initializes the next number to be allocated to a Person with the 
         *  number saved (in the FILE_NAME text file).
         *  @throws NumberFormatException if the person number is not a valid integer
         *  @throws IllegalArgumentException if the person number is invalid.
         *  @throws ArrayIndexOutOfBoundsException if there is not at least one value 
         *          in the string
         */
        protected void open()
        {
            try
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(FILE_NAME))
                {
                    Person.setNextNumber(long.Parse(file.ReadLine()));
                }
            }
            catch (FileNotFoundException)
            {
                Person.setNextNumber(1);
            }
        }//open

        /** Saves the next number to be allocated to a Person (to the FILE_NAME 
         *  text file).
         *  @throws FileNotFoundException if the file cannot be created
         */
        protected void close()
        {
            StreamWriter sw = new StreamWriter(FILE_NAME);
            sw.WriteLine(Person.getNextNumber());
            sw.Close();
        }//close 

        /** Writes all the persons in the collection to the given text file.
	     *  @param fileName the name of the text file
	     *  @throws FileNotFoundException if the file cannot be created.
	     */
        protected void saveToFile(String fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            foreach (Person aPerson in personList)
                sw.WriteLine(aPerson.toString());
            sw.Close();
        }//saveToFile

        /** Searches for a person with the given person number in the collection.
         *  @param personNumber the number for which to search
         *  @return the person if found, otherwise null
         */
        protected Person search(long personNumber)
        {
            foreach (Person aPerson in personList)
                if (aPerson.getNumber() == personNumber)
                    return aPerson;

            return null;
        }//search

        /** Adds the given person to the collection.
         *  @param aPerson the person to be added
         */
        public void add(Person aPerson)
        {
            personList.Add(aPerson);
        }//add

        /** Updates the given person in the collection.
         *  @param aPerson the person to be updated
         */
        public void update(Person aPerson)
        {
            //Unnecessary to do anything in the ArrayList implementation
            //            -- aPerson is already in the list
            /*Person existingPerson = find(aPerson.getPersonNumber());
            if (existingPerson == null)
                throw new IllegalArgumentException("Invalid person number: " 
                        + aPerson.getPersonNumber());
            existingPerson = aPerson.clone();
            */
        }//update

        /** Deletes the person with the given person number from the collection.
         *  @param personNumber the number of the person to delete
         */
        public void delete(long personNumber)
        {
            for (int i = 0; i < personList.Count; i++)
                if (personList[i].getNumber() == personNumber)
                {
                    personList.Remove(personList[i]);
                    return;
                }
        }//delete

        /** Returns all the persons in the collection
         *  @return all persons
         */
        public List<Person> getAll()
        {
            return personList;
        }//getAll

        //*******************instance variable 
        private List<Person> personList;

        //*******************class variable 
        private static String FILE_NAME = "Persons.txt";

    }//class Persons
}
