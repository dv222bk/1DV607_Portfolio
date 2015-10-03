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
            Main();
        }

        private void Main()
        {
            m_console.DisplayMainMenu();
            GetCurrentMenu();
        }

        private void CompactList()
        {
            m_console.DisplayCompactMemberList(m_memberList.GetMembers());
            MemberListResponse();
        }

        private void VerboseList()
        {
            m_console.DisplayVerboseMemberList(m_memberList.GetMembers());
            MemberListResponse();
        }

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
                            GetCurrentMenu(member);
                            return;
                        }
                    }
                    m_console.SetCurrentMenu(currentMenu);
                }
            }
            GetCurrentMenu();
        }

        private void Member(model.Member a_member)
        {
            m_console.DisplayMemberMenu();
            m_console.DisplayMember(a_member, true, true);
            MemberResponse(a_member);
        }

        private void MemberResponse(model.Member a_member)
        {
            int response = m_console.GetMemberResponse();
            if (response == -1)
            {
                m_memberList.RemoveMember(a_member);
                GetCurrentMenu();
                return;
            }
            else if (response > 0)
            {
                foreach (model.Boat boat in a_member.GetBoatList().GetBoats())
                {
                    if (boat.GetBoatID() == response)
                    {
                        GetCurrentMenu(a_member, response);
                        return;
                    }
                }
            }
            GetCurrentMenu(a_member);
        }

        private void AddMember()
        {
            m_console.AddMember();
            m_console.WriteMessage("Name: ");
            string name = m_console.ReadResponse();
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
            GetCurrentMenu();
        }

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
            GetCurrentMenu(a_member);
        }

        private void AddBoat(model.Member a_member)
        {
            m_console.AddBoat(a_member);
            for (int i = 0; i < (int)model.Boat.Type.Count; i += 1)
            {
                string message = string.Format("{0}: {1}", i, (model.Boat.Type)i);
                m_console.WriteMessage(message);
            }
            string type;
            while (true)
            {
                m_console.WriteMessage("Type: ");
                type = m_console.ReadResponse();
                if (type.All(char.IsNumber) && int.Parse(type) >= 0 && int.Parse(type) < (int)model.Boat.Type.Count)
                {
                    break;
                }
                m_console.WriteMessage("Invalid type, type must be a number choosen from the list above");
            }
            double outLength;
            while (true)
            {
                string length;
                m_console.WriteMessage("Length: ");
                length = m_console.ReadResponse();
                if (double.TryParse(length, out outLength))
                {
                    break;
                }
                m_console.WriteMessage("Invalid length. Length must be of the format XX or XX.XX");
            }
            a_member.GetBoatList().AddBoat((model.Boat.Type)int.Parse(type), outLength);
            m_console.SetCurrentMenu(view.Console.CurrentMenu.Member);
            GetCurrentMenu(a_member);
        }

        private void Boat(model.Member a_member, int a_boatID)
        {
            m_console.DisplayBoatMenu(a_member, a_boatID);
            BoatResponse(a_member, a_boatID);
        }

        private void BoatResponse(model.Member a_member, int a_boatID)
        {
            int response = m_console.GetBoatResponse();
            if (response == -1)
            {
                a_member.GetBoatList().RemoveBoat(a_boatID);
                GetCurrentMenu(a_member);
                return;
            }
            GetCurrentMenu(a_member, a_boatID);
        }

        private void EditBoat(model.Member a_member, int a_boatID)
        {
            m_console.EditBoat(a_member, a_boatID);
            for (int i = 0; i < (int)model.Boat.Type.Count; i += 1)
            {
                string message = string.Format("{0}: {1}", i, (model.Boat.Type)i);
                m_console.WriteMessage(message);
            }
            string type;
            while (true)
            {
                m_console.WriteMessage("New type: ");
                type = m_console.ReadResponse();
                if (type.All(char.IsNumber) && int.Parse(type) >= 0 && int.Parse(type) < (int)model.Boat.Type.Count)
                {
                    break;
                }
                m_console.WriteMessage("Invalid type, type must be a number choosen from the list above");
            }
            double outLength;
            while (true)
            {
                string length;
                m_console.WriteMessage("New length: ");
                length = m_console.ReadResponse();
                if (double.TryParse(length, out outLength))
                {
                    break;
                }
                m_console.WriteMessage("Invalid length. Length must be of the format XX or XX.XX");
            }
            a_member.GetBoatList().ChangeBoatInfo(a_boatID, (model.Boat.Type)int.Parse(type), outLength);
            m_console.SetCurrentMenu(view.Console.CurrentMenu.Member);
            GetCurrentMenu(a_member);
        }

        private void GetCurrentMenu(model.Member a_member = null, int a_boatID = 0)
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
