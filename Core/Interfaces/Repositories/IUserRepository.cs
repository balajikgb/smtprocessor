using Core.DataModels;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IUserRepository
	{
		UserDTO GetUserWithPassword(Core.Services.UserModel userMode);
		UserDTO GetUser(string username);
		bool InsertLogsInDB(int User_Id, string Action, string PartNumber);

		void UpdateAcceptedFlag(int User_Id);
		void AddNewRegistration(Core.DataModels.UserRegistration userRegistration);
	}
}
