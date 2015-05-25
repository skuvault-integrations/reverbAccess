using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReverbAccess;
using ReverbAccess.Models.Configuration;
using ReverbAccess.Models.Product;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;

namespace ReverbAccessTests.Products
{
	[ TestFixture ]
	public class ProductsTetsts
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

	}
}