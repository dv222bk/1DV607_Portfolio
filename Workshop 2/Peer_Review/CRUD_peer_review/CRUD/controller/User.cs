using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.controller
{
    class User
    {
        private model.MemberList m_memberList;
        private view.Console m_console;

        public User(model.MemberList a_memberList, view.Console a_console)
        {
            m_memberList = a_memberList;
            m_console = a_console;
        }

        /// <summary>
        /// Run the application
        /// </summary>
        public void RunApplication()
        {
            Main();
        }

        /// <summary>
        /// Main menu
        /// </summary>
        private void Main()
        {
            m_console.DisplayMainMenu();
            m_console.MainMenuResponse();
            GoToCurrentMenu();
        }

        /// <summary>
        /// A compact member list
        /// </summary>
        private void CompactList()
        {
            m_console.DisplayCompactMemberList(m_memberList.GetMembers());
            MemberListResponse();
        }

        /// <summary>
        /// A verbose member list
        /// </summary>
        private void VerboseList()
        {
            m_console.DisplayVerboseMemberList(m_memberList.GetMembers());
            MemberListResponse();
        }

        /// <summary>
        /// Get the users response from a member list and change the users menu position accordingly
        /// </summary>
        private void MemberListResponse()
        {
            view.Console.CurrentMenu currentMenu = m_console.GetCurrentMenu();
            int response = m_console.GetMemberListResponse();
            if (response > 0)
            {
                if (m_memberList.GetMembers().Count > 0)
                {
                    foreach (model.Member member in m_memberList.GetMembers())
                    {
                        if (member.GetMemberID() == response)
                        {
                            GoToCurrentMenu(member);
                            return;
                        }
                    }
                    m_console.SetCurrentMenu(currentMenu);
                }
            }
            GoToCurrentMenu();
        }

        /// <summary>
        /// Show a specific member menu
        /// </summary>
        /// <param name="a_member">model.Member, the member to be displayed</param>
        private void Member(model.Member a_member)
        {
            m_console.DisplayMemberMenu();
            m_console.DisplayMember(a_member, true, true);
            MemberResponse(a_member);
        }

        /// <summary>
        /// Read a response from the user, from the specific member menu and change the users menu position accordingly
        /// </summary>
        /// <param name="a_member"></param>
        private void MemberResponse(model.Member a_member)
        {
            int response = m_console.GetMemberResponse();
            if (response == -1)
            {
                m_memberList.RemoveMember(a_member);
                GoToCurrentMenu();
                return;
            }
            else if (response > 0)
            {
                foreach (model.Boat boat in a_member.GetBoatList().GetBoats())
                {
                    if (boat.GetBoatID() == response)
                    {
                        GoToCurrentMenu(a_member, response);
                        return;
                    }
                }
                m_console.SetCurrentMenu(view.Console.CurrentMenu.Member);
            }
            GoToCurrentMenu(a_member);
        }

        /// <summary>
        /// Add new member menu
        /// </summary>
        private void AddMember()
        {
            m_console.AddMember();
            string name;
            while (true)
            {
                m_console.WriteMessage("Name: ");
                name = m_console.ReadResponse();
                if (name.Length > 0)
                {
                    break;
                }
                m_console.WriteMessage("The member must have a name");
            }
            m_console.WriteMessage("Name: ");
            string pNumber;
            while (true)
            {
                m_console.WriteMessage("Personal Number: ");
                pNumber = m_console.ReadResponse();
                if (pNumber.All(char.IsNumber) && pNumber.Length == 12 && long.Parse(pNumber) >= 100000000000)
                {
                    break;
                }
                m_console.WriteMessage("Wrong format, format should be YYYYMMDDXXXX");
            }
            m_memberList.AddMember(name, long.Parse(pNumber));
            m_console.SetCurrentMenu(view.Console.CurrentMenu.Main);
            GoToCurrentMenu();
        }

        /// <summary>
        /// Edit a specific member menu
        /// </summary>
        /// <param name="a_member">model.Member, the member to be edited</param>
        private void EditMember(model.Member a_member)
        {
            m_console.EditMember(a_member);
            m_console.WriteMessage("New Name: ");
            string name = m_console.ReadResponse();
            string pNumber;
            while (true)
            {
                m_console.WriteMessage("New Personal Number: ");
                pNumber = m_console.ReadResponse();
                if (pNumber.All(char.IsNumber) && pNumber.Length == 12 && long.Parse(pNumber) >= 100000000000)
                {
                    break;
                }
                m_console.WriteMessage("Wrong format, format should be YYYYMMDDXXXX");
            }
            m_memberList.ChangeMemberInfo(a_member, name, long.Parse(pNumber));
            m_console.SetCurrentMenu(view.Console.CurrentMenu.Member);
            GoToCurrentMenu(a_member);
        }

        /// <summary>
        /// Add a boat menu
        /// </summary>
        /// <param name="a_member">model.Member, the member who owns the new boat</param>
        private void AddBoat(model.Member a_member)
        {
            m_console.AddBoat(a_member);
            for (int i = 0; i < (int)model.Boat.Type.Count; i += 1)
            {
                string message = string.Format("{0}: {1}", i, (model.Boat.Type)i);
                m_console.WriteMessage(message);
            }
            int type;
            while (true)
            {
                m_console.WriteMessage("Type: ");
                bool isNumeric = int.TryParse(m_console.ReadResponse(), out type);
                if (isNumeric && type >= 0 && type < (int)model.Boat.Type.Count)
                {
                    break;
                }
                m_console.WriteMessage("Invalid type, type must be a number choosen from the list above");
            }
            double length;
            while (true)
            {
                m_console.WriteMessage("Length: ");
                bool isDouble = double.TryParse(m_console.ReadResponse(), out length);
                if (isDouble && length > 0)
                {
                    break;
                }
                m_console.WriteMessage("Invalid length. Length must be of the format XX or XX,XX");
            }
            a_member.GetBoatList().AddBoat((model.Boat.Type)type, length);
            m_console.SetCurrentMenu(view.Console.CurrentMenu.Member);
            GoToCurrentMenu(a_member);
        }

        /// <summary>
        /// Show a specific boat menu
        /// </summary>
        /// <param name="a_member">model.Member, the member that owns the boat</param>
        /// <param name="a_boatID">int, the boatID of the boat</param>
        private void Boat(model.Member a_member, int a_boatID)
        {
            m_console.DisplayBoatMenu(a_member, a_boatID);
            BoatResponse(a_member, a_boatID);
        }

        /// <summary>
        /// Read a response from the user, from the specific boat menu and change the users menu position accordingly
        /// </summary>
        /// <param name="a_member">model.Member, the member that owns the boat</param>
        /// <param name="a_boatID">int, the boatID of the boat</param>
        private void BoatResponse(model.Member a_member, int a_boatID)
        {
            int response = m_console.GetBoatResponse();
            if (response == -1)
            {
                a_member.GetBoatList().RemoveBoat(a_boatID);
                GoToCurrentMenu(a_member);
                return;
            }
            GoToCurrentMenu(a_member, a_boatID);
        }

        /// <summary>
        /// Edit boat menu
        /// </summary>
        /// <param name="a_member">model.Member, the member that owns the boat</param>
        /// <param name="a_boatID">int, the boatID of the boat</param>
        private void EditBoat(model.Member a_member, int a_boatID)
        {
            bool boatExists = false;
            foreach (model.Boat boat in a_member.GetBoatList().GetBoats())
            {
                if (boat.GetBoatID() == a_boatID)
                {
                    boatExists = true;
                    break;
                }
            }
            if (boatExists)
            {
                m_console.EditBoat(a_member, a_boatID);
                for (int i = 0; i < (int)model.Boat.Type.Count; i += 1)
                {
                    string message = string.Format("{0}: {1}", i, (model.Boat.Type)i);
                    m_console.WriteMessage(message);
                }
                int type;
                while (true)
                {
                    m_console.WriteMessage("New type: ");
                    bool isNumeric = int.TryParse(m_console.ReadResponse(), out type);
                    if (isNumeric && type >= 0 && type < (int)model.Boat.Type.Count)
                    {
                        break;
                    }
                    m_console.WriteMessage("Invalid type, type must be a number choosen from the list above");
                }
                double length;
                while (true)
                {
                    m_console.WriteMessage("New length: ");
                    bool isDouble = double.TryParse(m_console.ReadResponse(), out length);
                    if (isDouble && length > 0)
                    {
                        break;
                    }
                    m_console.WriteMessage("Invalid length. Length must be of the format XX or XX,XX");
                }
                a_member.GetBoatList().ChangeBoatInfo(a_boatID, (model.Boat.Type)type, length);
            }
            m_console.SetCurrentMenu(view.Console.CurrentMenu.Member);
            GoToCurrentMenu(a_member);
        }

        /// <summary>
        /// Get the current menu and move to that menu position
        /// </summary>
        /// <param name="a_member">model.Member, optional. a member needed to access a specific menu</param>
        /// <param name="a_boatID">int, optional. A boatID needed to acces a specific menu</param>
        private void GoToCurrentMenu(model.Member a_member = null, int a_boatID = 0)
        {
            view.Console.CurrentMenu currentMenu = m_console.GetCurrentMenu();
            if (currentMenu == view.Console.CurrentMenu.CompactList)
            {
                CompactList();
            }
            else if (currentMenu == view.Console.CurrentMenu.VerboseList)
            {
                VerboseList();
            }
            else if (currentMenu == view.Console.CurrentMenu.AddMember)
            {
                AddMember();
            }
            else if (currentMenu == view.Console.CurrentMenu.Main)
            {
                Main();
            }
            else if (currentMenu == view.Console.CurrentMenu.Member)
            {
                Member(a_member);
            }
            else if (currentMenu == view.Console.CurrentMenu.EditMember)
            {
                EditMember(a_member);
            }
            else if (currentMenu == view.Console.CurrentMenu.Boat)
            {
                Boat(a_member, a_boatID);
            }
            else if (currentMenu == view.Console.CurrentMenu.AddBoat)
            {
                AddBoat(a_member);
            }
            else if (currentMenu == view.Console.CurrentMenu.EditBoat)
            {
                EditBoat(a_member, a_boatID);
            }
        }
    }
}
