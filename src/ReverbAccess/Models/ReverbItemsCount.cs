using System.Runtime.Serialization;

namespace ReverbAccess.Models
{
	[DataContract]
	internal sealed class ReverbItemsCount
	{
		[DataMember(Name = "count")]
		public int Count { get; set; }
	}
}