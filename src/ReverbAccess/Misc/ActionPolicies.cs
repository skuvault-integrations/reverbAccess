using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace ReverbAccess.Misc
{
	public static class ActionPolicies
	{
		public static ActionPolicy Submit
		{
			get { return _ReverbSumbitPolicy; }
		}

		private static readonly ActionPolicy _ReverbSumbitPolicy = ActionPolicy.Handle< Exception >().Retry( 10, ( ex, i ) =>
		{
			ReverbLogger.Log.Trace( ex, "Retrying Reverb API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync SubmitAsync
		{
			get { return _ReverbSumbitAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _ReverbSumbitAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( 10, async ( ex, i ) =>
		{
			ReverbLogger.Log.Trace( ex, "Retrying Reverb API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicy Get
		{
			get { return _ReverbGetPolicy; }
		}

		private static readonly ActionPolicy _ReverbGetPolicy = ActionPolicy.Handle< Exception >().Retry( 10, ( ex, i ) =>
		{
			ReverbLogger.Log.Trace( ex, "Retrying Reverb API get call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync GetAsync
		{
			get { return _ReverbGetAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _ReverbGetAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( 10, async ( ex, i ) =>
		{
			ReverbLogger.Log.Trace( ex, "Retrying Reverb API get call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );
	}
}