using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReverbAccess.Misc;
using ReverbAccess.Models;
using ReverbAccess.Models.Command;
using ReverbAccess.Models.Configuration;
using ReverbAccess.Models.Product;
using ReverbAccess.Services;
using CuttingEdge.Conditions;
using ServiceStack;
using System;
using ReverbAccess.Models.Order;

namespace ReverbAccess
{
	public class ReverbOrdersService : ReverbServiceBase, IReverbOrdersService
	{
		private readonly WebRequestServices _webRequestServices;

		public ReverbOrdersService( ReverbConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._webRequestServices = new WebRequestServices( config );
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

        public ReverbOrder GetOrdersSellingAll(DateTime dateFrom, DateTime dateTo)
        {
            ReverbOrder data;
            var endpoint = String.Empty;

            data = this.CollectOrdersSellingAll(endpoint);

            data = data ?? new ReverbOrder();

            return data;
        }

        public async Task<ReverbOrder> GetOrdersSellingAllAsync(DateTime dateFrom, DateTime dateTo)
        {
            ReverbOrder data;
            var endpoint = String.Empty;

            data = await this.CollectOrdersSellingAllAsync(endpoint);

            data = data ?? new ReverbOrder();

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
                this.CreateApiDelay().Wait();
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
                this.CreateApiDelay().Wait();
            });

            return data;
        }

        private ReverbOrder CollectOrdersSellingAll(string endpoint)
        {
            ReverbOrder data = null;

            ActionPolicies.Get.Do(() =>
            {
                data = this._webRequestServices.GetResponse<ReverbOrder>(ReverbCommand.GetOrdersSellingAll, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return data;
        }

        private async Task<ReverbOrder> CollectOrdersSellingAllAsync(string endpoint)
        {
            ReverbOrder data = null;

            await ActionPolicies.GetAsync.Do(async () =>
            {
                data = await this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersSellingAll, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

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
                this.CreateApiDelay().Wait();
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
                data = await this._webRequestServices.GetResponseAsync<ReverbOrder>(ReverbCommand.GetOrdersSellingAwaitingShipment, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return data;
        }
	}
}