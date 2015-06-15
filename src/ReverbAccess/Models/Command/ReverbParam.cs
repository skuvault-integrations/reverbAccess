namespace ReverbAccess.Models.Command
{
	internal class ReverbParam
	{
		public static readonly ReverbParam Unknown = new ReverbParam(string.Empty);
		public static readonly ReverbParam OrdersCreatedDateFrom = new ReverbParam("created_start_date");
		public static readonly ReverbParam OrdersCreatedDateTo = new ReverbParam("created_end_date");
		public static readonly ReverbParam OrdersModifiedDateFrom = new ReverbParam("updated_start_date");
		public static readonly ReverbParam OrdersModifiedDateTo = new ReverbParam("updated_end_date");

		public static readonly ReverbParam PerPage = new ReverbParam("per_page");
		public static readonly ReverbParam Page = new ReverbParam("page");

		public static readonly ReverbParam ListingsState = new ReverbParam("state");

		public static readonly ReverbParam Email = new ReverbParam("email");
		public static readonly ReverbParam Password = new ReverbParam("password");

		private ReverbParam(string name)
		{
			this.Name = name;
		}

		public string Name { get; private set; }
	}
}