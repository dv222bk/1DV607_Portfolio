using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.model
{
    class Boat
    {
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

        public Boat(Type a_type, double a_length)
        {
            m_type = a_type;
            m_length = a_length;
        }

        public Type getType() {
            return m_type;
        }

        public double getLength()
        {
            return m_length;
        }
    }
}
