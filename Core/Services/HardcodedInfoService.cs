using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
	public class HardcodedInfoService: IUserService
	{
        public UserDTO GetCurrentUser()
        {
            throw new NotImplementedException();
        }

		public string[] GetArasLoginDetails (string UserName)
        {
			return null;
        }

		public string GetCurrentUsername()
		{
			return "hendalf:)";
		}

		public string InsertLogs(int User_Id, string Action, string PartNumber)
		{
			return "Success";
		}

		public void UpdateAcceptedFlag(int User_Id)
		{
			
		
		}
	}
}
