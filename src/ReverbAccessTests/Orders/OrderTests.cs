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
		private IReverbFactory ReverbFactory;
		private ReverbConfig Config;

		[SetUp]
		public void Init()
		{
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
		public void GetOrders()
		{
			var service = this.ReverbFactory.CreateOrdersService(this.Config);
			var orders = service.GetOrders(DateTime.UtcNow.AddDays(-400), DateTime.UtcNow);

			orders.Should().NotBeNull();
		}

		[Test]
		public async Task GetOrdersAsync()
		{
			var service = this.ReverbFactory.CreateOrdersService(this.Config);
			var orders = await service.GetOrdersAsync(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow);

			var st = orders.GroupBy(x => x.Status).Select(x => x.Key).ToList();

			orders.Should().NotBeNull();
		}

		[Test]
		public void IsOrdersReceived()
		{
			var service = this.ReverbFactory.CreateOrdersService(this.Config);
			var value = service.IsOrdersReceived();

			value.Should().BeTrue();
		}

		[Test]
		public async Task IsOrdersReceivedAsync()
		{
			var service = this.ReverbFactory.CreateOrdersService(this.Config);
			var value = await service.IsOrdersReceivedAsync();

			value.Should().BeTrue();
		}
	}
}