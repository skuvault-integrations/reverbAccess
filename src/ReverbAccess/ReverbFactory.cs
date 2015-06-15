using ReverbAccess.Models.Configuration;

namespace ReverbAccess
{
	public interface IReverbFactory
	{
		IReverbAuthService CreateAuthService(ReverbConfig config);
		IReverbProductsService CreateProductsService(ReverbConfig config);
		IReverbOrdersService CreateOrdersService(ReverbConfig config);
	}

	public sealed class ReverbFactory : IReverbFactory
	{
		public IReverbAuthService CreateAuthService(ReverbConfig config)
		{
			return new ReverbAuthService(config);
		}

		public IReverbProductsService CreateProductsService(ReverbConfig config)
		{
			return new ReverbProductsService(config);
		}

		public IReverbOrdersService CreateOrdersService(ReverbConfig config)
		{
			return new ReverbOrdersService(config);
		}
	}
}