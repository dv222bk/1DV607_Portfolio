using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            model.MemberList memberList = new model.MemberList();
            view.Console console = new view.Console();
            controller.User user = new controller.User(memberList, console);

            user.RunApplication();
        }
    }
}
