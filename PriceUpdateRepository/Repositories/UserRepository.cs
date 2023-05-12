using Core.DataModels;
using Core.Interfaces.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PriceUpdateRepository.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceUpdateRepository.Repositories
{
	public class UserRepository : IUserRepository
	{
		protected Context _context;
		protected Context _contextupdate;
		protected IPasswordHasher<PriceUpdateRepository.DataModels.UserModel> _passwordHasher;
		public UserRepository(Context context, IPasswordHasher<PriceUpdateRepository.DataModels.UserModel> passwordHasher)
		{
			_context = context;
			_contextupdate = context;
			_passwordHasher = passwordHasher;
		}
		public UserDTO GetUser(string userName)
		{
			var user = _context.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() || x.Email.ToLower() == userName.ToLower());
			if (user != null)
			{
				return new UserDTO()
				{
					Id = user.Id,
					UserName = user.UserName,
					Role = user.UserRole,
					IsUserActive = user.Active,
					Accepted = user.Accepted,
					Language = user.Language,
					GenericID = user.GenericID,
					FirstName = user.FirstName,
					LastName= user.SecondName,
                    /*
					UserBaans = user.UserBaans.Select(x => new UserBaan
					{
						Baan = x.Baan,
						HasFullAccess = x.HasFullAccess
					}).ToList(),
					UserCompanies = user.UserCompanies.Where(x => x.Company != null).Select(x => new UserCompany
					{
						CompanyName = "test",
						CompanyId = "123",
						SortOrder = x.SortOrder
					}).ToList(),
					*/
                    IsAccessForbidden = user.IsAccessForbidden
					
				};
			}
			return null;
		}

		public bool InsertLogsInDB(int User_Id,string Action, string PartNumber )
		{
			try
			{
				var SearchLogsModel = new DataModels.SearchLogsModel()
				{
					UserId =User_Id.ToString(),
					Actions =Action,
					DateTime = DateTime.Now,
					PartNumber = PartNumber,
				};
				_context.Add(SearchLogsModel);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			

		}

		public UserDTO GetUserWithPassword(Core.Services.UserModel userModel)
		{
			var user = _context.Users.FirstOrDefault(x => x.Email.ToLower() == userModel.UserEmail.ToLower());

			if (null == user)
				return null;

			if (user != null && _passwordHasher.VerifyHashedPassword(user, user.Password, userModel.Password) == PasswordVerificationResult.Success)
			{
				return new UserDTO()
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.SecondName,
					UserName = user.UserName,
					Role = user.UserRole,
					IsUserActive = user.Active,
					/*
					UserBaans = user.UserBaans.Select(x => new UserBaan
					{
						Baan = x.Baan,
						HasFullAccess = x.HasFullAccess
					}).ToList(),
					UserCompanies = user.UserCompanies.Where(x => x.Company != null).Select(x => new UserCompany
					{
						CompanyName = "test",
						CompanyId = "1",
						SortOrder = x.SortOrder
					}).ToList(),
					*/
					IsAccessForbidden = user.IsAccessForbidden
				};
			}
			return null;
		}

		public void CreateUserIfNotExist(string username, bool shouldSaveChangesAfterAdding = true)
		{
			var user = _context.Users.FirstOrDefault(x => x.UserName == username);
			if (user == null)
			{
				user = new DataModels.UserModel()
				{
					UserName = username,
				};
				_context.Add(user);
				if (shouldSaveChangesAfterAdding)
				{
					_context.SaveChanges();
				}
			}
		}

		public void AddNewRegistration(Core.DataModels.UserRegistration userRegistration)
		{
			var userModel = new DataModels.UserModel()
			{
				Active = false,
				FirstName = userRegistration.FirstName,
				SecondName = userRegistration.LastName,
				CompanyName = userRegistration.CompanyName,
				CompanyAddres = userRegistration.CompanyAddres,
				Email = userRegistration.UserEmail,
				UserName = userRegistration.UserEmail,
				Comment = userRegistration.Comment
			};

			if (!string.IsNullOrWhiteSpace(userRegistration.Password))
            {
				userModel.Password = _passwordHasher.HashPassword(userModel, userRegistration.Password);
			}
			_context.Add(userModel);
			_context.SaveChanges();
		}

		public void UpdateAcceptedFlag(int User_Id)
		{
			var usermodel = _context.Users.FirstOrDefault(x => x.Id == User_Id);
			usermodel.Accepted = "Accepted";
			_context.SaveChanges();

		}
	}
}
