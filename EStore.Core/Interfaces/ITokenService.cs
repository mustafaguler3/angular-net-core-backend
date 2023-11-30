using System;
using EStore.Core.Entities;

namespace EStore.Core.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(AppUser user);
	}
}

