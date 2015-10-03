using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.model
{
    class Boat
    {

        /// <summary>
        /// Enum describing the different boat types a boat can have
        /// </summary>
        public enum Type
        {
            Sailboat,
            Motorsailer,
            Canoe,
            Other,
            Count
        }

        Type m_type;
        double m_length;
        int m_boatID;

        public Boat(Type a_type, double a_length, int a_boatID)
        {
            m_type = a_type;
            m_length = a_length;
            m_boatID = a_boatID;
        }

        /// <summary>
        /// Get the boats type
        /// </summary>
        /// <returns>Type. The boats type</returns>
        public Type GetBoatType() {
            return m_type;
        }

        /// <summary>
        /// Get the boats length
        /// </summary>
        /// <returns>double. The boats length</returns>
        public double GetLength()
        {
            return m_length;
        }

        /// <summary>
        /// Get the boats ID
        /// </summary>
        /// <returns>int. The boats ID</returns>
        public int GetBoatID()
        {
            return m_boatID;
        }

        /// <summary>
        /// Set the boats type
        /// </summary>
        /// <param name="a_type">Type. The boats type</param>
        public void SetType(Type a_type)
        {
            m_type = a_type;
        }

        /// <summary>
        /// Set the boats length
        /// </summary>
        /// <param name="a_length">double. The boats length</param>
        public void SetLength(double a_length)
        {
            m_length = a_length;
        }
    }
}
