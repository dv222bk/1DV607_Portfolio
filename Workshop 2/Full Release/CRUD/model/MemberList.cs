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
        private List<Member> m_members = new List<Member>();
        private dal.MemberDAL m_memberDAL = new dal.MemberDAL();

        public MemberList()
        {
            m_members = m_memberDAL.GetMembersFromFile();
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
                m_memberDAL.SaveMembers(m_members);
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
        /// Change a members information
        /// </summary>
        /// <param name="a_memberID">int. MemberID of the member to be edited</param>
        /// <param name="a_name">string. Name of the member</param>
        /// <param name="a_pNumber">int. Personal number of the member</param>
        public void ChangeMemberInfo(int a_memberID, string a_name, long a_pNumber)
        {
            try
            {
                foreach (Member member in m_members)
                {
                    if (member.GetMemberID() == a_memberID)
                    {
                        member.SetName(a_name);
                        member.SetPNumber(a_pNumber);
                        m_memberDAL.SaveMembers(m_members);
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
        /// <param name="a_member">Member. Member to be removed</param>
        public void RemoveMember(Member a_member)
        {
            a_member.GetBoatList().RemoveAllBoats();
            m_members.Remove(a_member);
            m_memberDAL.SaveMembers(m_members);
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
