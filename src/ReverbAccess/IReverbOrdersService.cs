using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Product;
using System;
using ReverbAccess.Models.Order;

namespace ReverbAccess
{
	public interface IReverbOrdersService
	{
		IEnumerable<ReverbOrderEntity> GetOrders(DateTime dateFrom, DateTime dateTo);
		Task<IEnumerable<ReverbOrderEntity>> GetOrdersAsync(DateTime dateFrom, DateTime dateTo);

		Boolean IsOrdersReceived();
		Task<Boolean> IsOrdersReceivedAsync();
	}
}