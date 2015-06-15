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

		public static string CreateProductUpdateEndpoint(String slug)
		{
			var endpoint = string.Format("{0}.json", slug);
			return endpoint;
		}

		public static string CreateOrdersParams(DateTime dateFrom, DateTime dateTo)
		{
			// yyyy-MM-ddTHH:mm:sszzz
			var endpoint = string.Format("?{0}={1}&{2}={3}",
				ReverbParam.OrdersModifiedDateFrom.Name, DateTime.SpecifyKind(dateFrom, DateTimeKind.Utc).ToString("o"),
				ReverbParam.OrdersModifiedDateTo.Name, DateTime.SpecifyKind(dateTo, DateTimeKind.Utc).ToString("o"));
			return endpoint;
		}

		public static string CreateOrdersParams(DateTime dateFrom, DateTime dateTo, Int32 page, Int32 per_page)
		{
			// yyyy-MM-ddTHH:mm:sszzz
			var endpoint = string.Format("?{0}={1}&{2}={3}&{4}={5}&{6}={7}",
				ReverbParam.OrdersModifiedDateFrom.Name, DateTime.SpecifyKind(dateFrom, DateTimeKind.Utc).ToString("o"),
				ReverbParam.OrdersModifiedDateTo.Name, DateTime.SpecifyKind(dateTo, DateTimeKind.Utc).ToString("o"),
				ReverbParam.Page.Name, page,
				ReverbParam.PerPage.Name, per_page);
			return endpoint;
		}

		public static string CreateProductsParams(String state)
		{
			var endpoint = string.Format("?{0}={1}",
				ReverbParam.ListingsState.Name, state);
			return endpoint;
		}

		public static string CreateProductsParams(String state, Int32 page, Int32 per_page)
		{
			var endpoint = string.Format("?{0}={1}&{2}={3}&{4}={5}",
				ReverbParam.ListingsState.Name, state,
				ReverbParam.Page.Name, page,
				ReverbParam.PerPage.Name, per_page);
			return endpoint;
		}


		public static string CreateProductUpdateEndpoint(long productId)
		{
			var endpoint = string.Format("{0}.json", productId);
			return endpoint;
		}

		public static string CreateProductOptionUpdateEndpoint(long productId, long optionId)
		{
			return string.Format("{0}/skus/{1}.json", productId, optionId);
		}

		public static string CreateGetSinglePageParams(ReverbCommandConfig config)
		{
			var endpoint = string.Format("?{0}={1}", ReverbParam.PerPage.Name, config.Limit);
			return endpoint;
		}

		public static string CreateGetNextPageParams(ReverbCommandConfig config)
		{
			var endpoint = string.Format("?{0}={1}&{2}={3}",
				ReverbParam.PerPage.Name, config.Limit,
				ReverbParam.Page.Name, config.Page);
			return endpoint;
		}

		public static string ConcatParams(this string mainEndpoint, params string[] endpoints)
		{
			var result = new StringBuilder(mainEndpoint);

			foreach (var endpoint in endpoints)
				result.Append(endpoint.Replace("?", "&"));

			return result.ToString();
		}
	}
}