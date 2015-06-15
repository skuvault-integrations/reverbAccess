using CuttingEdge.Conditions;

namespace ReverbAccess.Models.Configuration
{
	internal class ReverbCommandConfig
	{
		public int Page { get; private set; }
		public int Limit { get; private set; }

		public ReverbCommandConfig(int page, int limit)
			: this(limit)
		{
			Condition.Requires(page, "page").IsGreaterThan(0);
			Condition.Requires(limit, "limit").IsGreaterThan(0);

			this.Page = page;
		}

		public ReverbCommandConfig(int limit)
		{
			Condition.Requires(limit, "limit").IsGreaterThan(0);

			this.Limit = limit;
		}
	}
}