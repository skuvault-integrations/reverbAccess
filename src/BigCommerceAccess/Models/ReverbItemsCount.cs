using System.Runtime.Serialization;

namespace ReverbAccess.Models
{
	[ DataContract ]
	sealed class ReverbItemsCount
	{
		[ DataMember( Name = "count" ) ]
		public int Count { get; set; }
	}
}