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
using ReverbAccess.Models.Listing;

namespace ReverbAccessTests.Listings
{
	public class ListingsTests
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
				this.Config = new ReverbConfig( testConfig.UserName, testConfig.Password, testConfig.Token );
		}

		[ Test ]
		public void GetListings()
		{
			var service = this.ReverbFactory.CreateListingsService( this.Config );
			var listings = service.GetListings( DateTime.UtcNow.AddDays( -400 ), DateTime.UtcNow );

            listings.Should().NotBeNull();
		}

		[ Test ]
        public async Task GetListingsAsync()
		{
            var service = this.ReverbFactory.CreateListingsService(this.Config);
            var listings = await service.GetListingsAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

            listings.Should().NotBeNull();
		}

        [Test]
        public void GetListingsDrafts()
        {
            var service = this.ReverbFactory.CreateListingsService(this.Config);
            var listings = service.GetListingsDrafts(DateTime.UtcNow.AddDays(-400), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public async Task GetListingsDraftsAsync()
        {
            var service = this.ReverbFactory.CreateListingsService(this.Config);
            var listings = await service.GetListingsDraftsAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public void GetListingsById()
        {
            var service = this.ReverbFactory.CreateListingsService(this.Config);
            var listings = service.GetListingsById("732109");

            listings.Should().NotBeNull();
        }

        [Test]
        public async Task GetListingsByIdAsync()
        {
            var service = this.ReverbFactory.CreateListingsService(this.Config);
            var listings = await service.GetListingsByIdAsync("732109");

            listings.Should().NotBeNull();
        }

        [Test]
        public void UpdateListing()
        {
            var service = this.ReverbFactory.CreateListingsService(this.Config);
            
            var listingToUpdate = new ReverbListingParam { Slug = "732109", Inventory = 6, HasInventory = true };
            service.UpdateListings(new[] { listingToUpdate });
        }

        [Test]
        public async Task UpdateListingAsync()
        {
            var service = this.ReverbFactory.CreateListingsService(this.Config);

            var listingToUpdate = new ReverbListingParam { Slug = "732109", Inventory = 6, HasInventory = true };
            await service.UpdateListingsAsync(new[] { listingToUpdate });
        }
	}
}