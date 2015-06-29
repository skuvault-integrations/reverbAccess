using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ReverbAccess;
using ReverbAccess.Models.Configuration;
using ReverbAccess.Models.Order;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ReverbAccess.Models.Product;

namespace ReverbAccessTests.Products
{
	public class ProductsTests
	{
		private IReverbFactory ReverbFactory = new ReverbFactory();
		private ReverbConfig Config;

		[SetUp]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			
			const string credentialsFilePath = @"..\..\Files\ReverbCredentials.csv";

			var cc = new CsvContext();
			var testConfig =
				cc.Read<TestConfig>(credentialsFilePath, new CsvFileDescription {FirstLineHasColumnNames = true}).FirstOrDefault();

			if (testConfig != null)
			{
				this.ReverbFactory = new ReverbFactory(testConfig.NLogin, testConfig.NPassword, "https://sandbox.reverb.com");
				this.Config = new ReverbConfig(testConfig.Token);
			}
		}

		[Test]
		public void GetProducts()
		{
			var service = this.ReverbFactory.CreateProductsService(this.Config);
			var Products = service.GetProducts();

			Products.Should().NotBeNull();
		}

		[Test]
		public async Task GetProductsAsync()
		{
			var service = this.ReverbFactory.CreateProductsService(this.Config);
			var Products = await service.GetProductsAsync();

			Products.Should().NotBeNull();
		}

		[Test]
		public void UpdateProduct()
		{
			var service = this.ReverbFactory.CreateProductsService(this.Config);

			var listingToUpdate1 = new ReverbProductEntity
			{
				Slug = "603178",
				Sku = "inventory-20",
				Inventory = 7,
				HasInventory = true
			};

			var listingToUpdate2 = new ReverbProductEntity
			{
				Slug = "603158",
				Sku = "tester1234",
				Inventory = 23,
				HasInventory = true
			};

			service.UpdateProducts(new[] {listingToUpdate1, listingToUpdate2});
		}

		[Test]
		public async Task UpdateProductAsync()
		{
			var service = this.ReverbFactory.CreateProductsService(this.Config);

			var listingToUpdate = new ReverbProductEntity
			{
				Slug = "603158",
				Sku = "tester1234",
				Inventory = 12,
				HasInventory = true
			};

			await service.UpdateProductsAsync(new[] {listingToUpdate});
		}

		[Test]
		public void IsProductReceived()
		{
			var service = this.ReverbFactory.CreateProductsService(this.Config);
			var value = service.IsProductsReceived();

			value.Should().BeTrue();
		}

		[Test]
		public async Task IsProductReceivedAsync()
		{
			var service = this.ReverbFactory.CreateProductsService(this.Config);
			var value = await service.IsProductsReceivedAsync();

			value.Should().BeTrue();
		}
	}
}