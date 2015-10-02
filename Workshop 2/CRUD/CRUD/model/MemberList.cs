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
        List<Member> m_members;
        string m_file = "members.txt";

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
                            int pNumber = int.Parse(sr.ReadLine());
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
        public void AddMember(string a_name, int a_pNumber) 
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

            return highestMemberID++;
        }

        /// <summary>
        /// Saves all members in the list to the member file
        /// </summary>
        public void SaveMembers()
        {
            try
            {
                using (StreamWriter sw = File.AppendText(m_file))
                {
                    foreach (Member member in m_members)
                    {
                        sw.WriteLine(member.GetName());
                        sw.WriteLine(member.GetPNumber());
                        sw.WriteLine(member.GetMemberID());
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
        /// Change a members information
        /// </summary>
        /// <param name="a_memberID">int. MemberID of the member</param>
        /// <param name="a_name">string. Name of the member</param>
        /// <param name="a_pNumber">int. Personal number of the member</param>
        public void ChangeMemberInfo(int a_memberID, string a_name, int a_pNumber)
        {
            try
            {
                foreach (Member member in m_members)
                {
                    if (member.GetMemberID() == a_memberID)
                    {
                        member.SetName(a_name);
                        member.SetPNumber(a_pNumber);
                        SaveMembers();
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
        /// Remove a member from the memberlist
        /// </summary>
        /// <param name="a_memberID">int. MemberID of the member</param>
        public void RemoveMember(int a_memberID)
        {
            foreach (Member member in m_members)
            {
                if (member.GetMemberID() == a_memberID)
                {
                    m_members.Remove(member);
                    SaveMembers();
                    break;
                }
            }
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
