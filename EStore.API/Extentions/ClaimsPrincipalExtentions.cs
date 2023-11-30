using System;
using System.Security.Claims;

namespace EStore.API.Extentions
{
	public static class ClaimsPrincipalExtentions
	{
		public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
		{
			return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
		}
	}
}

