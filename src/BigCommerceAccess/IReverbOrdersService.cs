using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Models.Product;
using System;
using ReverbAccess.Models.Order;

namespace ReverbAccess
{
	public interface IReverbOrdersService
	{
        ReverbOrder GetOrdersBuyingAll(DateTime dateFrom, DateTime dateTo);

        Task<ReverbOrder> GetOrdersBuyingAllAsync(DateTime dateFrom, DateTime dateTo);

        ReverbOrder GetOrdersBuyingUnpaid(DateTime dateFrom, DateTime dateTo);

        Task<ReverbOrder> GetOrdersBuyingUnpaidAsync(DateTime dateFrom, DateTime dateTo);

        ReverbOrder GetOrdersSellingAll(DateTime dateFrom, DateTime dateTo);

        Task<ReverbOrder> GetOrdersSellingAllAsync(DateTime dateFrom, DateTime dateTo);

        ReverbOrder GetOrdersSellingUnpaid(DateTime dateFrom, DateTime dateTo);

        Task<ReverbOrder> GetOrdersSellingUnpaidAsync(DateTime dateFrom, DateTime dateTo);

        ReverbOrder GetOrdersSellingAwaitingShipment(DateTime dateFrom, DateTime dateTo);

        Task<ReverbOrder> GetOrdersSellingAwaitingShipmentAsync(DateTime dateFrom, DateTime dateTo);
	}
}