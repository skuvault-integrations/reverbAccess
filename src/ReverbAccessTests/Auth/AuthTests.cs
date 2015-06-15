using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using ReverbAccess;
using NUnit.Framework;
using ReverbAccess.Models.Configuration;
using ReverbAccessTests;

namespace ReverbAccessTests.Auth
{
	public class AuthTests
	{
		private readonly IReverbFactory ReverbFactory = new ReverbFactory();
		private ReverbConfig Config;

		[SetUp]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\ReverbCredentials.csv";

			var cc = new CsvContext();
			var testConfig =
				cc.Read<TestConfig>(credentialsFilePath, new CsvFileDescription {FirstLineHasColumnNames = true}).FirstOrDefault();

			if (testConfig != null)
				this.Config = new ReverbConfig(testConfig.UserName, testConfig.Password, testConfig.Token, testConfig.NLogin,
					testConfig.NPassword);
		}

		[Test]
		public void GetUserToken()
		{
			var service = this.ReverbFactory.CreateAuthService(this.Config);
			var result = service.GetUserToken(this.Config.UserName, this.Config.Password);

			result.Should().NotBeNull();
		}

		[Test]
		public async Task GetUserTokenAsync()
		{
			var service = this.ReverbFactory.CreateAuthService(this.Config);
			var result = await service.GetUserTokenAsync(this.Config.UserName, this.Config.Password);

			result.Should().NotBeNull();
		}
	}
}