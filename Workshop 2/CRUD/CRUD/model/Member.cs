using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.model
{
    class Member
    {
        string m_name;
        int m_pNumber;
        int m_memberID;
        BoatList m_boatList;

        public Member(string a_name, int a_pNumber, int a_memberID, BoatList a_boatList)
        {
            if (a_name == "" || a_name == null)
            {
                throw new ArgumentNullException("name is null");
            }

            if(a_pNumber.ToString().Length != 12) 
            {
                throw new ArgumentException("pNumber has an invalid format. Should be YYYYMMDDXXXX");
            }

            if (a_memberID < 1)
            {
                throw new ArgumentException("Invalid memberID");
            }

            m_name = a_name;
            m_pNumber = a_pNumber;
            m_memberID = a_memberID;
            m_boatList = a_boatList;
        }

        /// <summary>
        /// Get the members name
        /// </summary>
        /// <returns>string. The members name</returns>
        public string GetName()
        {
            return m_name;
        }

        /// <summary>
        /// Get the members personal number
        /// </summary>
        /// <returns>int. The members personal number</returns>
        public int GetPNumber()
        {
            return m_pNumber;
        }

        /// <summary>
        /// Get the members memberID
        /// </summary>
        /// <returns>int. The members memberID</returns>
        public int GetMemberID()
        {
            return m_memberID;
        }

        /// <summary>
        /// Get the members list of boat
        /// </summary>
        /// <returns>BoatList. The members list of boats</returns>
        public BoatList GetBoatList()
        {
            return m_boatList;
        }

        /// <summary>
        /// Set the members name
        /// </summary>
        /// <param name="a_name">string. The members name</param>
        public void SetName(string a_name) {
            if (a_name == "" || a_name == null)
            {
                throw new ArgumentNullException("name is null");
            }

            m_name = a_name;
        }

        /// <summary>
        /// Set the members personal number
        /// </summary>
        /// <param name="a_pNumber">int. The members personal number</param>
        public void SetPNumber(int a_pNumber)
        {
            if (a_pNumber.ToString().Length != 12)
            {
                throw new ArgumentException("pNumber has an invalid format. Should be YYYYMMDDXXXX");
            }

            m_pNumber = a_pNumber;
        }
    }
}
