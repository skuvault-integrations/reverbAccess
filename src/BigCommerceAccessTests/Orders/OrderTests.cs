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

namespace ReverbAccessTests.Listings
{
	public class OrderTests
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
        public void GetOrdersBuyingAll()
		{
			var service = this.ReverbFactory.CreateOrdersService( this.Config );
			var listings = service.GetOrdersBuyingAll( DateTime.UtcNow.AddDays( -400 ), DateTime.UtcNow );

            listings.Should().NotBeNull();
		}

		[ Test ]
        public async Task GetOrdersBuyingAllAsync()
		{
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = await service.GetOrdersBuyingAllAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

            listings.Should().NotBeNull();
		}

        [Test]
        public void GetOrdersBuyingUnpaid()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = service.GetOrdersBuyingUnpaid(DateTime.UtcNow.AddDays(-400), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public async Task GetOrdersBuyingUnpaidAsync()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = await service.GetOrdersBuyingUnpaidAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public void GetOrdersSellingAll()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = service.GetOrdersSellingAll(DateTime.UtcNow.AddDays(-400), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public async Task GetOrdersSellingAllAsync()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = await service.GetOrdersSellingAllAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public void GetOrdersSellingUnpaid()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = service.GetOrdersSellingUnpaid(DateTime.UtcNow.AddDays(-400), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public async Task GetOrdersSellingUnpaidAsync()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = await service.GetOrdersSellingUnpaidAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public void GetOrdersSellingAwaitingShipment()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = service.GetOrdersSellingAwaitingShipment(DateTime.UtcNow.AddDays(-400), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }

        [Test]
        public async Task GetOrdersSellingAwaitingShipmentAsync()
        {
            var service = this.ReverbFactory.CreateOrdersService(this.Config);
            var listings = await service.GetOrdersSellingAwaitingShipmentAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

            listings.Should().NotBeNull();
        }
	}
}