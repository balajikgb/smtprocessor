using Core.Services;
using System.Collections.Generic;
using System; 
using Core.Enums;
using Microsoft.Extensions.Hosting;
using Core.DataModels;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ArasPLMWebAp.Models;
using ArasPLMWebAp.Models.UserManagement;
using PriceUpdateWebApp.Services;
 


namespace PriceUpdateWebApp.Services
{
    public class Access
    {
        public Boolean UserAccess(UserDTO _user)
        {
            if (_user.Page == "User")
            {
                if (_user.Role == Core.Enums.UserRoles.Admin)
                {
                    if (_user.Action == "Create" || _user.Action == "View" || _user.Action == "Edit")
                    {
                        return true;
                    }
                }

                else if (_user.Role == Core.Enums.UserRoles.Manager)
                {
                    if (_user.Action == "View")
                    {
                        return true;
                    }
                }

            }
            if (_user.Page == "Batch")
            {
                if (_user.Role == Core.Enums.UserRoles.Admin)
                {
                    if (_user.Action == "Create" || _user.Action == "View" || _user.Action == "Edit")
                    {
                        return true;
                    }
                }

                else if (_user.Role == Core.Enums.UserRoles.Manager)
                {
                    if (_user.Action == "Create" || _user.Action == "View" || _user.Action == "Edit")
                    {
                        return true;
                    }
                }
                else if (_user.Role == Core.Enums.UserRoles.Editor)
                {
                    if (_user.Action == "Create" || _user.Action == "View" || _user.Action == "Edit")
                    {
                        return true;
                    }
                }
                else if (_user.Role == Core.Enums.UserRoles.Viewer)
                {
                    if (_user.Action == "View")
                    {
                        return true;
                    }
                }
            }

            if (_user.Page == "Environment")
            {
                if (_user.Role == Core.Enums.UserRoles.Admin)
                {
                    if (_user.Action == "Create" || _user.Action == "View" || _user.Action == "Edit")
                    {
                        return true;
                    }
                }

                else if (_user.Role == Core.Enums.UserRoles.Manager)
                {
                    if (_user.Action == "Create" || _user.Action == "View" || _user.Action == "Edit")
                    {
                        return true;
                    }
                }
                else if (_user.Role == Core.Enums.UserRoles.Editor)
                {
                    if (_user.Action == "View" || _user.Action == "Edit")
                    {
                        return true;
                    }
                }
                else if (_user.Role == Core.Enums.UserRoles.Viewer)
                {
                    if (_user.Action == "View")
                    {
                        return true;
                    }
                }
            }


            if (_user.Page == "MatrixView")
            {
                if (_user.Role == Core.Enums.UserRoles.Admin)
                {
                    if (_user.Action == "View")
                    {
                        return true;
                    }
                }
            }






                return false;
        }


        public List<ButtonAccessList> UserButtonAccess(UserDTO _user)
        {
            List<ButtonAccessList> objlist = new List<ButtonAccessList>();


            string Button = "";
            bool Status;


            if (_user.Role == Core.Enums.UserRoles.Admin)
            {

                ButtonAccessList BA = new ButtonAccessList();

                BA.Create = "true";
                BA.Save = "true";
                BA.QuickApply = "true";
                BA.Edit = "true";
                BA.Publish = "true";
                BA.View = "true";
                BA.Delete = "true";
                BA.MakeChanges = "true";
                objlist.Add(BA);

            }

            else if (_user.Role == Core.Enums.UserRoles.Manager)
            {
                ButtonAccessList BA = new ButtonAccessList();


                if (_user.Page == "Batch")
                {
                    BA.Create = "true";
                    BA.Save = "true";
                    BA.QuickApply = "true";
                    BA.Edit = "true";
                    BA.Publish = "true";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }

                if (_user.Page == "User")
                {
                    BA.Create = "false";
                    BA.Save = "false";
                    BA.QuickApply = "true";
                    BA.Edit = "false";
                    BA.Publish = "false";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }

                if (_user.Page == "Environment")
                {
                    BA.Create = "true";
                    BA.Save = "true";
                    BA.QuickApply = "true";
                    BA.Edit = "true";
                    BA.Publish = "true";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }


                if (_user.Page == "PriceUpdate")
                {
                    BA.Create = "false";
                    BA.Save = "true";
                    BA.QuickApply = "true";
                    BA.Edit = "true";
                    BA.Publish = "true";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }
                if (_user.Page == "MatrixData")
                {
                    BA.Create = "false";
                    BA.Save = "false";
                    BA.QuickApply = "true";
                    BA.Edit = "true";
                    BA.Publish = "false";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }


            }

            else if (_user.Role == Core.Enums.UserRoles.Editor)
            {
                ButtonAccessList BA = new ButtonAccessList();

                if (_user.Page == "Batch")
                {
                    BA.Create = "true";
                    BA.Save = "true";
                    BA.QuickApply = "false";
                    BA.Edit = "true";
                    BA.Publish = "false";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }

                if (_user.Page == "User")
                {
                    BA.Create = "false";
                    BA.Save = "false";
                    BA.QuickApply = "false";
                    BA.Edit = "false";
                    BA.Publish = "false";
                    BA.View = "false";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }

                if (_user.Page == "Environment")
                {
                    BA.Create = "false";
                    BA.Save = "false";
                    BA.QuickApply = "false";
                    BA.Edit = "true";
                    BA.Publish = "false";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }

                if (_user.Page == "PriceUpdate")
                {
                    BA.Create = "false";
                    BA.Save = "true";
                    BA.QuickApply = "false";
                    BA.Edit = "true";
                    BA.Publish = "true";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }
                if (_user.Page == "MatrixData")
                {
                    BA.Create = "false";
                    BA.Save = "false";
                    BA.QuickApply = "false";
                    BA.Edit = "true";
                    BA.Publish = "false";
                    BA.View = "true";
                    BA.Delete = "false";
                    BA.MakeChanges = "true";
                    objlist.Add(BA);
                }

            }

            else if (_user.Role == Core.Enums.UserRoles.Viewer)
            {
                ButtonAccessList BA = new ButtonAccessList();
                BA.Create = "false";
                BA.Save = "false";
                BA.QuickApply = "false";
                BA.Edit = "false";
                BA.Publish = "false";
                BA.View = "true";
                BA.Delete = "false";
                BA.MakeChanges = "false";
                objlist.Add(BA);
            }

            return objlist;



        }

    }
}
