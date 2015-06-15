using System.Threading.Tasks;
using ReverbAccess.Misc;
using ReverbAccess.Models.Command;
using ReverbAccess.Models.Configuration;
using ReverbAccess.Services;
using CuttingEdge.Conditions;
using ReverbAccess.Models.Auth;

namespace ReverbAccess
{
	public class ReverbAuthService : ReverbServiceBase, IReverbAuthService
	{
		private readonly WebRequestServices _webRequestServices;

		public ReverbAuthService(ReverbConfig config)
		{
			Condition.Requires(config, "config").IsNotNull();

			this._webRequestServices = new WebRequestServices(config);
		}

		public UserKeyToken GetUserToken(string email, string password)
		{
			UserKeyToken token = new UserKeyToken();
			var endpoint = ParamsBuilder.CreateAuthParams(email, password);

			ActionPolicies.Get.Do(() =>
			{
				token = this._webRequestServices.GetPostData<UserKeyToken>(ReverbCommand.GetToken, endpoint, "");

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return token;
		}

		public async Task<UserKeyToken> GetUserTokenAsync(string email, string password)
		{
			UserKeyToken token = new UserKeyToken();
			var endpoint = ParamsBuilder.CreateAuthParams(email, password);

			await ActionPolicies.GetAsync.Do(async () =>
			{
				token = await this._webRequestServices.GetPostDataAsync<UserKeyToken>(ReverbCommand.GetToken, endpoint, "");

				//API requirement
				await this.CreateApiDelay();
			});

			return token;
		}
	}
}