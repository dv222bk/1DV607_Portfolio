using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.view
{
    class Console
    {
        /// <summary>
        /// Enum describing what menu the user is currently at.
        /// </summary>
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

        private CurrentMenu currentMenu;

        // MAIN MENU

        /// <summary>
        /// Displays the main menu to the user.
        /// </summary>
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
        }

        /// <summary>
        /// Read a response from the user, connected with the main menu, and change the currentMenu based on the response
        /// </summary>
        public void MainMenuResponse()
        {
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


        /// <summary>
        /// Displays the memberlist menu to the user
        /// </summary>
        private void DisplayMemberListMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Type the memberID of the member you wish to edit");
            System.Console.WriteLine("q to go back");
            System.Console.WriteLine("----------------------------------");
        }

        /// <summary>
        /// Displays a compact member list, which contains basic information, to the user. 
        /// </summary>
        /// <param name="a_memberList">List<model.Member>. Memberlist to display</param>
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

        /// <summary>
        /// Displays a verbose member list, with all information on record, to the user.
        /// </summary>
        /// <param name="a_memberList">List<model.Member>. MemberList to display</param>
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

        /// <summary>
        /// Read a response from the user, connected with the member list, and change the currentMenu based on the response
        /// </summary>
        /// <returns>0, unless the user enters a number above 0, then returns the number</returns>
        public int GetMemberListResponse()
        {
            string readResponse = ReadResponse();
            if (readResponse.Equals("q"))
            {
                currentMenu = CurrentMenu.Main;
            }
            else
            {
                int number = 0;
                bool isNumeric = int.TryParse(readResponse, out number);

                if (isNumeric && number > 0)
                {
                    currentMenu = CurrentMenu.Member;
                    return int.Parse(readResponse);
                }
            }
            return 0;
        }

        // DISPLAY MEMBER

        /// <summary>
        /// Displays the member menu to the user
        /// </summary>
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

        /// <summary>
        /// Display a member to the user
        /// </summary>
        /// <param name="a_member">model.Member. The member to be displayed</param>
        /// <param name="showPNumber">bool. True if the Personal Number should be displayed, false otherwise</param>
        /// <param name="showBoats">bool. True if the boats the user owns should be displayed, false if only the number of boats owned should be displayed</param>
        public void DisplayMember(model.Member a_member, bool showPNumber, bool showBoats)
        {
            System.Console.WriteLine("---");
            System.Console.WriteLine("MemberID: {0}", a_member.GetMemberID());
            System.Console.WriteLine("Name: {0}", a_member.GetName());

            if (showPNumber)
            {
                System.Console.WriteLine("Personal Number: {0}", a_member.GetPNumber());
            }

            List<model.Boat> boats = a_member.GetBoatList().GetBoats();

            if (boats.Count > 0)
            {
                if (showBoats)
                {
                    System.Console.WriteLine("Boats:");

                    foreach (model.Boat boat in boats)
                    {
                        DisplayBoat(boat);
                    }
                }
                else
                {
                    System.Console.WriteLine("Boats: {0}", boats.Count);
                }
            }
        }

        /// <summary>
        /// Read a response from the user, connected with the member view, and change the currentMenu based on the response
        /// </summary>
        /// <returns>0, unless the user enters a number above 0, then returns the number. In case the user wants to the delete the member, -1 is returned</returns>
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
            else
            {
                int number = 0;
                bool isNumeric = int.TryParse(readResponse, out number);

                if (isNumeric && number > 0)
                {
                    currentMenu = CurrentMenu.Boat;
                    return int.Parse(readResponse);
                }
            }

            return 0;
        }

        // DISPLAY BOAT

        /// <summary>
        /// Display the boat menu to the user
        /// </summary>
        /// <param name="a_member">model.Member. The member that ownes the boat</param>
        /// <param name="a_boatID">int. The boat to be edited</param>
        public void DisplayBoatMenu(model.Member a_member, int a_boatID)
        {
            System.Console.Clear();
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

        /// <summary>
        /// Displays a boat to the user
        /// </summary>
        /// <param name="a_member">model.Member. The member that ownes the boat</param>
        /// <param name="a_boatID">int. The ID of that boat to be displayed</param>
        public void DisplayBoat(model.Member a_member, int a_boatID)
        {
            foreach (model.Boat boat in a_member.GetBoatList().GetBoats()){
                if(a_boatID == boat.GetBoatID()) {
                    System.Console.WriteLine("BoatID: {0}", a_boatID);
                    System.Console.WriteLine("Type: {0}", boat.GetBoatType());
                    System.Console.WriteLine("Length: {0}", boat.GetLength());
                    break;
                }
            }
        }

        /// <summary>
        /// Displays a boat to the user
        /// </summary>
        /// <param name="a_boat">model.Boat. The boat to be displayed</param>
        public void DisplayBoat(model.Boat a_boat)
        {
            System.Console.WriteLine("BoatID: {0}", a_boat.GetBoatID());
            System.Console.WriteLine("Type: {0}", a_boat.GetBoatType());
            System.Console.WriteLine("Length: {0}", a_boat.GetLength());
        }

        /// <summary>
        /// Read a response from the user, connected with the boatmenu, and change the currentMenu based on the response
        /// </summary>
        /// <returns>0, unless the user wants to the delete the boat, then -1 is returned</returns>
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
        
        /// <summary>
        /// Display a header descirbing the add member menu.
        /// </summary>
        public void AddMember()
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Add a new member");
            System.Console.WriteLine("----------------------------------");
        }

        // CHANGE MEMBER INFORMATION

        /// <summary>
        /// Display a header describing the edit member menu
        /// </summary>
        /// <param name="a_member">model.Member, the member being edited</param>
        public void EditMember(model.Member a_member)
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Editing member:");
            DisplayMember(a_member, true, false);
            System.Console.WriteLine("----------------------------------");
        }

        // ADD BOAT

        /// <summary>
        /// Display a header describing the add boat menu
        /// </summary>
        /// <param name="a_member">model.Member, the member to add a boat to</param>
        public void AddBoat(model.Member a_member)
        {
            System.Console.Clear();
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine("Adding a boat for member:");
            DisplayMember(a_member, true, true);
            System.Console.WriteLine("----------------------------------");
        }

        // EDIT BOAT

        /// <summary>
        /// Display a header descirbing the edit boat menu
        /// </summary>
        /// <param name="a_member">model.Member, the member whos boat is being edited</param>
        /// <param name="a_boatID">int, the boat id of the boat being edited</param>
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

        /// <summary>
        /// Write a message in the console
        /// </summary>
        /// <param name="a_message">string. The message that should be written</param>
        public void WriteMessage(string a_message)
        {
            System.Console.WriteLine(a_message);
        }

        // GET RESPONSE

        /// <summary>
        /// Read a response from the user
        /// </summary>
        /// <returns>string. The response from the user</returns>
        public string ReadResponse()
        {
            return System.Console.ReadLine();
        }

        // GET CURRENT MENU

        /// <summary>
        /// Gets the current menu the user is currently at
        /// </summary>
        /// <returns>CurrentMenu. the current menu enum value</returns>
        public CurrentMenu GetCurrentMenu()
        {
            return currentMenu;
        }

        // SET CURRENT MENU

        /// <summary>
        /// Set the menu the user should be at
        /// </summary>
        /// <param name="a_menu">CurrentMenu. the currentmenu enum value</param>
        public void SetCurrentMenu(CurrentMenu a_menu)
        {
            currentMenu = a_menu;
        }
    }
}
