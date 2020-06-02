using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Product;
using System;
using ReverbAccess.Models.Order;
using ReverbAccess.Models.Shipping;

namespace ReverbAccess
{
	public interface IReverbOrdersService
	{
		IEnumerable<ReverbOrderEntity> GetOrders(DateTime dateFrom, DateTime dateTo);
		Task<IEnumerable<ReverbOrderEntity>> GetOrdersAsync(DateTime dateFrom, DateTime dateTo);
		String ShipOrder(String orderNumber, ReverbShippingEntity shippingEntity);

		Boolean IsOrdersReceived();
		Task<Boolean> IsOrdersReceivedAsync();
	}
}