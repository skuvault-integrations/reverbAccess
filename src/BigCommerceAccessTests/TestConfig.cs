using LINQtoCSV;

namespace ReverbAccessTests
{
	internal class TestConfig
	{
		[ CsvColumn( Name = "UserName", FieldIndex = 1 ) ]
		public string UserName { get; set; }

		[ CsvColumn( Name = "Password", FieldIndex = 2 ) ]
		public string Password { get; set; }

        [ CsvColumn(Name = "Token", FieldIndex = 3) ]
        public string Token { get; set; }
	}
}