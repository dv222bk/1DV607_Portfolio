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
        List<Boat> m_boats = new List<Boat>();
        int m_memberID;
        string m_file = "database/boats.txt";
        string m_memberFile = "database/members.txt";

        public BoatList(int a_memberID)
        {
            m_memberID = a_memberID;
            GetBoatsFromFile();
        }

        /// <summary>
        /// Read all boats in a boatlist connected to the member with member id m_memberID
        /// </summary>
        private void GetBoatsFromFile()
        {
            try
            {
                if (File.Exists(m_file))
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
                                m_boats.Add(new Boat(type, length, GetUniqueBoatID()));
                            }
                        }
                        sr.Close();
                    }
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
        public void SaveBoats()
        {
            try
            {
                List<BoatList> boatLists = new List<BoatList>();
                if (File.Exists(m_file))
                {
                    // Add all boatLists not owned by the saving member to boatLists
                    using (StreamReader sr = new StreamReader(m_memberFile))
                    {
                        while (!sr.EndOfStream)
                        {
                            sr.ReadLine();
                            sr.ReadLine();
                            int memberID = int.Parse(sr.ReadLine());
                            if (memberID != m_memberID) {
                                boatLists.Add(new BoatList(memberID));
                            }
                        }
                        sr.Close();
                    }
                }

                // Add this boatList to the boatLists
                boatLists.Add(this);

                // Remove the old boats file to make room for the new one
                File.Create(m_file).Dispose();

                // Save all boats to the file
                using (StreamWriter sw = File.AppendText(m_file))
                {
                    if (boatLists.Count > 0)
                    {
                        foreach (BoatList boatList in boatLists)
                        {
                            foreach (Boat boat in boatList.GetBoats())
                            {
                                sw.WriteLine((int)boat.GetBoatType());
                                sw.WriteLine(boat.GetLength());
                                sw.WriteLine(boatList.GetMemberID());
                            }
                        }
                        sw.Close();
                    }
                    else
                    {
                        sw.Close();
                        File.WriteAllText(m_file, String.Empty);
                    }
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
        private int GetUniqueBoatID()
        {
            int highestBoatID = 0;

            foreach (Boat boat in m_boats)
            {
                if (boat.GetBoatID() > highestBoatID)
                {
                    highestBoatID = boat.GetBoatID();
                }
            }

            return highestBoatID + 1;
        }

        /// <summary>
        /// Remove a boat from the boat list
        /// </summary>
        /// <param name="a_boatID">int. The boat ID of the boat</param>
        public void RemoveBoat(int a_boatID)
        {
            foreach (Boat boat in m_boats)
            {
                if (boat.GetBoatID() == a_boatID)
                {
                    m_boats.Remove(boat);
                    SaveBoats();
                    break;
                }
            }
        }

        /// <summary>
        /// Removes all boats from the list
        /// </summary>
        public void RemoveAllBoats()
        {
            m_boats = new List<Boat>();
            SaveBoats();
        }

        /// <summary>
        /// Add a boat to the boat list
        /// </summary>
        /// <param name="a_type">Boat.Type. Type of boat</param>
        /// <param name="a_length">double. The length of the boat</param>
        public void AddBoat(Boat.Type a_type, double a_length)
        {
            try
            {
                m_boats.Add(new Boat(a_type, a_length, GetUniqueBoatID()));
                SaveBoats();
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
        public void ChangeBoatInfo(int a_boatID, Boat.Type a_type, double a_length)
        {
            try
            {
                foreach (Boat boat in m_boats)
                {
                    if (boat.GetBoatID() == a_boatID)
                    {
                        boat.SetType(a_type);
                        boat.SetLength(a_length);
                        SaveBoats();
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
        public List<Boat> GetBoats()
        {
            return m_boats;
        }

        /// <summary>
        /// Get the memberID of the owner of this boat list
        /// </summary>
        /// <returns>int. The memberID of the member</returns>
        public int GetMemberID()
        {
            return m_memberID;
        }
    }
}
