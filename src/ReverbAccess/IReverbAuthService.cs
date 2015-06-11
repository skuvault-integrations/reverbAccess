using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Order;
using ReverbAccess.Models.Auth;

namespace ReverbAccess
{
	public interface IReverbAuthService
	{
        UserKeyToken GetUserToken(string email, string password);

        Task<UserKeyToken> GetUserTokenAsync(string email, string password);
	}
}