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

        [CsvColumn(Name = "NLogin", FieldIndex = 4)]
        public string NLogin { get; set; }

        [CsvColumn(Name = "NPassword", FieldIndex = 5)]
        public string NPassword { get; set; }
	}
}