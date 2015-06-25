using System;
using ReverbAccess.Models.Configuration;

namespace ReverbAccess
{
	public interface IReverbFactory
	{
		IReverbProductsService CreateProductsService(ReverbConfig config);
		IReverbOrdersService CreateOrdersService(ReverbConfig config);
	}

	public sealed class ReverbFactory : IReverbFactory
	{
		private string Login { get; set; }

		private string Password { get; set; }

		private string Host { get; set; }

		public ReverbFactory()
		{
			this.Login = string.Empty;
			this.Password = string.Empty;
			this.Host = "https://reverb.com";
		}

		public ReverbFactory(string login, string password, string host = "https://reverb.com")
		{
			this.Login = login;
			this.Password = password;
			this.Host = host;
		}
		
		public IReverbProductsService CreateProductsService(ReverbConfig config)
		{
			config.AddConfigParams(this.Login, this.Password, this.Host);
			return new ReverbProductsService(config);
		}

		public IReverbOrdersService CreateOrdersService(ReverbConfig config)
		{
			config.AddConfigParams(this.Login, this.Password, this.Host);
			return new ReverbOrdersService(config);
		}
	}
}