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
using NUnit.Framework;
using ReverbAccess.Models.Product;

namespace ReverbAccessTests.Products
{
	public class ProductsTests
	{
		private readonly IReverbFactory ReverbFactory = new ReverbFactory();
		private ReverbConfig Config;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\ReverbCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this.Config = new ReverbConfig( testConfig.UserName, testConfig.Password, testConfig.Token, testConfig.NLogin, testConfig.NPassword, "https://sandbox.reverb.com" );
		}

		[ Test ]
		public void GetProducts()
		{
			var service = this.ReverbFactory.CreateProductsService( this.Config );
			var Products = service.GetProducts("all");

            Products.Should().NotBeNull();
		}

		[ Test ]
        public async Task GetProductsAsync()
		{
            var service = this.ReverbFactory.CreateProductsService(this.Config);
            var Products = await service.GetProductsAsync("all");

            Products.Should().NotBeNull();
		}

        [Test]
        public void UpdateProduct()
        {
            var service = this.ReverbFactory.CreateProductsService(this.Config);

            var listingToUpdate = new ReverbProductEntity { Slug = "603103-tadam", Inventory = 9, HasInventory = true };
            service.UpdateProducts(new[] { listingToUpdate });
        }

        [Test]
        public async Task UpdateProductAsync()
        {
            var service = this.ReverbFactory.CreateProductsService(this.Config);

            var listingToUpdate = new ReverbProductEntity { Slug = "603103-tadam", Inventory = 9, HasInventory = true };
            await service.UpdateProductsAsync(new[] { listingToUpdate });
        }
	}
}