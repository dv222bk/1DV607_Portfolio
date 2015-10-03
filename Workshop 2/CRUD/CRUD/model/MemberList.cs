using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.model
{
    class MemberList
    {
        List<Member> m_members = new List<Member>();
        string m_file = "database/members.txt";

        public MemberList()
        {
            GetMembersFromFile();
        }

        /// <summary>
        /// Read all members in a member list
        /// </summary>
        private void GetMembersFromFile()
        {
            try
            {
                if (File.Exists(m_file))
                {
                    using (StreamReader sr = new StreamReader(m_file))
                    {
                        while (!sr.EndOfStream)
                        {
                            string name = sr.ReadLine();
                            long pNumber = long.Parse(sr.ReadLine());
                            int memberID = int.Parse(sr.ReadLine());
                            BoatList boatList = new BoatList(memberID);
                            m_members.Add(new Member(name, pNumber, memberID, boatList));
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
        /// Add a member to the member list
        /// </summary>
        /// <param name="a_name">string. The name of the member</param>
        /// <param name="a_pNumber">int. The personalnumber of the member</param>
        public void AddMember(string a_name, long a_pNumber) 
        {
            try
            {
                int memberID = GetUniqueMemberID();
                m_members.Add(new Member(a_name, a_pNumber, memberID, new BoatList(memberID)));
                SaveMembers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get a unique memberID based on the highest memberID in the memberlist
        /// </summary>
        /// <returns>int, a unique memberID</returns>
        private int GetUniqueMemberID()
        {
            int highestMemberID = 0;

            foreach (Member member in m_members)
            {
                if (member.GetMemberID() > highestMemberID)
                {
                    highestMemberID = member.GetMemberID();
                }
            }

            return highestMemberID + 1;
        }

        /// <summary>
        /// Saves all members in the list to the member file
        /// </summary>
        public void SaveMembers()
        {
            try
            {
                File.Create(m_file).Dispose();
                using (StreamWriter sw = File.AppendText(m_file))
                {
                    if (m_members.Count() > 0)
                    {
                        foreach (Member member in m_members)
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
        /// Change a members information
        /// </summary>
        /// <param name="a_memberID">int. MemberID of the member</param>
        /// <param name="a_name">string. Name of the member</param>
        /// <param name="a_pNumber">int. Personal number of the member</param>
        public void ChangeMemberInfo(Member a_member, string a_name, long a_pNumber)
        {
            try
            {
                a_member.SetName(a_name);
                a_member.SetPNumber(a_pNumber);
                SaveMembers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove a member from the memberlist
        /// </summary>
        /// <param name="a_member">Member. Member to be removed</param>
        public void RemoveMember(Member a_member)
        {
            a_member.GetBoatList().RemoveAllBoats();
            m_members.Remove(a_member);
            SaveMembers();
        }

        /// <summary>
        /// Get the member list
        /// </summary>
        /// <returns>List<Member>. List of all members</returns>
        public List<Member> GetMembers()
        {
            return m_members;
        }
    }
}
