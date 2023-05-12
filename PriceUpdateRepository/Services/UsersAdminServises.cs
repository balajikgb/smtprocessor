using Core.Enums;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceUpdateRepository.Services
{
    public class UsersAdminServises : IUsersAdminServises
    {
        protected Context _context;
        public UsersAdminServises(Context context)
        {
            _context = context;
        }
        public List<UserDTO> GetUsers()
        {
            var users = _context.Users.ToList();
            if(users!=null)
            {
                List<UserDTO> userDTOs = users
                    .Select(u => new UserDTO()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.SecondName,
                        UserName = u.UserName.Trim(),
                        Role = u.UserRole,
                        IsUserActive = u.Active,
                        Language=u.Language,
                        IsAccessForbidden = u.IsAccessForbidden
                    })
                    .ToList();

                foreach (UserDTO user in userDTOs)
                {
                    if (user.Role == UserRoles.Admin)
                    {
                        // without `as` throws exception "'Array' does not contain a definition for 'Select'"
                        // even though linq is included on top of the file
                        //user.UserBaans = (Enum.GetValues(typeof(BaanEnum)) as IEnumerable<BaanEnum>)
                        //    .Select(baanEnum => new UserBaan()
                        //    {
                        //        Baan = baanEnum,
                        //        HasFullAccess = true
                        //    })
                        //    .ToList();
                    }
                }

                return userDTOs;
            }
            return null;
        }

        public bool ToggleUserIsForbiddenAccess(int userId, bool IsAccessForbidden)
        {
            PriceUpdateRepository.DataModels.UserModel userModel = _context.Users.FirstOrDefault(user => user.Id == userId);
            userModel.IsAccessForbidden = IsAccessForbidden;
            _context.SaveChanges();
            return true;
        }
    }
}
