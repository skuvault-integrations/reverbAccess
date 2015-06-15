using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReverbAccess.Misc;
using ReverbAccess.Models.Command;
using ReverbAccess.Models.Configuration;
using ReverbAccess.Services;
using CuttingEdge.Conditions;
using System;
using ReverbAccess.Models.Order;

namespace ReverbAccess
{
	public class ReverbOrdersService : ReverbServiceBase, IReverbOrdersService
	{
		private readonly WebRequestServices _webRequestServices;

		public ReverbOrdersService(ReverbConfig config)
		{
			Condition.Requires(config, "config").IsNotNull();

			this._webRequestServices = new WebRequestServices(config);
		}

		#region Get

		public ReverbOrder GetOrdersBuyingAll(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = this.CollectOrdersBuyingAll(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public async Task<ReverbOrder> GetOrdersBuyingAllAsync(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = await this.CollectOrdersBuyingAllAsync(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public ReverbOrder GetOrdersBuyingUnpaid(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = this.CollectOrdersBuyingUnpaid(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public async Task<ReverbOrder> GetOrdersBuyingUnpaidAsync(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = await this.CollectOrdersBuyingUnpaidAsync(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public IEnumerable<ReverbOrderEntity> GetOrders(DateTime dateFrom, DateTime dateTo)
		{
			var reverbOrder = this.CollectOrdersSellingFromAllPages(dateFrom, dateTo);
			List<ReverbOrderEntity> data = reverbOrder.orders != null
				? reverbOrder.orders
					.Select(x => new ReverbOrderEntity(x))
					.ToList()
				: new List<ReverbOrderEntity>();

			//if (data.Count > 0)
			//    data.FirstOrDefault().Sku = "603103-boom";

			return data;
		}

		public async Task<IEnumerable<ReverbOrderEntity>> GetOrdersAsync(DateTime dateFrom, DateTime dateTo)
		{
			var reverbOrder = await this.CollectOrdersSellingFromAllPagesAsync(dateFrom, dateTo);
			List<ReverbOrderEntity> data = reverbOrder.orders != null
				? reverbOrder.orders
					.Select(x => new ReverbOrderEntity(x))
					.ToList()
				: new List<ReverbOrderEntity>();

			//if (data.Count > 0)
			//{
			//    data.FirstOrDefault().Sku = "603103-boom";
			//    data.FirstOrDefault().Quantity = 2;
			//    //data.FirstOrDefault().StatusStr = "payment_pending";
			//}

			return data;
		}

		public ReverbOrder GetOrdersSellingUnpaid(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = this.CollectOrdersSellingUnpaid(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public async Task<ReverbOrder> GetOrdersSellingUnpaidAsync(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = await this.CollectOrdersSellingUnpaidAsync(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public ReverbOrder GetOrdersSellingAwaitingShipment(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = this.CollectOrdersSellingAwaitingShipment(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public async Task<ReverbOrder> GetOrdersSellingAwaitingShipmentAsync(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data;
			var endpoint = String.Empty;

			data = await this.CollectOrdersSellingAwaitingShipmentAsync(endpoint);

			data = data ?? new ReverbOrder();

			return data;
		}

		public Boolean IsOrdersReceived()
		{
			try
			{
				var endpoint = ParamsBuilder.CreateOrdersParams(DateTime.Now.AddMonths(-1), DateTime.Now, 1, Int32.MaxValue);
				this._webRequestServices.GetResponse<ReverbOrder>(ReverbCommand.GetOrdersSellingAll, endpoint);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Boolean> IsOrdersReceivedAsync()
		{
			try
			{
				var endpoint = ParamsBuilder.CreateOrdersParams(DateTime.Now.AddMonths(-1), DateTime.Now, 1, Int32.MaxValue);
				await this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersSellingAll, endpoint);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		#endregion

		#region Update

		#endregion

		#region Count

		#endregion

		private ReverbOrder CollectOrdersBuyingAll(string endpoint)
		{
			ReverbOrder data = null;

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbOrder>(ReverbCommand.GetOrdersBuyingAll, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbOrder> CollectOrdersBuyingAllAsync(string endpoint)
		{
			ReverbOrder data = null;

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data = await this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersBuyingAll, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}

		private ReverbOrder CollectOrdersBuyingUnpaid(string endpoint)
		{
			ReverbOrder data = null;

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbOrder>(ReverbCommand.GetOrdersBuyingUnpaid, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbOrder> CollectOrdersBuyingUnpaidAsync(string endpoint)
		{
			ReverbOrder data = null;

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data = await this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersBuyingUnpaid, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}

		private ReverbOrder CollectOrdersSellingFromAllPages(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data = new ReverbOrder();
			data.orders = new List<ReverbOrderItem>();

			ReverbOrder dataItem = null;

			Int32 pageIndex = 1;

			do
			{
				ActionPolicies.Get.Do(() =>
				{
					dataItem = CollectOrdersSellingFromSinglePage(dateFrom, dateTo, pageIndex, Int32.MaxValue);

					//API requirement
					this.CreateApiDelay().Wait();

					if (dataItem.orders != null)
					{
						data.orders.AddRange(dataItem.orders);
					}

					pageIndex++;
				});
			} while (dataItem._links != null && dataItem._links.next != null);

			return data;
		}

		private async Task<ReverbOrder> CollectOrdersSellingFromAllPagesAsync(DateTime dateFrom, DateTime dateTo)
		{
			ReverbOrder data = new ReverbOrder();
			data.orders = new List<ReverbOrderItem>();

			ReverbOrder dataItem = null;

			Int32 pageIndex = 1;

			do
			{
				await ActionPolicies.GetAsync.Do(async () =>
				{
					dataItem = await CollectOrdersSellingFromSinglePageAsync(dateFrom, dateTo, pageIndex, Int32.MaxValue);

					//API requirement
					await this.CreateApiDelay();

					if (dataItem.orders != null)
					{
						data.orders.AddRange(dataItem.orders);
					}

					pageIndex++;
				});
			} while (dataItem._links != null && dataItem._links.next != null);

			return data;
		}

		private ReverbOrder CollectOrdersSellingFromSinglePage(DateTime dateFrom, DateTime dateTo, Int32 page, Int32 per_page)
		{
			ReverbOrder data = null;

			var endpoint = ParamsBuilder.CreateOrdersParams(dateFrom, dateTo, page, per_page);

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbOrder>(ReverbCommand.GetOrdersSellingAll, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			if (data.orders != null)
				data.orders = data.orders.Where(x => !String.IsNullOrEmpty(x.sku)).ToList();

			return data;
		}

		private async Task<ReverbOrder> CollectOrdersSellingFromSinglePageAsync(DateTime dateFrom, DateTime dateTo, Int32 page,
			Int32 per_page)
		{
			ReverbOrder data = null;

			var endpoint = ParamsBuilder.CreateOrdersParams(dateFrom, dateTo, page, per_page);

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data = await this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersSellingAll, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			if (data.orders != null)
				data.orders = data.orders.Where(x => !String.IsNullOrEmpty(x.sku)).ToList();

			return data;
		}

		private ReverbOrder CollectOrdersSellingUnpaid(string endpoint)
		{
			ReverbOrder data = null;

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbOrder>(ReverbCommand.GetOrdersSellingUnpaid, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbOrder> CollectOrdersSellingUnpaidAsync(string endpoint)
		{
			ReverbOrder data = null;

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data = await this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersSellingUnpaid, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}

		private ReverbOrder CollectOrdersSellingAwaitingShipment(string endpoint)
		{
			ReverbOrder data = null;

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbOrder>(ReverbCommand.GetOrdersSellingAwaitingShipment, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbOrder> CollectOrdersSellingAwaitingShipmentAsync(string endpoint)
		{
			ReverbOrder data = null;

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data =
					await
						this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersSellingAwaitingShipment, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}
	}
}