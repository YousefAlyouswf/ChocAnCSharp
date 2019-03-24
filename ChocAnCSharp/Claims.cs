using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAnCSharp
{
    public class Claims
    {
        /**
         * Creates a new empty Claims object
         */
        public Claims()
        {
            claimList = new List<Claim>();
        }//default constructor

        /** Reads all the claims in the FILE_NAME text file into the collection.
         *  @throws NumberFormatException if the provider or member number is 
         *             not a valid integer
         *  @throws IllegalArgumentException if any of the values are
         *             invalid.
         *  @throws IndexOutOfBoundsException if there are 
         *             not enough values in the string
         *  @throws ParseException if the submission date and time or the 
         *             service date is not in the correct format
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
                        Claim aClaim = new Claim(line);
                        add(aClaim);
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }
        }//open

        /** Writes all the claims in the collection to the FILE_NAME text file.
         *  @throws FileNotFoundException if the file cannot be created.
         */
        public void close()
        {
            StreamWriter sw = new StreamWriter(FILE_NAME);
            foreach (Claim aClaim in claimList)
                sw.WriteLine(aClaim.toString());
            sw.Close();
        }//close

        /** Adds the given claim to the collection.
	     *  @param aClaim the claim to be added
	     */
        public void add(Claim aClaim)
        {
            claimList.Add(aClaim);
        }//add

        /** Finds all the claims submitted by a given provider.
	     *  @param providerNumber the provider's number
	     *  @return all the claims submitted by the given provider
	     */
        public List<Claim> findByProvider(long providerNumber)
        {
            List<Claim> providerClaims = new List<Claim>();
            foreach (Claim aClaim in claimList)
                if (aClaim.getProviderNumber() == providerNumber)
                    providerClaims.Add(aClaim);

            return providerClaims;
        }//findByProvider

        /** Finds all the claims for services rendered to a given member.
	     *  @param memberNumber the member's number
	     *  @return all the claims for services rendered to the given member
	     */
        public List<Claim> findByMember(long memberNumber)
        {
            List<Claim> memberClaims = new List<Claim>();
            foreach (Claim aClaim in claimList)
                if (aClaim.getMemberNumber() == memberNumber)
                    memberClaims.Add(aClaim);

            //Sort by service date (Use a bubble sort for clarity)
            for (int i = memberClaims.Count - 1; i > 0; i--)
                for (int j = 0; j < i; j++)
                    if (memberClaims[j].getServiceDate() > memberClaims[i].getServiceDate())
                    {
                        Claim temp = memberClaims[i];
                        memberClaims[i] = memberClaims[j];
                        memberClaims[j] = temp;
                    }

            return memberClaims;
        }//findByMember

        //class variable 
        private static String FILE_NAME = "Claims.txt";
        //instance variable  
        private List<Claim> claimList;

    }//class Claims
}
