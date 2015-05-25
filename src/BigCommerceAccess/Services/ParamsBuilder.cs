using System;
using System.Text;
using ReverbAccess.Models.Command;
using ReverbAccess.Models.Configuration;

namespace ReverbAccess.Services
{
	internal static class ParamsBuilder
	{
		public static readonly string EmptyParams = string.Empty;

        public static string CreateAuthParams(string email, string password)
        {
            // INFO: curl -XPOST --data "email=your@email.com&password=foo" https://reverb.com/api/auth/email

            var endpoint = string.Format("?{0}={1}&{2}={3}",
                ReverbParam.Email.Name, email,
                ReverbParam.Password.Name, password);
            return endpoint;
        }

        public static string CreateListingUpdateEndpoint(String slug)
        {
            var endpoint = string.Format("{0}.json", slug);
            return endpoint;
        }





		public static string CreateOrdersParams( DateTime startDate, DateTime endDate )
		{
			var endpoint = string.Format( "?{0}={1}&{2}={3}",
				ReverbParam.OrdersModifiedDateFrom.Name, DateTime.SpecifyKind( startDate, DateTimeKind.Utc ).ToString( "o" ),
				ReverbParam.OrdersModifiedDateTo.Name, DateTime.SpecifyKind( endDate, DateTimeKind.Utc ).ToString( "o" ) );
			return endpoint;
		}

		public static string CreateProductUpdateEndpoint( long productId )
		{
			var endpoint = string.Format( "{0}.json", productId );
			return endpoint;
		}

		public static string CreateProductOptionUpdateEndpoint( long productId, long optionId )
		{
			return string.Format( "{0}/skus/{1}.json", productId, optionId );
		}

		public static string CreateGetSinglePageParams( ReverbCommandConfig config )
		{
			var endpoint = string.Format( "?{0}={1}", ReverbParam.Limit.Name, config.Limit );
			return endpoint;
		}

		public static string CreateGetNextPageParams( ReverbCommandConfig config )
		{
			var endpoint = string.Format( "?{0}={1}&{2}={3}",
				ReverbParam.Limit.Name, config.Limit,
				ReverbParam.Page.Name, config.Page );
			return endpoint;
		}

		public static string ConcatParams( this string mainEndpoint, params string[] endpoints )
		{
			var result = new StringBuilder( mainEndpoint );

			foreach( var endpoint in endpoints )
				result.Append( endpoint.Replace( "?", "&" ) );

			return result.ToString();
		}
	}
}