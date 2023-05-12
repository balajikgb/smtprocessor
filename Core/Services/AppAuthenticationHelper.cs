using Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Services
{
    public static class AppAuthenticationHelper
    {
        public const string AzureADDSignInProvider = "AzureAd";
        public const string JWTSignInProvider = "JwtBearerAuthentication";
        public const string ItemsCustomClaimsConst = "CustomClaims";

        public static Claim CreateAzureAADClaim()
        {
            return new Claim(ClaimsTypeEnum.SignInProvider.ToString(), AzureADDSignInProvider);
        }

        public static Claim CreateJWTClaim()
        {
            return new Claim(ClaimsTypeEnum.SignInProvider.ToString(), JWTSignInProvider);
        }

        public static bool IsJWTSignInProvider(this ClaimsPrincipal principal)
        {
            var providerClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimsTypeEnum.SignInProvider.ToString());
            return providerClaim?.Value == JWTSignInProvider;
        }

        public static bool IsAzureADDSignInProvider(this ClaimsPrincipal principal)
        {
            var providerClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimsTypeEnum.SignInProvider.ToString());
            return providerClaim?.Value == AzureADDSignInProvider;
        }

        public static UserDTO GetUserDTO(this HttpContext principal)
        {
            return principal.Items[ItemsCustomClaimsConst] as UserDTO;
        }

        public static void AddUserDTO(this HttpContext principal, UserDTO user)
        {
            principal.Items[ItemsCustomClaimsConst] = user;
        }

        public static bool IsInCustomRole(this HttpContext principal, UserRoles? role = null)
        {
            var user = principal.GetUserDTO();
            return role == null ? (user?.Role != null) : user?.Role == role;
        }

        public static string GetUsername(this HttpContext principal)
        {
            return principal.GetUserDTO()?.UserName;
        }

        public static ICollection<UserCompany> GetUserCompanies(this HttpContext principal)
        {
            return principal.GetUserDTO()?.UserCompanies;
        }
    }
}
