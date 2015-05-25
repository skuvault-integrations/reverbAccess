using CuttingEdge.Conditions;

namespace ReverbAccess.Models.Configuration
{
	public sealed class ReverbConfig
	{
		public string NativeHost{ get; private set; }
		public string CustomHost{ get; private set; }
		public string UserName{ get; private set; }
		public string Password{ get; private set; }
        public string ShopName { get; private set; }
        public string Token { get; private set; }

		public ReverbConfig( string userName, string password, string token )
		{
			Condition.Requires( userName, "userName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( password, "password" ).IsNotNullOrWhiteSpace();
            Condition.Requires( token, "token" ).IsNotNullOrWhiteSpace();

            this.NativeHost = string.Format(@"https://sandbox.reverb.com");
            this.CustomHost = string.Format(@"https://sandbox.reverb.com");
			this.UserName = userName;
			this.Password = password;
            this.Token = token;

            this.ShopName = string.Empty;
		}
	}
}