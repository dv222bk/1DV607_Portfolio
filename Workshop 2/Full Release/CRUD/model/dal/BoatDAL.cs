using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.model.dal
{
    class BoatDAL
    {
        private string m_boatFilePath = "database/boats.txt";
        private string m_memberFilePath = "database/members.txt";
        private int m_memberID;

        public BoatDAL(int a_memberID)
        {
            m_memberID = a_memberID;
        }

        /// <summary>
        /// Read all boats in a boatlist connected to the member with member id m_memberID
        /// </summary>
        /// <returns>List<Boat>. A list of boats the member owns</returns>
        public List<model.Boat> GetBoatsFromFile()
        {
            try
            {
                List<model.Boat> boats = new List<model.Boat>();
                int uniqueBoatID = 1;
                if (File.Exists(m_boatFilePath))
                {
                    using (StreamReader sr = new StreamReader(m_boatFilePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            model.Boat.Type type = (model.Boat.Type)int.Parse(sr.ReadLine());
                            double length = double.Parse(sr.ReadLine());
                            int memberID = int.Parse(sr.ReadLine());

                            if (memberID == m_memberID)
                            {
                                boats.Add(new model.Boat(type, length, uniqueBoatID));
                                uniqueBoatID += 1;
                            }
                        }
                        sr.Close();
                    }
                }
                return boats;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save all boats in the list to the boat list file
        /// </summary>
        /// <param name="a_memberBoatList">BoatList. A list of boats to be saved</param>
        public void SaveBoats(model.BoatList a_memberBoatList)
        {
            try
            {
                List<model.BoatList> boatLists = new List<model.BoatList>();
                if (File.Exists(m_boatFilePath))
                {
                    // Add all boatLists not owned by the saving member to boatLists
                    using (StreamReader sr = new StreamReader(m_memberFilePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            sr.ReadLine();
                            sr.ReadLine();
                            int memberID = int.Parse(sr.ReadLine());
                            if (memberID != m_memberID)
                            {
                                boatLists.Add(new model.BoatList(memberID));
                            }
                        }
                        sr.Close();
                    }
                }

                // Add this boatList to the boatLists
                boatLists.Add(a_memberBoatList);

                // Remove the old boats file to make room for the new one
                File.Create(m_boatFilePath).Dispose();

                // Save all boats to the file
                using (StreamWriter sw = File.AppendText(m_boatFilePath))
                {
                    if (boatLists.Count > 0)
                    {
                        foreach (model.BoatList boatList in boatLists)
                        {
                            foreach (model.Boat boat in boatList.GetBoats())
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
                        File.WriteAllText(m_boatFilePath, String.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
