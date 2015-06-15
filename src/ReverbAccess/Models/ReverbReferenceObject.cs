using System.Runtime.Serialization;

namespace ReverbAccess.Models
{
	[DataContract]
	public sealed class ReverbReferenceObject
	{
		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "resource")]
		public string Resource { get; set; }
	}
}