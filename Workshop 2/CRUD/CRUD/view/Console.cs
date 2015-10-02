using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.view
{
    class Console
    {
        public void DisplayMainMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("Welcome, member, to The Happy Pirates member and boat list application");
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("1 to view a compact member list");
            System.Console.WriteLine("2 to view a verbose member list");
            System.Console.WriteLine("3 to create a new member");
            System.Console.WriteLine("q to quit");
            System.Console.WriteLine("----------------------------------");
        }

        private void DisplayMemberListMenu()
        {
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Type the memberID of the member you wish to edit");
            System.Console.WriteLine("q to go back");
            System.Console.WriteLine("----------------------------------");
        }

        public void DisplayMember(model.Member a_member)
        {
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Type the boatID of the boat you want to edit");
            System.Console.WriteLine("b to add a boat");
            System.Console.WriteLine("c to change member information");
            System.Console.WriteLine("d to delete the member");
            System.Console.WriteLine("q to go back");
            System.Console.WriteLine("----------------------------------");
            DisplayMemberWithBoats(a_member);
        }

        public void DisplayCompactMemberList(List<model.Member> a_memberList)
        {
            System.Console.Clear();
            DisplayMemberListMenu();
            System.Console.WriteLine("Compact memberlist");
            if (a_memberList.Count < 1)
            {
                System.Console.WriteLine("There are no members in the list!");
            }
            else
            {
                foreach (model.Member member in a_memberList)
                {
                    System.Console.WriteLine("---");
                    System.Console.WriteLine("MemberID: {0}", member.GetMemberID());
                    System.Console.WriteLine("Name: {0}", member.GetName());
                    System.Console.WriteLine("Boats: {0}", member.GetBoatList().GetBoats().Count);
                }
            }
        }

        private void DisplayMemberWithBoats(model.Member a_member)
        {
            System.Console.WriteLine("---");
            System.Console.WriteLine("MemberID: {0}", a_member.GetMemberID());
            System.Console.WriteLine("Name: {0}", a_member.GetName());
            System.Console.WriteLine("Personal Number: {0}", a_member.GetPNumber());
            List<model.Boat> memberBoatList = a_member.GetBoatList().GetBoats();
            if (memberBoatList.Count > 0)
            {
                System.Console.WriteLine("Boats:");

                foreach (model.Boat boat in memberBoatList)
                {
                    System.Console.WriteLine("BoatID: {0}", boat.GetBoatID());
                    System.Console.WriteLine("Type: {0}", boat.GetType());
                    System.Console.WriteLine("Length: {0}", boat.GetLength());
                }
            }
        }

        public void DisplayVerboseMemberList(List<model.Member> a_memberList)
        {
            System.Console.Clear();
            DisplayMemberListMenu();
            System.Console.WriteLine("Verbose memberlist");
            if (a_memberList.Count < 1)
            {
                System.Console.WriteLine("There are no members in the list!");
            }
            else
            {
                foreach (model.Member member in a_memberList)
                {
                    DisplayMemberWithBoats(member);
                }
            }
        }

        public char ReadKey()
        {
            return System.Console.ReadKey().KeyChar;
        }
    }
}
