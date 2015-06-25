using System;
using CuttingEdge.Conditions;

namespace ReverbAccess.Models.Configuration
{
	public sealed class ReverbConfig
	{
		public string NativeHost { get; internal set; }
		public string Token { get; internal set; }

		internal string Login { get; set; }
		internal string Password { get; set; }

		public ReverbConfig(string token)
		{
			Condition.Requires(token, "token").IsNotNullOrWhiteSpace();

			this.NativeHost = string.Empty;
			this.Token = token;

			this.Login = string.Empty;
			this.Password = string.Empty;
		}

		internal void AddConfigParams(string login, string password, string host)
		{
			this.Login = login;
			this.Password = password;
			this.NativeHost = host;
		}
	}
}