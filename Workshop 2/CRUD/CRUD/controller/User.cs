using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.controller
{
    class User
    {
        model.MemberList m_memberList;
        view.Console m_console;

        public User(model.MemberList a_memberList, view.Console a_console)
        {
            m_memberList = a_memberList;
            m_console = a_console;
        }

        public void RunApplication()
        {
            m_console.DisplayMainMenu();
        }
    }
}
