using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Core.Services
{
    public class AppAuthenticationHandler: IAppAuthenticationHandler, IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IUserRepository _userRepository;

        public AppAuthenticationHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public string GetAuthenticationScheme(string provider)
        {
            string authenticationScheme = null;

            if (String.Equals("JwtBearerAuthentication",
                provider, StringComparison.OrdinalIgnoreCase))
            {
                authenticationScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            else if (String.Equals("AAD",
                provider, StringComparison.OrdinalIgnoreCase))
            {
                authenticationScheme = "AzureAd";
            }

            return authenticationScheme;
        }

        public string GetCurrentUsername()
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (currentUser == null || currentUser.Identity?.IsAuthenticated != true)
            {
                return null;
            }
            if (AppAuthenticationHelper.IsJWTSignInProvider(currentUser))
            {
                return currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            }
            else
            {
                return currentUser.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
        }

        public UserDTO GetCurrentUser()
        {
            var username = GetCurrentUsername();
            return _userRepository.GetUser(username);
        }

        public string InsertLogs(int User_id, string Action, string PartNumber)
        {
            bool Result = _userRepository.InsertLogsInDB(User_id, Action, PartNumber);
            
            if (Result)
                return "Success";
            else
                return "Failed";
        
        }
        public void UpdateAcceptedFlag(int User_Id)
        {
            _userRepository.UpdateAcceptedFlag(User_Id);
        }
    }
}

