using ReverbAccess.Models.Configuration;

namespace ReverbAccess
{
	public interface IReverbFactory
	{
        IReverbAuthService CreateAuthService( ReverbConfig config );
		IReverbListingsService CreateListingsService( ReverbConfig config );
		IReverbOrdersService CreateOrdersService( ReverbConfig config );
	}

	public sealed class ReverbFactory : IReverbFactory
	{
        public IReverbAuthService CreateAuthService( ReverbConfig config )
        {
            return new ReverbAuthService( config );
        }

        public IReverbListingsService CreateListingsService(ReverbConfig config)
		{
			return new ReverbListingsService( config );
		}

        public IReverbOrdersService CreateOrdersService(ReverbConfig config)
		{
			return new ReverbOrdersService( config );
		}
	}
}