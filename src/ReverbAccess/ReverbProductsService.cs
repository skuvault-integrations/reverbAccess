using System;
using System.Linq;
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
using ReverbAccess.Models.Product;
using ServiceStack;

namespace ReverbAccess
{
	public class ReverbProductsService : ReverbServiceBase, IReverbProductsService
	{
		private readonly WebRequestServices _webRequestServices;

		private const String PreconditionFailedMessage = "Precondition Failed for product {0}. Sku: {1}. Inventory: {2}";
		private const String NotFoundMessage = "Not found product {0}. Sku: {1}. Inventory: {2}";
		private const String UnprocessableEntityMessage = "Unprocessable Entity for product {0}. Sku: {1}. Inventory: {2}";

		public ReverbProductsService(ReverbConfig config)
		{
			Condition.Requires(config, "config").IsNotNull();

			this._webRequestServices = new WebRequestServices(config);
		}

		public IEnumerable<ReverbProductEntity> GetProducts()
		{
			ReverbProduct data = GetCommonProducts("all", null, null);

			return data.listings.Select(x => new ReverbProductEntity()
			{
				Sku = !String.IsNullOrEmpty(x.sku) ? x.sku : String.Empty,
				Slug = !String.IsNullOrEmpty(x.unique_id) ? x.unique_id : String.Empty,
				Inventory = x.inventory,
				HasInventory = x.inventory != 0
			})
				.ToList();
		}

		public async Task<IEnumerable<ReverbProductEntity>> GetProductsAsync()
		{
			ReverbProduct data = await GetCommonProductsAsync("all", null, null);

			return data.listings.Select(x => new ReverbProductEntity()
			{
				Sku = !String.IsNullOrEmpty(x.sku) ? x.sku : String.Empty,
				Slug = !String.IsNullOrEmpty(x.unique_id) ? x.unique_id : String.Empty,
				Inventory = x.inventory,
				HasInventory = x.inventory != 0
			})
				.ToList();
		}

		public ReverbProduct GetProductsDrafts(DateTime dateFrom, DateTime dateTo)
		{
			ReverbProduct data;
			var endpoint = String.Empty;

			data = this.CollectProductsDrafts(endpoint);

			data = data ?? new ReverbProduct();

			return data;
		}

		public async Task<ReverbProduct> GetProductsDraftsAsync(DateTime dateFrom, DateTime dateTo)
		{
			ReverbProduct data;
			var endpoint = String.Empty;

			data = await this.CollectProductsDraftsAsync(endpoint);

			data = data ?? new ReverbProduct();

			return data;
		}

		[Obsolete("Getting products by ID is no longer supported by Reverb and results in a 404 error.")]
		public ReverbProductData GetProductsById(String id)
		{
			ReverbProductData data;
			var endpoint = String.Empty;

			data = this.CollectProductsById(endpoint, id);

			data = data ?? new ReverbProductData();

			return data;
		}

		[Obsolete("Getting products by ID is no longer supported by Reverb and results in a 404 error.")]
		public async Task<ReverbProductData> GetProductsByIdAsync(String id)
		{
			ReverbProductData data;
			var endpoint = String.Empty;

			data = await this.CollectProductsByIdAsync(endpoint, id);

			data = data ?? new ReverbProductData();

			return data;
		}

		public ReverbProduct GetProductsBySKU(String sku) {
			ReverbProduct data;
			var endpoint = $"?sku={sku}&state=all";
			data = this.CollectProductsBySKU(endpoint);
			data = data ?? new ReverbProduct();
			return data;
		}

		public void UpdateProducts(IEnumerable<ReverbProductEntity> products)
		{
			foreach (var item in products)
			{
				ReverbProductParam param = new ReverbProductParam()
				{
					Sku = item.Sku,
					Slug = item.Slug,
					HasInventory = item.HasInventory,
					Inventory = item.Inventory,
					Price = item.Price
				};

				this.UpdateProductQuantity(param);
			}
		}

		public async Task UpdateProductsAsync(IEnumerable<ReverbProductEntity> products)
		{
			foreach (var item in products)
			{
				ReverbProductParam param = new ReverbProductParam()
				{
					Sku = item.Sku,
					Slug = item.Slug,
					HasInventory = item.HasInventory,
					Inventory = item.Inventory
				};

				await this.UpdateProductQuantityAsync(param);
			}
		}

		public Boolean IsProductsReceived()
		{
			try
			{
				var endpoint = ParamsBuilder.CreateProductsParams("all", 1, Int32.MaxValue);
				this._webRequestServices.GetResponse<ReverbProduct>(ReverbCommand.GetProducts, endpoint);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Boolean> IsProductsReceivedAsync()
		{
			try
			{
				var endpoint = ParamsBuilder.CreateProductsParams("all", 1, Int32.MaxValue);
				await this._webRequestServices.GetResponseAsync<ReverbProduct>(ReverbCommand.GetProducts, endpoint);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		#region products

		private ReverbProduct CollectProducts(string endpoint)
		{
			ReverbProduct data = null;

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbProduct>(ReverbCommand.GetProducts, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbProduct> CollectProductsAsync(string endpoint)
		{
			ReverbProduct data = null;

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data = await this._webRequestServices.GetResponseAsync<ReverbProduct>(ReverbCommand.GetProducts, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}

		private ReverbProduct GetCommonProducts(String state, Int32? page = null, Int32? per_page = null)
		{
			ReverbProduct data;

			if (!(page.HasValue && per_page.HasValue))
			{
				data = this.CollectProductsFromAllPages(state);
			}
			else
			{
				data = this.CollectProductsFromSinglePage(state, page.Value, per_page.Value);
			}

			data = data ?? new ReverbProduct();

			return data;
		}

		private async Task<ReverbProduct> GetCommonProductsAsync(String state, Int32? page = null, Int32? per_page = null)
		{
			ReverbProduct data;

			if (!(page.HasValue && per_page.HasValue))
			{
				data = await this.CollectProductsFromAllPagesAsync(state);
			}
			else
			{
				data = await this.CollectProductsFromSinglePageAsync(state, page.Value, per_page.Value);
			}

			data = data ?? new ReverbProduct();

			return data;
		}

		private ReverbProduct CollectProductsFromAllPages(string state)
		{
			ReverbProduct data = new ReverbProduct();
			data.listings = new List<ReverbProductItem>();

			ReverbProduct listingItem = null;

			Int32 pageIndex = 1;

			do
			{
				ActionPolicies.Get.Do(() =>
				{
					listingItem = CollectProductsFromSinglePage(state, pageIndex, Int32.MaxValue);

					//API requirement
					this.CreateApiDelay().Wait();

					if (listingItem.listings != null)
					{
						data.listings.AddRange(listingItem.listings);
					}

					pageIndex++;
				});
			} while (listingItem._links != null && listingItem._links.next != null);

			return data;
		}

		private async Task<ReverbProduct> CollectProductsFromAllPagesAsync(string state)
		{
			ReverbProduct data = new ReverbProduct();
			data.listings = new List<ReverbProductItem>();

			ReverbProduct listingItem = null;

			Int32 pageIndex = 1;

			do
			{
				await ActionPolicies.GetAsync.Do(async () =>
				{
					listingItem = await CollectProductsFromSinglePageAsync(state, pageIndex, Int32.MaxValue);

					//API requirement
					await this.CreateApiDelay();

					if (listingItem.listings != null)
					{
						data.listings.AddRange(listingItem.listings);
					}

					pageIndex++;
				});
			} while (listingItem._links != null && listingItem._links.next != null);

			return data;
		}

		private ReverbProduct CollectProductsFromSinglePage(string state, Int32 page, Int32 per_page)
		{
			ReverbProduct data = null;

			var endpoint = ParamsBuilder.CreateProductsParams(state, page, per_page);

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbProduct>(ReverbCommand.GetProducts, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbProduct> CollectProductsFromSinglePageAsync(string state, Int32 page, Int32 per_page)
		{
			ReverbProduct data = null;

			var endpoint = ParamsBuilder.CreateProductsParams(state, page, per_page);

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data = await this._webRequestServices.GetResponseAsync<ReverbProduct>(ReverbCommand.GetProducts, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}

		#endregion

		#region listings drafts

		private ReverbProduct CollectProductsDrafts(string endpoint)
		{
			ReverbProduct data = null;

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbProduct>(ReverbCommand.GetProductsDrafts, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbProduct> CollectProductsDraftsAsync(string endpoint)
		{
			ReverbProduct data = null;

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data = await this._webRequestServices.GetResponseAsync<ReverbProduct>(ReverbCommand.GetProductsDrafts, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}

		#endregion

		#region products by sku
		private ReverbProduct CollectProductsBySKU(string endpoint) {
			ReverbProduct data = null;

			ActionPolicies.Get.Do(() => {
				data = this._webRequestServices.GetResponse<ReverbProduct>(ReverbCommand.GetProductsBySKU, endpoint);
				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}
		#endregion

		#region products by id

		private ReverbProductData CollectProductsById(string endpoint, string id)
		{
			ReverbProductData data = null;

			ActionPolicies.Get.Do(() =>
			{
				data = this._webRequestServices.GetResponse<ReverbProductData>(ReverbCommand.GetProductsById, new[] {id}, endpoint);

				//API requirement
				this.CreateApiDelay().Wait();
			});

			return data;
		}

		private async Task<ReverbProductData> CollectProductsByIdAsync(string endpoint, string id)
		{
			ReverbProductData data = null;

			await ActionPolicies.GetAsync.Do(async () =>
			{
				data =
					await
						this._webRequestServices.GetResponseAsync<ReverbProductData>(ReverbCommand.GetProductsById, new[] {id}, endpoint);

				//API requirement
				await this.CreateApiDelay();
			});

			return data;
		}

		#endregion

		#region update quantity

		internal class jsonContainer {
			public bool has_inventory { get; set; }
			public int inventory { get; set; }
			public ReverbPrice price { get; set; }
		}

		private void UpdateProductQuantity(ReverbProductParam listing) { 			

			var data = new[] {listing.Slug};
			var container = new jsonContainer() { has_inventory = listing.HasInventory, inventory = listing.Inventory, price = listing.Price };
			var jsonContent = container.ToJson();

			ActionPolicies.Submit.Do(() =>
			{
				try
				{
					this._webRequestServices.PutJSONData(ReverbCommand.UpdateProduct, data, jsonContent);
				}
				catch (Exception ex)
				{
					if (ex.Message.Contains("Precondition Failed"))
					{
						ReverbLogger.Log.Error(ex, PreconditionFailedMessage, listing.Slug, listing.Sku, listing.Inventory);
					}
					else if (ex.Message.Contains("(404) Not Found"))
					{
						ReverbLogger.Log.Error(ex, NotFoundMessage, listing.Slug, listing.Sku, listing.Inventory);
					}
					else if (ex.Message.Contains("Unprocessable Entity"))
					{
						ReverbLogger.Log.Error(ex, UnprocessableEntityMessage, listing.Slug, listing.Sku, listing.Inventory);
					}
					else
					{
						throw ex;
					}
				}

				//API requirement
				this.CreateApiDelay().Wait();
			});
		}

		private async Task UpdateProductQuantityAsync(ReverbProductParam listing)
		{
			var data = new[] {listing.Slug};
			var jsonContent = String.Format(@"has_inventory={0}&inventory={1}", listing.HasInventory, listing.Inventory);

			await ActionPolicies.SubmitAsync.Do(async () =>
			{
				try
				{
					await this._webRequestServices.PutFormatDataAsync(ReverbCommand.UpdateProduct, data, jsonContent);
				}
				catch (Exception ex)
				{
					if (ex.Message.Contains("Precondition Failed"))
					{
						ReverbLogger.Log.Error(ex, PreconditionFailedMessage, listing.Slug, listing.Sku, listing.Inventory);
					}
					else if (ex.Message.Contains("(404) Not Found"))
					{
						ReverbLogger.Log.Error(ex, NotFoundMessage, listing.Slug, listing.Sku, listing.Inventory);
					}
					else if (ex.Message.Contains("Unprocessable Entity"))
					{
						ReverbLogger.Log.Error(ex, UnprocessableEntityMessage, listing.Slug, listing.Sku, listing.Inventory);
					}
					else
					{
						throw ex;
					}
				}

				//API requirement
				await this.CreateApiDelay();
			});
		}

		#endregion
	}
}