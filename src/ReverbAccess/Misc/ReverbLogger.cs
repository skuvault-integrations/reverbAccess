using Netco.Logging;

namespace ReverbAccess.Misc
{
	public class ReverbLogger
	{
		public static ILogger Log { get; private set; }

		static ReverbLogger()
		{
			Log = NetcoLogger.GetLogger("ReverbLogger");
		}
	}
}