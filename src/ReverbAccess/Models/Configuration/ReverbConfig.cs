using System;
using CuttingEdge.Conditions;

namespace ReverbAccess.Models.Configuration
{
	public sealed class ReverbConfig
	{
		public string NativeHost{ get; private set; }
		public string CustomHost{ get; private set; }
		public string UserName{ get; private set; }
		public string Password{ get; private set; }
        //public string ShopName { get; private set; }
        public string Token { get; private set; }

        public string NLogin { get; set; }
        public string NPassword { get; set; }

        public ReverbConfig(string token, string host = "https://reverb.com")
        {
            Condition.Requires(token, "token").IsNotNullOrWhiteSpace();

            this.NativeHost = host;
            this.CustomHost = host;
            this.Token = token;

            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.NLogin = string.Empty;
            this.NPassword = string.Empty;

            //this.ShopName = string.Empty;
        }

        public ReverbConfig(string login, string password, string token, string host = "https://reverb.com")
        {
            Condition.Requires(login, "nlogin").IsNotNullOrWhiteSpace();
            Condition.Requires(password, "npassword").IsNotNullOrWhiteSpace();
            Condition.Requires(token, "token").IsNotNullOrWhiteSpace();
            
            this.NativeHost = host;
            this.CustomHost = host;
            this.NLogin = login;
            this.NPassword = password;
            this.Token = token;
            
            this.UserName = string.Empty;
            this.Password = string.Empty;

            //this.ShopName = string.Empty;
        }

        public ReverbConfig(string userName, string password, string token, string nlogin, string npassword, string host = "https://reverb.com")
		{
			Condition.Requires( userName, "userName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( password, "password" ).IsNotNullOrWhiteSpace();
            Condition.Requires( token, "token" ).IsNotNullOrWhiteSpace();

            Condition.Requires(nlogin, "nlogin").IsNotNullOrWhiteSpace();
            Condition.Requires(npassword, "npassword").IsNotNullOrWhiteSpace();

            this.NativeHost = host;
            this.CustomHost = host;
			this.UserName = userName;
			this.Password = password;
            this.Token = token;

            this.NLogin = nlogin;
            this.NPassword = npassword;

            //this.ShopName = string.Empty;
		}
	}
}