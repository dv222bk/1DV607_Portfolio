using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.model.dal
{
    class MemberDAL
    {
        private string m_memberFilePath = "database/members.txt";

        /// <summary>
        /// Read all members in a member list
        /// </summary>
        public List<model.Member> GetMembersFromFile()
        {
            try
            {
                List<model.Member> members = new List<Member>();
                if (File.Exists(m_memberFilePath))
                {
                    using (StreamReader sr = new StreamReader(m_memberFilePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string name = sr.ReadLine();
                            long pNumber = long.Parse(sr.ReadLine());
                            int memberID = int.Parse(sr.ReadLine());
                            model.BoatList boatList = new model.BoatList(memberID);
                            members.Add(new model.Member(name, pNumber, memberID, boatList));
                        }
                        sr.Close();
                    }
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saves all members in the list to the member file
        /// </summary>
        /// <param name="a_memberList">List<Member>. A list of members to be saved</param>
        public void SaveMembers(List<model.Member> a_memberList)
        {
            try
            {
                File.Create(m_memberFilePath).Dispose();
                using (StreamWriter sw = File.AppendText(m_memberFilePath))
                {
                    if (a_memberList.Count() > 0)
                    {
                        foreach (model.Member member in a_memberList)
                        {
                            sw.WriteLine(member.GetName());
                            sw.WriteLine(member.GetPNumber());
                            sw.WriteLine(member.GetMemberID());
                        }
                        sw.Close();
                    }
                    else
                    {
                        sw.Close();
                        File.WriteAllText(m_memberFilePath, String.Empty);
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
