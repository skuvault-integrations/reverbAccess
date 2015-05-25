using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReverbAccess.Misc;
using ReverbAccess.Models;
using ReverbAccess.Models.Address;
using ReverbAccess.Models.Command;
using ReverbAccess.Models.Configuration;
using ReverbAccess.Models.Order;
using ReverbAccess.Services;
using CuttingEdge.Conditions;
using ReverbAccess.Models.Listing;
using ServiceStack;

namespace ReverbAccess
{
	public class ReverbListingsService : ReverbServiceBase, IReverbListingsService
	{
		private readonly WebRequestServices _webRequestServices;

		public ReverbListingsService( ReverbConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._webRequestServices = new WebRequestServices( config );
		}

        public ReverbListing GetListings(DateTime dateFrom, DateTime dateTo)
        {
            ReverbListing listing;
            var endpoint = String.Empty;
            
            listing = this.CollectListings(endpoint);

            listing = listing ?? new ReverbListing();
            
            return listing;
        }

        public async Task<ReverbListing> GetListingsAsync(DateTime dateFrom, DateTime dateTo)
        {
            ReverbListing listing;
            var endpoint = String.Empty;

            listing = await this.CollectListingsAsync(endpoint);

            listing = listing ?? new ReverbListing();

            return listing;
        }

        public ReverbListing GetListingsDrafts(DateTime dateFrom, DateTime dateTo)
        {
            ReverbListing listing;
            var endpoint = String.Empty;

            listing = this.CollectListingsDrafts(endpoint);

            listing = listing ?? new ReverbListing();

            return listing;
        }

        public async Task<ReverbListing> GetListingsDraftsAsync(DateTime dateFrom, DateTime dateTo)
        {
            ReverbListing listing;
            var endpoint = String.Empty;

            listing = await this.CollectListingsDraftsAsync(endpoint);

            listing = listing ?? new ReverbListing();

            return listing;
        }

        public ReverbListingData GetListingsById(String id)
        {
            ReverbListingData listing;
            var endpoint = String.Empty;

            listing = this.CollectListingsById(endpoint, id);

            listing = listing ?? new ReverbListingData();

            return listing;
        }

        public async Task<ReverbListingData> GetListingsByIdAsync(String id)
        {
            ReverbListingData listing;
            var endpoint = String.Empty;

            listing = await this.CollectListingsByIdAsync(endpoint, id);

            listing = listing ?? new ReverbListingData();

            return listing;
        }

        public void UpdateListings(IEnumerable<ReverbListingParam> listings)
        {
            foreach (var listing in listings)
                this.UpdateListingQuantity(listing);
        }

        public async Task UpdateListingsAsync(IEnumerable<ReverbListingParam> listings)
        {
            foreach (var listing in listings)
                await this.UpdateListingQuantityAsync(listing);
        }

        #region listings

        private ReverbListing CollectListings(string endpoint)
        {
            ReverbListing listing = null;

            ActionPolicies.Get.Do(() =>
            {
                listing = this._webRequestServices.GetResponse<ReverbListing>(ReverbCommand.GetListings, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return listing;
        }

        private async Task<ReverbListing> CollectListingsAsync(string endpoint)
        {
            ReverbListing listing = null;

            await ActionPolicies.GetAsync.Do(async () =>
            {
                listing = await this._webRequestServices.GetResponseAsync<ReverbListing>(ReverbCommand.GetListings, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return listing;
        }

        #endregion

        #region listings drafts

        private ReverbListing CollectListingsDrafts(string endpoint)
        {
            ReverbListing listing = null;

            ActionPolicies.Get.Do(() =>
            {
                listing = this._webRequestServices.GetResponse<ReverbListing>(ReverbCommand.GetListingsDrafts, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return listing;
        }

        private async Task<ReverbListing> CollectListingsDraftsAsync(string endpoint)
        {
            ReverbListing listing = null;

            await ActionPolicies.GetAsync.Do(async () =>
            {
                listing = await this._webRequestServices.GetResponseAsync<ReverbListing>(ReverbCommand.GetListingsDrafts, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return listing;
        }

        #endregion

        #region listings by id

        private ReverbListingData CollectListingsById(string endpoint, string id)
        {
            ReverbListingData listing = null;

            ActionPolicies.Get.Do(() =>
            {
                listing = this._webRequestServices.GetResponse<ReverbListingData>(ReverbCommand.GetListingsById, new [] { id }, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return listing;
        }

        private async Task<ReverbListingData> CollectListingsByIdAsync(string endpoint, string id)
        {
            ReverbListingData listing = null;

            await ActionPolicies.GetAsync.Do(async () =>
            {
                listing = await this._webRequestServices.GetResponseAsync<ReverbListingData>(ReverbCommand.GetListingsById, new[] { id }, endpoint);

                //API requirement
                this.CreateApiDelay().Wait();
            });

            return listing;
        }

        #endregion

        #region update quantity

        private void UpdateListingQuantity(ReverbListingParam listing)
        {
            var endpoint = ParamsBuilder.CreateListingUpdateEndpoint(listing.Slug);
            var jsonContent = new { inventory = listing.Inventory, has_inventory = listing.HasInventory }.ToJson();

            ActionPolicies.Submit.Do(() =>
            {
                this._webRequestServices.PutData(ReverbCommand.UpdateListing, endpoint, jsonContent);

                //API requirement
                this.CreateApiDelay().Wait();
            });
        }

        private async Task UpdateListingQuantityAsync(ReverbListingParam listing)
        {
            var endpoint = ParamsBuilder.CreateListingUpdateEndpoint(listing.Slug);
            var jsonContent = new { inventory = listing.Inventory, has_inventory = listing.HasInventory }.ToJson();

            await ActionPolicies.SubmitAsync.Do(async () =>
            {
                await this._webRequestServices.PutDataAsync(ReverbCommand.UpdateListing, endpoint, jsonContent);
                //API requirement
                this.CreateApiDelay().Wait();
            });
        }

        #endregion
    }
}