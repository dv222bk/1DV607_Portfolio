using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.view
{
    class Console
    {
        public enum CurrentMenu
        {
            Main,
            CompactList,
            VerboseList,
            Member,
            AddMember,
            EditMember,
            Boat,
            AddBoat,
            EditBoat,
            Quit,
            Count
        }

        CurrentMenu currentMenu;

        // MAIN MENU
        public void DisplayMainMenu()
        {
            currentMenu = CurrentMenu.Main;
            System.Console.Clear();
            System.Console.WriteLine("Welcome, member, to The Happy Pirates member and boat list application");
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("c to view a compact member list");
            System.Console.WriteLine("v to view a verbose member list");
            System.Console.WriteLine("a to add a new member");
            System.Console.WriteLine("q to quit");
            System.Console.WriteLine("----------------------------------");

            string readResponse = ReadResponse();
            if (readResponse.Equals("c"))
            {
                currentMenu = CurrentMenu.CompactList;
            } 
            else if (readResponse.Equals("v"))
            {
                currentMenu = CurrentMenu.VerboseList;
            }
            else if (readResponse.Equals("a"))
            {
                currentMenu = CurrentMenu.AddMember;
            }
            else if (readResponse.Equals("q"))
            {
                currentMenu = CurrentMenu.Quit;
            }
        }

        // DISPLAY MEMBER LIST

        private void DisplayMemberListMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Type the memberID of the member you wish to edit");
            System.Console.WriteLine("q to go back");
            System.Console.WriteLine("----------------------------------");
        }

        public void DisplayCompactMemberList(List<model.Member> a_memberList)
        {
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
                    DisplayMember(member, false, false);
                }
            }
        }

        public void DisplayVerboseMemberList(List<model.Member> a_memberList)
        {
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
                    DisplayMember(member, true, true);
                }
            }
        }

        public int GetMemberListResponse()
        {
            string readResponse = ReadResponse();
            if (readResponse.Equals("q"))
            {
                currentMenu = CurrentMenu.Main;
            }
            else if (int.Parse(readResponse) != 0)
            {
                currentMenu = CurrentMenu.Member;
                return int.Parse(readResponse);
            }
            return 0;
        }

        // DISPLAY MEMBER

        public void DisplayMemberMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Type the boatID of the boat you want to edit");
            System.Console.WriteLine("b to add a boat");
            System.Console.WriteLine("c to change member information");
            System.Console.WriteLine("d to delete the member");
            System.Console.WriteLine("q to go back");
            System.Console.WriteLine("----------------------------------");
        }

        public void DisplayMember(model.Member a_member, bool showPNumber, bool showBoats)
        {
            System.Console.WriteLine("---");
            System.Console.WriteLine("MemberID: {0}", a_member.GetMemberID());
            System.Console.WriteLine("Name: {0}", a_member.GetName());

            if (showPNumber)
            {
                System.Console.WriteLine("Personal Number: {0}", a_member.GetPNumber());
            }

            if (a_member.GetBoatList().GetBoats().Count > 0)
            {
                if (showBoats)
                {
                    System.Console.WriteLine("Boats:");

                    foreach (model.Boat boat in a_member.GetBoatList().GetBoats())
                    {
                        DisplayBoat(a_member, boat.GetBoatID());
                    }
                }
                else
                {
                    System.Console.WriteLine("Boats: {0}", a_member.GetBoatList().GetBoats().Count);
                }
            }
        }

        public int GetMemberResponse()
        {
            string readResponse = ReadResponse();
            if (readResponse.Equals("b"))
            {
                currentMenu = CurrentMenu.AddBoat;
            }
            else if (readResponse.Equals("c"))
            {
                currentMenu = CurrentMenu.EditMember;
            }
            else if (readResponse.Equals("d"))
            {
                currentMenu = CurrentMenu.Main;
                return -1;
            }
            else if (readResponse.Equals("q"))
            {
                currentMenu = CurrentMenu.Main;
            }
            else if (int.Parse(readResponse) != 0)
            {
                currentMenu = CurrentMenu.EditBoat;
                return int.Parse(readResponse);
            }

            return 0;
        }

        // DISPLAY BOAT

        public void DisplayBoatMenu(model.Member a_member, int a_boatID)
        {
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Boat:");
            DisplayBoat(a_member, a_boatID);
            System.Console.WriteLine("The boat belongs to member:");
            DisplayMember(a_member, true, false);
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("e to edit");
            System.Console.WriteLine("d to delete");
            System.Console.WriteLine("q to go back");
            System.Console.WriteLine("----------------------------------");
        }

        public void DisplayBoat(model.Member a_member, int a_boatID)
        {
            foreach (model.Boat boat in a_member.GetBoatList().GetBoats()){
                if(a_boatID == boat.GetBoatID()) {
                    System.Console.WriteLine("BoatID: {0}", a_boatID);
                    System.Console.WriteLine("Type: {0}", boat.GetBoatType());
                    System.Console.WriteLine("Length: {0}", boat.GetLength());
                }
            }
        }

        public int GetBoatResponse()
        {
            string readResponse = ReadResponse();
            if (readResponse.Equals("e"))
            {
                currentMenu = CurrentMenu.EditBoat;
            }
            else if (readResponse.Equals("d"))
            {
                currentMenu = CurrentMenu.Member;
                return -1;
            }
            else if (readResponse.Equals("q"))
            {
                currentMenu = CurrentMenu.Member;
            }
            return 0;
        }

        // ADD MEMBER

        public void AddMember()
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Add a new member");
            System.Console.WriteLine("----------------------------------");
        }

        // CHANGE MEMBER INFORMATION

        public void EditMember(model.Member a_member)
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Editing member:");
            DisplayMember(a_member, true, false);
            System.Console.WriteLine("----------------------------------");
        }

        // ADD BOAT

        public void AddBoat(model.Member a_member)
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Adding a boat for member:");
            DisplayMember(a_member, true, true);
            System.Console.WriteLine("----------------------------------");
        }

        // EDIT BOAT

        public void EditBoat(model.Member a_member, int a_boatID)
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Editing boat:");
            DisplayBoat(a_member, a_boatID);
            System.Console.WriteLine("The boat belongs to member:");
            DisplayMember(a_member, true, false);
            System.Console.WriteLine("----------------------------------");
        }

        // WRITE MESSAGE

        public void WriteMessage(string a_message)
        {
            System.Console.WriteLine(a_message);
        }

        // GET RESPONSE

        public string ReadResponse()
        {
            return System.Console.ReadLine();
        }

        // GET CURRENT MENU

        public CurrentMenu GetCurrentMenu()
        {
            return currentMenu;
        }

        // SET CURRENT MENU

        public void SetCurrentMenu(CurrentMenu a_menu)
        {
            currentMenu = a_menu;
        }
    }
}
