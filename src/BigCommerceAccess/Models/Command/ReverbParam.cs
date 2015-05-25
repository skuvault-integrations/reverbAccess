namespace ReverbAccess.Models.Command
{
	internal class ReverbParam
	{
		public static readonly ReverbParam Unknown = new ReverbParam( string.Empty );
		public static readonly ReverbParam OrdersModifiedDateFrom = new ReverbParam( "min_date_modified" );
		public static readonly ReverbParam OrdersModifiedDateTo = new ReverbParam( "max_date_modified" );
		public static readonly ReverbParam Limit = new ReverbParam( "limit" );
		public static readonly ReverbParam Page = new ReverbParam( "page" );
        
        public static readonly ReverbParam Email = new ReverbParam("email");
        public static readonly ReverbParam Password = new ReverbParam("password");

		private ReverbParam( string name )
		{
			this.Name = name;
		}

		public string Name { get; private set; }
	}
}