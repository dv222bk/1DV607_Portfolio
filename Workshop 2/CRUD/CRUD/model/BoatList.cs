using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.model
{
    class BoatList
    {
        List<Boat> m_boats;
        int m_memberID;
        string m_file = "boats.txt";

        public BoatList(int a_memberID)
        {
            m_memberID = a_memberID;
            getBoatsFromFile();
        }

        /// <summary>
        /// Read all boats in a boatlist connected to the member with member id m_memberID
        /// </summary>
        private void getBoatsFromFile()
        {
            List<Boat> boatList = new List<Boat>();
            try
            {
                using (StreamReader sr = new StreamReader(m_file))
                {
                    while (!sr.EndOfStream)
                    {
                        Boat.Type type = (Boat.Type)int.Parse(sr.ReadLine());
                        double length = double.Parse(sr.ReadLine());
                        int memberID = int.Parse(sr.ReadLine());

                        if (memberID == m_memberID)
                        {
                            m_boats.Add(new Boat(type, length, getUniqueBoatID()));
                        }
                    }
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save all boats in the list to the boat list file
        /// </summary>
        public void saveBoats()
        {
            try
            {
                List<Boat> boatList = new List<Boat>();

                // Add all boats not owned by the member to boatList
                using (StreamReader sr = new StreamReader(m_file))
                {
                    while (!sr.EndOfStream)
                    {
                        Boat.Type type = (Boat.Type)int.Parse(sr.ReadLine());
                        double length = double.Parse(sr.ReadLine());
                        int memberID = int.Parse(sr.ReadLine());

                        if (memberID != m_memberID)
                        {
                            boatList.Add(new Boat(type, length, getUniqueBoatID()));
                        }
                    }
                    sr.Close();
                }

                // Add all boats owned by the member to the boatList
                boatList.AddRange(m_boats);

                // Save all boats in boatList to the file
                using (StreamWriter sw = new StreamWriter(m_file))
                {
                    foreach (Boat boat in boatList)
                    {
                        sw.WriteLine((int)boat.getType());
                        sw.WriteLine(boat.getLength());
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get a unique boat ID, only used in the application
        /// </summary>
        /// <returns>int. a unique boat ID for the member</returns>
        private int getUniqueBoatID()
        {
            int highestBoatID = 0;

            foreach (Boat boat in m_boats)
            {
                if (boat.getBoatID() > highestBoatID)
                {
                    highestBoatID = boat.getBoatID();
                }
            }

            return highestBoatID++;
        }

        /// <summary>
        /// Remove a boat from the boat list
        /// </summary>
        /// <param name="a_boatID">int. The boat ID of the boat</param>
        public void removeBoat(int a_boatID)
        {
            foreach (Boat boat in m_boats)
            {
                if (boat.getBoatID() == a_boatID)
                {
                    m_boats.Remove(boat);
                    saveBoats();
                    break;
                }
            }
        }

        /// <summary>
        /// Add a boat to the boat list
        /// </summary>
        /// <param name="a_type">Boat.Type. Type of boat</param>
        /// <param name="a_length">double. The length of the boat</param>
        public void addBoat(Boat.Type a_type, double a_length)
        {
            try
            {
                m_boats.Add(new Boat(a_type, a_length, getUniqueBoatID()));
                saveBoats();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Change a boats information
        /// </summary>
        /// <param name="a_boatID">int. The boats ID</param>
        /// <param name="a_type">Boat.Type. The type of boat</param>
        /// <param name="a_length">double. The length of the boat</param>
        public void changeBoatInfo(int a_boatID, Boat.Type a_type, double a_length)
        {
            try
            {
                foreach (Boat boat in m_boats)
                {
                    if (boat.getBoatID() == a_boatID)
                    {
                        boat.setType(a_type);
                        boat.setLength(a_length);
                        saveBoats();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all boats in a list
        /// </summary>
        /// <returns>List<Boat>. A list of all boats for member m_memberID</returns>
        public List<Boat> getBoats()
        {
            return m_boats;
        }
    }
}
